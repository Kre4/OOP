using System;
using Banks.Tools;

namespace Banks.BankAccount
{
    public class CreditAccount : IAccount
    {
        private double _limit;
        private double _fineOnNegativeBalance;
        private double _balance;
        private Guid _accountGuid;

        public CreditAccount(double limit, double fineOnNegativeBalance, double starterBalance)
        {
            _limit = limit;
            _fineOnNegativeBalance = fineOnNegativeBalance;
            _balance = starterBalance;
            _accountGuid = Guid.NewGuid();
        }

        public Guid GetGuid() => _accountGuid;
        public double GetMoney() => _balance;

        public void HandleCommissionEndOfDay()
        {
            if (_balance > 0)
            {
                return;
            }

            if (IsInLimitRange(_balance - _fineOnNegativeBalance))
            {
                _balance -= _fineOnNegativeBalance;
            }
        }

        public void HandleCommissionEndOfMonth()
        {
            // empty because nothing was said about commission in the end of the month in task;
        }

        public void RemoveMoney(double amount)
        {
            if (!IsInLimitRange(_balance - amount))
                throw new BankException("Transaction will lead to over-limit balance (Guid: " + _accountGuid + " )");
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

        private bool IsInLimitRange(double sum) => Math.Abs(sum) <= Math.Abs(_limit);
    }
}