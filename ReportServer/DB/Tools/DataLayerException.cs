using System;

namespace ReportServer.DB.Tools
{
    public class DataLayerException : Exception
    {
        public DataLayerException()
            : base()
        {
        }
        
        public DataLayerException(string message)
            : base(message)
        {
        }
    }
}