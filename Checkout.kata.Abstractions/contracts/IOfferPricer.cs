using Checkout.kata.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.kata.Abstractions.contracts
{
    public interface IOfferPricer
    {
        int CalculatePrice(int quantity, PricingRule rule);
    }
}
