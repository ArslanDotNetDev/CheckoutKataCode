using Checkout.kata.Domain.DTOs;
using Checkout.kata.Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.kata.Tests
{
    // Helper data for tests
    public static class TestData
    {
        public static Dictionary<string, PricingRule> GetPricingRules()
        {
            return new Dictionary<string, PricingRule>
            {
                { "A", new PricingRule("A", 50, new List<QuantityPriceOffer>(){ new QuantityPriceOffer(3, 130) }) },
                { "B", new PricingRule("B", 30, new List<QuantityPriceOffer>(){ new QuantityPriceOffer(2, 45) }) },
                { "C", new PricingRule("C", 20) },
                { "D", new PricingRule("D", 15) }
            };
        }
    }
}
