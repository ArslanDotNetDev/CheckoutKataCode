using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.kata.Domain.Models
{
    public class ErrorResponse
    {
        public string? Message { get; set; }
        public string? Detail { get; set; }
        public string? TraceId { get; set; }

    }
}
