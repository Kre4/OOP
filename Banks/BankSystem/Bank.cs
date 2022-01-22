using System;
using System.Collections.Generic;
using Banks.BankAccount;
using Banks.BankClient;
using Banks.ConditionsBankAccount;
using Banks.DTO;
using Banks.Tools;
using Banks.UI;

namespace Banks.BankSystem
{
    public class Bank
    {
        private string _bankName;
        private List<Client> _clients = new List<Client>();
        private List<IAccountInfo> _providedAccounts;
        private List<Transaction> _transactions = new List<Transaction>();

        public Bank(string bankName, List<IAccountInfo> providedAccounts = null)
        {
            _bankName = bankName;
            _providedAccounts = providedAccounts ?? new List<IAccountInfo>();
        }

        public void ChangeProvidedAccount(IAccountInfo oldProvidedAccountInfo, IAccountInfo newProvidedAccountInfo)
        {
            _providedAccounts.RemoveAt(_providedAccounts.FindIndex(account =>
                account.GetName() == oldProvidedAccountInfo.GetName()));
            _providedAccounts.Add(newProvidedAccountInfo);
        }

        public Transaction RemoveMoneyFromAccount(Guid accountId, double money)
        {
            var transaction = new Transaction(accountId, -money);
            var account = FindAccountById(accountId);
            if (account != null)
            {
                account.RemoveMoney(money);
                _transactions.Add(transaction);
            }
            else
            {
                throw new BankException("No account, or suspicious account with id" + accountId);
            }

            return transaction;
        }

        public Transaction DepositMoneyOnAccount(Guid accountId, double money)
        {
            return RemoveMoneyFromAccount(accountId, -money);
        }

        public void RequestForMoneyTransfer(Guid senderAccountId, Bank receiverBank, Guid receiverAccountId, double money)
        {
            CentralBank.CentralBankInstance()
                .ProvideMoneyTransfer(this, senderAccountId, receiverBank, receiverAccountId, money);
        }

        public void CancelTransaction(Guid transactionId)
        {
            var transactionToCancel =
                _transactions.Find(transaction => transaction.GetTransactionId() == transactionId);
            if (transactionToCancel != null)
            {
                FindAccountById(transactionToCancel.GetReceiverId())?.Cancel(-transactionToCancel.GetSumOfChanges());
            }
        }

        public Client RegisterNewClient(RegistrationData registrationData)
        {
            Client newClient = new Client.ClientBuilder()
                .BuildFullName(registrationData.GetFullName())
                .BuildPassportData(registrationData.GetPassportData())
                .BuildAddress(registrationData.GetAddress())
                .BuildAccount(registrationData.GetAccountType())
                .GetInstance();
            _clients.Add(newClient);
            return newClient;
        }

        public void HandleEndOfDay()
        {
            _clients.ForEach(client => client.HandleEndOfDay());
        }

        public void HandleEndOfMonth()
        {
            _clients.ForEach(client => client.HandleEndOfMonth());
        }

        public List<IAccountInfo> GetProvidedAccounts() => _providedAccounts;
        public string GetName() => _bankName;

        private IAccount FindAccountById(Guid accountId)
        {
            foreach (var client in _clients)
            {
                var account = client.GetAccountById(accountId);
                if (account.GetGuid() == accountId)
                {
                    return client.IsSuspicious() ? null : account;
                }
            }

            return null;
        }
    }
}