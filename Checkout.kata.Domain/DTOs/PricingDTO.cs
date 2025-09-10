using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.kata.Domain.DTOs
{
    public class PricingDTO
    {
        public List<PricingRuleDTO> Rules { get; set; } = new();
    }
}
