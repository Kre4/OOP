using System;
using System.Collections.Generic;
using Banks.BankAccount;
using Banks.DTO;
using Banks.Tools;

namespace Banks.BankClient
{
    public class Client
    {
        private FullName _fullName = null;
        private List<IAccount> _accounts = new List<IAccount>();
        private string _address;
        private PassportData _passportData = null;
        private bool _isSubscribed = false;
        private Client() { }

        public FullName GetFullName() => _fullName.DeepCopy();
        public List<IAccount> GetAccount() => _accounts;
        public string GetAddress() => _address;
        public PassportData GetPassportData() => _passportData.DeepCopy();
        public bool IsSubscribedToNotifications() => _isSubscribed;
        public void SubscribeToNotification() => _isSubscribed = true;

        public IAccount GetAccountById(Guid accountId)
        {
            return _accounts.Find(account => account.GetGuid() == accountId);
        }

        public bool IsSuspicious()
        {
            return (_address == string.Empty) || (_passportData == null);
        }

        public void HandleEndOfDay()
        {
            _accounts.ForEach(account => account.HandleCommissionEndOfDay());
        }

        public void HandleEndOfMonth()
        {
            _accounts.ForEach(account => account.HandleCommissionEndOfMonth());
        }

        private void SetFullName(FullName fullName) => _fullName = fullName.DeepCopy();
        private void AddAccount(IAccount account) => _accounts.Add(account);

        private void RemoveAccountById(Guid guid) =>
            _accounts.RemoveAt(_accounts.FindIndex(account => account.GetGuid() == guid));

        private void SetAddress(string address) => _address = address;
        private void SetPassportData(PassportData passportData) => _passportData = passportData.DeepCopy();

        public class ClientBuilder
        {
            private Client _client;

            public ClientBuilder BuildFullName(FullName fullName)
            {
                if (fullName != null)
                {
                    _client = new Client();
                }

                try
                {
                    _client.SetFullName(fullName);
                }
                catch (NullReferenceException)
                {
                    throw new BankException("Full name can't be null");
                }

                return this;
            }

            public ClientBuilder BuildAccount(IAccount account)
            {
                if (account != null)
                {
                    DoIfFullNameIsSetted(() =>
                    {
                        _client.AddAccount(account);
                    });
                }

                return this;
            }

            public ClientBuilder BuildAddress(string address)
            {
                DoIfFullNameIsSetted(() =>
                {
                    _client.SetAddress(address);
                });
                return this;
            }

            public ClientBuilder BuildPassportData(PassportData passportData)
            {
                if (passportData != null)
                {
                    DoIfFullNameIsSetted(() =>
                    {
                        _client.SetPassportData(passportData);
                    });
                }

                return this;
            }

            public Client GetInstance()
            {
                if (_client == null)
                {
                    throw new BankException("Client's full name was not initialized");
                }

                return _client;
            }

            private void DoIfFullNameIsSetted(Action action)
            {
                try
                {
                    action();
                }
                catch (Exception)
                {
                    throw new BankException("Client's full name was not initialized");
                }
            }
        }
    }
}