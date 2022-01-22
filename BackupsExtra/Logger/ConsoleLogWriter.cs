using System;

namespace BackupsExtra.Logger
{
    public class ConsoleLogWriter : ILogWriter
    {
        public void WriteLog(string message)
        {
            Console.WriteLine(DateTime.Now + ": " + message);
        }
    }
}