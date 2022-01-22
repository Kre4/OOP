namespace BackupsExtra.Logger
{
    public class NoLog : ILogWriter
    {
        public void WriteLog(string message)
        {
        }
    }
}