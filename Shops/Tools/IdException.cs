using System;

namespace Shops.Tools
{
    public class IdException : Exception
    {
        public IdException()
        {
        }

        public IdException(string message)
            : base(message)
        {
        }

        public IdException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}