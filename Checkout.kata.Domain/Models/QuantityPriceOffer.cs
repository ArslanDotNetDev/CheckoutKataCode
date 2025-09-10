using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.kata.Domain.Models
{
    public class QuantityPriceOffer
    {
        public int Quantity { get; }
        public int Price { get; }

        public QuantityPriceOffer(int quantity, int price)
        {
            if (quantity <= 0) throw new ArgumentOutOfRangeException(nameof(quantity));
            if (price <= 0) throw new ArgumentOutOfRangeException(nameof(price));

            Quantity = quantity;
            Price = price;
        }

    }
}
