using Banks.BankAccount;
using Banks.BankClient;

namespace Banks.DTO
{
    public class RegistrationData
    {
        private FullName _fullName;
        private PassportData _passportData;
        private string _address;
        private IAccount _accountType;

        public RegistrationData(FullName fullName, PassportData passportData, string address, IAccount accountType)
        {
            _fullName = fullName;
            _passportData = passportData;
            _address = address;
            _accountType = accountType;
        }

        public FullName GetFullName() => _fullName;
        public PassportData GetPassportData() => _passportData;
        public string GetAddress() => _address;
        public IAccount GetAccountType() => _accountType;
    }
}