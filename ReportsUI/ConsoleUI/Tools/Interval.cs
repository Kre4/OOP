namespace UIReports.ConsoleUI.Tools
{
    public class Interval
    {
        public int From { get; set; }
        public int To { get; set; }

        public Interval(int from, int to)
        {
            From = from;
            To = to;
        }

        public bool IsInInterval(int digit)
        {
            return digit <= To && digit >= From;
        }
    }
}