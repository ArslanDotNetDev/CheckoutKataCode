using Checkout.kata.Abstractions.contracts;
using Checkout.kata.Domain.Models;
using Checkout.kata.Services;

namespace Checkout.kata.Tests
{
    public class CheckoutTest
    {
        [Fact]
        public void Scan_SingleItemA_ReturnsCorrectPrice()
        {
            // Arrange
            var pricingRules = TestData.GetPricingRules();
            var checkout = new CheckoutService();

            // Act
            checkout.Scan("A");
            var total = checkout.GetTotalPrice();

            // Assert
            Assert.Equal(50, total); 
        }
    }

    
}