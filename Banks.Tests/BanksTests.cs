using System;
using System.Collections.Generic;
using System.IO;
using Banks.BankAccount;
using Banks.BankClient;
using Banks.BankSystem;
using Banks.ConditionsBankAccount;
using Banks.DTO;
using Banks.Tools;
using NUnit.Framework;

namespace Banks.Tests
{
    
    public class BanksTests
    {
        private List<IAccountInfo> _providedAccounts;
        [SetUp]
        public void SetUp()
        {
            _providedAccounts = new List<IAccountInfo>();
            _providedAccounts.Add(new CreditAccountInfo(100000, 10));

            var depositRanges = new List<DepositRange>();
            depositRanges.Add(new DepositRange(0, 100000, 3));
            _providedAccounts.Add(new DepositAccountInfo(depositRanges, new TimeSpan(100, 0, 0, 0)));
            
            _providedAccounts.Add(new DebitAccountInfo(3.65));
        }

        [Test]
        public void CreateNewBank_BankCreated()
        {
            var bank = CentralBank.CentralBankInstance().CreateBank("TinkOn bank", _providedAccounts);
            Assert.AreEqual(bank.GetProvidedAccounts().Count, 3);
        }

        [Test]
        public void RegisterNewCorrectClient_ClientIsCorrect()
        {
            var bank = CentralBank.CentralBankInstance().CreateBank("TinkOn bank", _providedAccounts);
            var account = _providedAccounts[0].GenerateBankAccount(100);
            var client = bank.RegisterNewClient(GenerateRegistrationData(false, true, account));
            Assert.AreEqual(client.IsSuspicious(), false);
        }

        [Test]
        public void RegisterSuspiciousClientAndRemoveMoney_ThrowException()
        {
            var bank = CentralBank.CentralBankInstance().CreateBank("TinkOn bank", _providedAccounts);
            var account = _providedAccounts[0].GenerateBankAccount(100);
            var client = bank.RegisterNewClient(GenerateRegistrationData(true, true, account));
            Assert.Catch<BankException>(() =>
            {
                bank.RemoveMoneyFromAccount(account.GetGuid(), 200);
            });
        }

        [Test]
        public void RegisterWithoutFullName_ThrowException()
        {
            var bank = CentralBank.CentralBankInstance().CreateBank("TinkOn bank", _providedAccounts);
            var account = _providedAccounts[0].GenerateBankAccount(100);
            Assert.Catch<BankException>(() =>
            {
                 var client = bank.RegisterNewClient(GenerateRegistrationData(true, false, account));
            });
        }

        [Test]
        public void CreateCorrectDebitAccountRemoveMoneyMoreThatOnBalance_ThrowException()
        {
            var bank = CentralBank.CentralBankInstance().CreateBank("TinkOn bank", _providedAccounts);
            var account = _providedAccounts[2].GenerateBankAccount(100);
            var client = bank.RegisterNewClient(GenerateRegistrationData(true, true, account));
            Assert.Catch<BankException>(() =>
            {
                 account.RemoveMoney(300);
            });
        }

        [Test]
        public void CheckMathLogicEmulateMonthWithOneDay_BalanceIsCorrect()
        {
            var bank = CentralBank.CentralBankInstance().CreateBank("TinkOn bank", _providedAccounts);
            var account = _providedAccounts[2].GenerateBankAccount( 100000);
            var client = bank.RegisterNewClient(GenerateRegistrationData(true, true, account));
            CentralBank.CentralBankInstance().NotifyBanksEndOfDay();
            CentralBank.CentralBankInstance().NotifyBanksEndOfMonth();
            Assert.AreEqual(account.GetMoney(), 100010.0);
            
        }

        private RegistrationData GenerateRegistrationData(bool shouldBeSuspicious, bool canBeCreated, IAccount account)
        {
            FullName fullName = canBeCreated ? new FullName("Adolf", "Heinz"): null;
            PassportData passportData = shouldBeSuspicious ? null : new PassportData("1000", "123456");
            return new RegistrationData(fullName, passportData, "Blabla str. 17", account);
        }
    }
}