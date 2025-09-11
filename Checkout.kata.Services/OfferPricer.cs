using Checkout.kata.Abstractions.contracts;
using Checkout.kata.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.kata.Services
{
    public sealed class OfferPricer : IOfferPricer
    {
        public int CalculatePrice(int quantity, PricingRule rule)
        {
            if (quantity <= 0) return 0;

            int total = 0;
            int remaining = quantity;

            foreach (var offer in rule.Offers.OrderByDescending(o => o.Quantity))
            {
                int bundles = remaining / offer.Quantity;
                total += bundles * offer.Price;
                remaining %= offer.Quantity;
            }

            total += remaining * rule.UnitPrice;
            return total;
        }
    }
}
