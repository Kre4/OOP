using System;
using Isu.Tools;

namespace IsuExtra.Tools
{
    public class OgnpException : IsuException
    {
        public OgnpException()
        {
        }

        public OgnpException(string message)
            : base(message)
        {
        }

        public OgnpException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}