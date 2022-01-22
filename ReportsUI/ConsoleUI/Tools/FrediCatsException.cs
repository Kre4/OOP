using System;

namespace UIReports.ConsoleUI.Tools
{
    public class FrediCatsException : Exception
    {
        public FrediCatsException() :
            base()
        {
        }

        public FrediCatsException(string message) :
            base(message)
        {
        }
    }
}