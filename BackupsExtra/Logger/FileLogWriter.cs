using System;
using System.IO;
using System.Net;

namespace BackupsExtra.Logger
{
    public class FileLogWriter : ILogWriter
    {
        private string _pathToLog;

        public FileLogWriter(string pathToLog)
        {
            _pathToLog = pathToLog;
            File.Create(_pathToLog);
        }

        public void WriteLog(string message)
        {
            File.AppendText(DateTime.Now + ": " + message);
        }
    }
}