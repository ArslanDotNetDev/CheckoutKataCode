using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.kata.Domain.DTOs
{
    public class PricingRuleDTO
    {
        public string Sku { get; set; } = string.Empty;
        public int UnitPrice { get; set; }
        public List<QuantityPriceOfferDTO>? Offers { get; set; }
    }
}
