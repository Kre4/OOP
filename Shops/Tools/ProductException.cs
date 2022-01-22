using System;

namespace Shops.Tools
{
    public class ProductException : Exception
    {
        public ProductException()
        {
        }

        public ProductException(string message)
            : base(message)
        {
        }

        public ProductException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}