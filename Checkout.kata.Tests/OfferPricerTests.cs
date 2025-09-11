using Checkout.kata.Abstractions.contracts;
using Checkout.kata.Domain.Models;
using Checkout.kata.Services;

namespace Checkout.kata.Tests
{
    public class OfferPricerTests
    {
        private readonly OfferPricer _pricer = new OfferPricer();

        [Fact]
        public void CalculatePrice_ZeroQuantity_ReturnsZero()
        {
            var rule = new PricingRule("A", 50);

            var total = _pricer.CalculatePrice(0, rule);

            Assert.Equal(0, total);
        }

        [Fact]
        public void CalculatePrice_NoOffers_UsesUnitPrice()
        {
            var rule = new PricingRule("A", 50);

            var total = _pricer.CalculatePrice(3, rule);

            Assert.Equal(150, total);
        }

        [Fact]
        public void CalculatePrice_WithSingleOffer_AppliesOfferCorrectly()
        {
            var rule = new PricingRule("A", 50, new List<QuantityPriceOffer>() { new QuantityPriceOffer(3, 130) });

            var total = _pricer.CalculatePrice(3, rule);

            Assert.Equal(130, total);
        }

        [Fact]
        public void CalculatePrice_WithOfferAndExtra_UsesOfferPlusRemaining()
        {
            var rule = new PricingRule("A", 50, new List<QuantityPriceOffer>() { new QuantityPriceOffer(3, 130) });

            var total = _pricer.CalculatePrice(4, rule);

            Assert.Equal(180, total);
        }

        [Fact]
        public void CalculatePrice_WithMultipleBundles_AppliesOfferMultipleTimes()
        {
            var rule = new PricingRule("A", 50, new List<QuantityPriceOffer>() { new QuantityPriceOffer(3, 130) });

            var total = _pricer.CalculatePrice(6, rule);

            Assert.Equal(260, total);
        }

        [Fact]
        public void CalculatePrice_MultipleOffers_AppliesHighestQuantityFirst()
        {
            var rule = new PricingRule("A", 50,
                new List<QuantityPriceOffer>() { new QuantityPriceOffer(5, 200), new QuantityPriceOffer(3, 130) });

            var total = _pricer.CalculatePrice(8, rule);

            Assert.Equal(330, total);
        }

    }
}