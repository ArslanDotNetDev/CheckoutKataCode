using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.kata.Domain.Models
{
    public class PricingRule
    {
        public string Sku { get; }
        public int UnitPrice { get; }
        public IReadOnlyList<QuantityPriceOffer> Offers { get; }

        public PricingRule(string sku, int unitPrice, IEnumerable<QuantityPriceOffer>? offers = null)
        {
            if (string.IsNullOrWhiteSpace(sku))
                throw new ArgumentException("SKU cannot be null or empty", nameof(sku));

            if (unitPrice <= 0)
                throw new ArgumentOutOfRangeException(nameof(unitPrice), "Unit price must be greater than zero");

            Sku = sku;
            UnitPrice = unitPrice;
            Offers = offers?.ToList() ?? new List<QuantityPriceOffer>();
        }
    }
}
