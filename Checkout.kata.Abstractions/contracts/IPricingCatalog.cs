using Checkout.kata.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.kata.Abstractions.contracts
{
    public interface IPricingCatalog
    {
        bool TryGetPricing(string sku, out PricingRule rule);
    }
}
