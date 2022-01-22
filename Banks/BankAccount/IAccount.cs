using System;

namespace Banks.BankAccount
{
    public interface IAccount
    {
        Guid GetGuid();

        double GetMoney();
        void HandleCommissionEndOfDay();
        void HandleCommissionEndOfMonth();

        void RemoveMoney(double amount);
        void DepositMoney(double amount);
        void Cancel(double amount);
    }
}