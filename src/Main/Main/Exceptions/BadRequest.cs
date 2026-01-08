using System;
using System.Collections.Generic;
using System.Text;

namespace Main.Exceptions
{
    public class BadRequest : Exception
    {
        public BadRequest(string message) : base(message)
        {

        }

        public BadRequest(string message, string details) : base(message)
        {
            Details = details;
        }

        public string? Details { get; set; }
    }
}
