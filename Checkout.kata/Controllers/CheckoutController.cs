using Checkout.kata.Abstractions.contracts;
using Microsoft.AspNetCore.Mvc;

namespace Checkout.kata.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CheckoutController : ControllerBase
    {
        private readonly ICheckout _checkout;
        public CheckoutController(ICheckout checkout)
        {
            _checkout = checkout;
        }

        
    }
}
