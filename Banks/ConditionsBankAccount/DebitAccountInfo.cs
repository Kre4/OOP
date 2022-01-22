using Banks.BankAccount;

namespace Banks.ConditionsBankAccount
{
    public class DebitAccountInfo : IAccountInfo
    {
        private double _percent;

        public DebitAccountInfo(double percent)
        {
            _percent = percent;
        }

        public string GetName()
        {
            return "DebitAccount" + _percent;
        }

        public IAccount GenerateBankAccount(double starterBalance)
        {
            return new DebitAccount(_percent, starterBalance);
        }

        public double GetPercent() => _percent;
    }
}