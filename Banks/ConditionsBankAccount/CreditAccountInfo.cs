using Banks.BankAccount;

namespace Banks.ConditionsBankAccount
{
    public class CreditAccountInfo : IAccountInfo
    {
        private double _limit;
        private double _fineOnNegativeBalance;

        public CreditAccountInfo(double limit, double fineOnNegativeBalance)
        {
            _limit = limit;
            _fineOnNegativeBalance = fineOnNegativeBalance;
        }

        public string GetName()
        {
            return "CreditAccount " + _limit + _fineOnNegativeBalance;
        }

        public IAccount GenerateBankAccount(double starterBalance)
        {
            return new CreditAccount(_limit, _fineOnNegativeBalance, starterBalance);
        }

        public double GetLimit() => _limit;
        public double GetNegativeBlanceFine() => _fineOnNegativeBalance;
    }
}