using Checkout.kata.Abstractions.contracts;
using Checkout.kata.Domain.DTOs;
using Checkout.kata.Domain.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace Checkout.kata.Infrastructure
{
    public class CachePricingCatalog : IPricingCatalog
    {
        private readonly IMemoryCache _cache;
        private readonly PricingDTO _config;

        private const string CacheKey = "PricingRules";

        public CachePricingCatalog(IOptions<PricingDTO> options, IMemoryCache cache)
        {
            _config = options?.Value ?? throw new ArgumentNullException(nameof(options));
            _cache = cache;
        }

        private IReadOnlyDictionary<string, PricingRule> LoadRules()
        {
            return _config.Rules
                .Select(r =>
                    new PricingRule(
                        r.Sku,
                        r.UnitPrice,
                        r.Offers?.Select(o => new QuantityPriceOffer(o.Quantity, o.Price))
                    ))
                .ToDictionary(r => r.Sku, StringComparer.OrdinalIgnoreCase);
        }

        private IReadOnlyDictionary<string, PricingRule> GetCachedRules()
        {
            return _cache.GetOrCreate(CacheKey, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5); 
                return LoadRules();
            })!;
        }

        public bool TryGetPricing(string sku, out PricingRule rule)
        {
            var dict = GetCachedRules();
            return dict.TryGetValue(sku, out rule!);
        }
    }
}
