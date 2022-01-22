namespace Banks.Tools
{
    public struct DepositRange
    {
        private double _from;
        private double _to;
        private double _percent;

        public DepositRange(double startOfRange, double endOfRange, double percent)
        {
            _from = startOfRange;
            _to = endOfRange;
            _percent = percent;
        }

        public double GetStartOfRange() => _from;
        public double GetEndOfRange() => _to;
        public double GetPercent() => _percent;

        public bool IsInDepositRange(double sum)
        {
            return sum >= _from && sum <= _to;
        }
    }
}