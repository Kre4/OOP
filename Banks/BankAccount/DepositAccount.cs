using System;
using Banks.Tools;

namespace Banks.BankAccount
{
    public class DepositAccount : IAccount
    {
        private double _percent;
        private double _balance;
        private double _addAtTheEndOfMonth = 0;
        private Guid _accountGuid;
        private DateTime _dateOfFullAccess;

        public DepositAccount(double percent, double starterBalance, TimeSpan validationDate)
        {
            _percent = percent;
            _balance = starterBalance;
            _accountGuid = Guid.NewGuid();
            _dateOfFullAccess = DateTime.Now + validationDate;
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
            CheckValidationDate();
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

        private void CheckValidationDate()
        {
            if (DateTime.Now < _dateOfFullAccess)
                throw new BankException("Operation denied due to validation date");
        }
    }
}