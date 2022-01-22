using System;

namespace BackupsExtra.Tools
{
    public class RestorePointsException : Exception
    {
        public RestorePointsException()
            : base()
        {
        }

        public RestorePointsException(string message)
            : base(message)
        {
        }

        public RestorePointsException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}