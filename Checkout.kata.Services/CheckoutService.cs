using Checkout.kata.Abstractions.contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.kata.Services
{
    public class CheckoutService : ICheckout
    {
        private readonly IPricingCatalog _catalog;
        private readonly IOfferPricer _pricer;
        private readonly Dictionary<string, int> _items = new(StringComparer.OrdinalIgnoreCase);

        public CheckoutService(IPricingCatalog catalog, IOfferPricer pricer)
        {
            _catalog = catalog ?? throw new ArgumentNullException(nameof(catalog));
            _pricer = pricer ?? throw new ArgumentNullException(nameof(pricer));
        }

        public void Scan(string item)
        {
            if (!_catalog.TryGetPricing(item, out _))
                throw new ArgumentException($"Unknown SKU: {item}");

            if (_items.ContainsKey(item))
                _items[item]++;
            else
                _items[item] = 1;
        }

        public int GetTotalPrice()
        {
            int total = 0;
            foreach (var entry in _items)
            {
                if (!_catalog.TryGetPricing(entry.Key, out var rule))
                    throw new InvalidOperationException($"Pricing not found for SKU {entry.Key}");

                total += _pricer.CalculatePrice(entry.Value, rule);
            }
            return total;
        }
    }
}
