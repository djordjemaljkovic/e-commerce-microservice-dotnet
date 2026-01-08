using System;
using System.Collections.Generic;
using System.Text;

namespace Main.Exceptions
{
    public class NotFound : Exception
    {
        public NotFound(string message) : base(message)
        {
            
        }

        public NotFound(string name, object key) : base($"Entity \"{name}\" ({key}))")
        {
            
        }
    }
}
