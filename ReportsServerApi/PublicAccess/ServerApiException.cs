using System;

namespace ReportsServerApi.PublicAccess
{
    public class ServerApiException : Exception
    {
        public ServerApiException() :
            base()
        {
        }

        public ServerApiException(string message) :
            base(message)
        {
        }
    }
}