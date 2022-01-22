namespace Banks.Tools
{
    public static class MathLogic
    {
        public static double GetPercentFromSum(double sum, double percent)
        {
            return sum * percent / 100;
        }
    }
}