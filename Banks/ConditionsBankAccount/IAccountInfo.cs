using Banks.BankAccount;

namespace Banks.ConditionsBankAccount
{
    public interface IAccountInfo
    {
        string GetName();
        IAccount GenerateBankAccount(double starterBalance);
    }
}