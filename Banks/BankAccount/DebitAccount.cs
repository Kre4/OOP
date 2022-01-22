using System;
using Banks.Tools;

namespace Banks.BankAccount
{
    public class DebitAccount : IAccount
    {
        private double _percent;
        private double _balance;
        private double _addAtTheEndOfMonth;
        private Guid _accountGuid;

        public DebitAccount(double percent, double starterBalance)
        {
            _percent = percent;
            CheckBalance(starterBalance);
            _balance = starterBalance;
            _addAtTheEndOfMonth = 0;
            _accountGuid = Guid.NewGuid();
        }

        public Guid GetGuid() => _accountGuid;
        public double GetMoney() => _balance;

        public void HandleCommissionEndOfDay()
        {
            _addAtTheEndOfMonth += MathLogic.GetPercentFromSum(_balance, _percent / Consts.DaysInYear);
        }

        public void HandleCommissionEndOfMonth()
        {
            _balance += _addAtTheEndOfMonth;
            _addAtTheEndOfMonth = 0;
        }

        public void RemoveMoney(double amount)
        {
            CheckBalance(_balance - amount);
            _balance -= amount;
        }

        public void DepositMoney(double amount)
        {
            RemoveMoney(-amount);
        }

        public void Cancel(double amount)
        {
            _balance += amount;
        }

        private void CheckBalance(double balance)
        {
            if (balance < 0)
                throw new BankException("Not enough money");
        }
    }
}