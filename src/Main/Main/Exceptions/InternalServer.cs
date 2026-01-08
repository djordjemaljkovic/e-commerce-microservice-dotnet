using System;
using System.Collections.Generic;
using System.Text;

namespace Main.Exceptions
{
    public class InternalServer : Exception
    {
        public InternalServer(string message) : base(message)
        {
            
        }

        public InternalServer(string message, string details) : base(message)
        {
            Details = details;
        }

        public string? Details { get; set; }

    }
}
