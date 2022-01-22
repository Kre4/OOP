using System;
using System.Collections.Generic;
using Banks.ConditionsBankAccount;
using Banks.Tools;

namespace Banks.BankSystem
{
    public class CentralBank
    {
        private static CentralBank _instance;
        private List<Bank> _banks = new List<Bank>();

        private CentralBank() { }

        public static CentralBank CentralBankInstance()
        {
            return _instance ??= new CentralBank();
        }

        public Bank CreateBank(string name, List<IAccountInfo> providedAccounts)
        {
            var bank = new Bank(name, providedAccounts);
            _banks.Add(bank);
            return bank;
        }

        public void NotifyBanksEndOfDay()
        {
            _banks.ForEach(bank => bank.HandleEndOfDay());
        }

        public void NotifyBanksEndOfMonth()
        {
            _banks.ForEach(bank => bank.HandleEndOfMonth());
        }

        public void ProvideMoneyTransfer(
            Bank senderBank,
            Guid senderAccountId,
            Bank receiverBank,
            Guid receiverAccountId,
            double money)
        {
            Transaction senderTransaction = null;
            try
            {
                senderTransaction = senderBank.RemoveMoneyFromAccount(senderAccountId, money);
            }
            catch (Exception)
            {
                throw new BankException("Can't transfer money from sus account");
            }

            try
            {
                receiverBank.DepositMoneyOnAccount(receiverAccountId, money);
            }
            catch (Exception)
            {
                senderBank.CancelTransaction(senderTransaction.GetTransactionId());
                throw new BankException("Can't transfer money to sus account, transaction declined");
            }
        }

        public List<Bank> GetBanks() => _banks;
    }
}