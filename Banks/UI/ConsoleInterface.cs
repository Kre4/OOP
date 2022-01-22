using System;
using System.Collections.Generic;
using Banks.BankAccount;
using Banks.BankClient;
using Banks.BankSystem;
using Banks.ConditionsBankAccount;
using Banks.DTO;

namespace Banks.UI
{
    public class ConsoleInterface : IUserInterface
    {
        public ConsoleInterface() { }

        public void ShowMainMenu()
        {
            Console.WriteLine("Greetings, buddy, it's online bank application v2.3\nFunctions:\n 1) Register new Account\n2) Operation with existing account");
            int answer = RequestIntChoice(2);
            switch (answer)
            {
                case 1:
                    RequestRegistrationData();
                    break;
                case 2:
                    TransferMoney();
                    break;
            }
        }

        public RegistrationData RequestRegistrationData()
        {
            int bankIndex = ShowBankNames_RequestChoice();
            var account =
                RequestAccountType(CentralBank.CentralBankInstance().GetBanks()[bankIndex].GetProvidedAccounts());
            return new RegistrationData(
                RequestFullName(),
                RequestPassportData(),
                RequestAddress(),
                account);
        }

        public void TransferMoney()
        {
            Console.WriteLine("Your bank:\n");
            List<Bank> banks = CentralBank.CentralBankInstance().GetBanks();
            Bank senderBank = banks[ShowBankNames_RequestChoice()];
            Guid senderGuid = Guid.Parse(RequestStringChoice("Enter your account guid"));

            Console.WriteLine("Receiver bank:\n");
            Bank receiverBank = banks[ShowBankNames_RequestChoice()];
            Guid receiverGuid = Guid.Parse(RequestStringChoice("Enter receiver guid"));

            CentralBank.CentralBankInstance().ProvideMoneyTransfer(senderBank, senderGuid, receiverBank, receiverGuid, RequestIntChoice(int.MaxValue));
        }

        private int ShowBankNames_RequestChoice()
        {
            string bankNamesList = string.Empty;
            List<Bank> banksList = CentralBank.CentralBankInstance().GetBanks();
            for (int i = 0; i < banksList.Count; ++i)
            {
                bankNamesList += i + ") " + banksList[i].GetName() + "\n";
            }

            Console.WriteLine("Available banks:\n" + bankNamesList + "Choose one: ");
            return RequestIntChoice(banksList.Count);
        }

        private int ShowBankProvidedAccounts_RequestChoice(Bank bank)
        {
            var providedAccounts = bank.GetProvidedAccounts();
            string allInfo = string.Empty;
            for (int i = 0; i < providedAccounts.Count; ++i)
            {
                allInfo += i + ")" + GetAccountInfo(providedAccounts[i]) + "\n";
            }

            Console.WriteLine(allInfo);
            return RequestIntChoice(providedAccounts.Count);
        }

        private FullName RequestFullName()
        {
            return new FullName(RequestStringChoice("Enter your name: "), RequestStringChoice("Enter your surname: "));
        }

        private string RequestAddress()
        {
            return RequestStringChoice("Enter your address");
        }

        private PassportData RequestPassportData()
        {
            return new PassportData(
                RequestStringChoice("Enter your passport's serial number"),
                RequestStringChoice("Enter your passport's number"));
        }

        private IAccount RequestAccountType(List<IAccountInfo> providedAccounts)
        {
            Console.WriteLine("Choose\n");
            for (int i = 0; i < providedAccounts.Count; ++i)
            {
                Console.WriteLine(i + ") " + GetAccountInfo(providedAccounts[i]) + "\n");
            }

            int answer = RequestIntChoice(providedAccounts.Count);
            Console.WriteLine("How much would you like to put on your balance?");
            int starterBalance = int.Parse(Console.ReadLine() ?? "0");
            return providedAccounts[answer - 1].GenerateBankAccount(starterBalance);
        }

        private int RequestIntChoice(int maxDigitOfChoice)
        {
            int answer = -1;
            while (answer == -1 || answer < 1 || answer > maxDigitOfChoice)
            {
                Console.WriteLine("Type digit from " + 1 + " to " + maxDigitOfChoice);
                answer = int.Parse(Console.ReadLine() ?? "-1");
            }

            return answer;
        }

        private string RequestStringChoice(string message)
        {
            string answer = string.Empty;
            while (answer == string.Empty)
            {
                Console.WriteLine(message);
                answer = Console.ReadLine() ?? string.Empty;
            }

            return answer;
        }

        private string GetAccountInfo(IAccountInfo account)
        {
            switch (account)
            {
                case DebitAccountInfo debitAccountInfo:
                    return "Debit account with " + debitAccountInfo.GetPercent() + "% per month";
                case CreditAccountInfo creditAccountInfo:
                    return "Account balance can be from " + -creditAccountInfo.GetLimit() + " to " + creditAccountInfo.GetLimit() +
                           ".\n If balance is below zero the fine is: " + creditAccountInfo.GetNegativeBlanceFine();
                case DepositAccountInfo depositAccountInfo:
                    string result = "Deposit account:\n";
                    depositAccountInfo.GetDepositRanges().ForEach(deposit =>
                    {
                        result += "* " + deposit.GetStartOfRange() + " - " + deposit.GetEndOfRange() + " with " +
                                  deposit.GetPercent() + ";\n";
                    });
                    return result;
            }

            return string.Empty;
        }
    }
}