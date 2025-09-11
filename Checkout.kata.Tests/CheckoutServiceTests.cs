using Checkout.kata.Abstractions.contracts;
using Checkout.kata.Domain.Models;
using Checkout.kata.Services;
using Xunit;
using Moq;

namespace Checkout.kata.Tests
{
    public class CheckoutServiceTests
    {
        private readonly Mock<IPricingCatalog> _catalogMock = new();
        private readonly Mock<IOfferPricer> _pricerMock = new();
        private readonly CheckoutService _checkout;

        public CheckoutServiceTests()
        {
            _checkout = new CheckoutService(_catalogMock.Object, _pricerMock.Object);
        }

        [Fact]
        public void Scan_UnknownSku_ThrowsArgumentException()
        {
            _catalogMock.Setup(c => c.TryGetPricing("X", out It.Ref<PricingRule>.IsAny))
                        .Returns(false);

            Assert.Throws<ArgumentException>(() => _checkout.Scan("X"));
        }

        [Fact]
        public void Scan_ValidSku_AddsItem()
        {
            var rule = new PricingRule("A", 50);
            _catalogMock.Setup(c => c.TryGetPricing("A", out rule))
                        .Returns(true);

            _checkout.Scan("A");

            _pricerMock.Setup(p => p.CalculatePrice(1, rule)).Returns(50);

            var total = _checkout.GetTotalPrice();

            Assert.Equal(50, total);
        }

        [Fact]
        public void GetTotalPrice_CallsOfferPricerWithCorrectQuantity()
        {
            var rule = new PricingRule("A", 50);
            _catalogMock.Setup(c => c.TryGetPricing("A", out rule)).Returns(true);

            _checkout.Scan("A");
            _checkout.Scan("A");

            _pricerMock.Setup(p => p.CalculatePrice(2, rule)).Returns(100);

            var total = _checkout.GetTotalPrice();

            _pricerMock.Verify(p => p.CalculatePrice(2, rule), Times.Once);
            Assert.Equal(100, total);
        }

        [Fact]
        public void GetTotalPrice_UnknownSkuInCatalog_ThrowsInvalidOperation()
        {
            var rule = new PricingRule("A", 50);
            _catalogMock.Setup(c => c.TryGetPricing("A", out rule)).Returns(true);
            _checkout.Scan("A");

            _catalogMock.Setup(c => c.TryGetPricing("A", out rule)).Returns(false);

            Assert.Throws<InvalidOperationException>(() => _checkout.GetTotalPrice());
        }
    }
}