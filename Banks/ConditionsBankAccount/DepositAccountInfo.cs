using System;
using System.Collections.Generic;
using Banks.BankAccount;
using Banks.Tools;

namespace Banks.ConditionsBankAccount
{
    public class DepositAccountInfo : IAccountInfo
    {
        private List<DepositRange> _depositRanges;
        private TimeSpan _accountValidityTime;

        public DepositAccountInfo(List<DepositRange> depositRanges, TimeSpan accountValidityTime)
        {
            _depositRanges = depositRanges;
            _accountValidityTime = accountValidityTime;
        }

        public string GetName()
        {
            return "DepositAccount" + _depositRanges.Count + _accountValidityTime;
        }

        public IAccount GenerateBankAccount(double starterBalance)
        {
            return new DepositAccount(GetRangeOfSum(starterBalance).GetPercent(), starterBalance, _accountValidityTime);
        }

        public List<DepositRange> GetDepositRanges() => _depositRanges;
        public TimeSpan GetAccountValidityTime() => _accountValidityTime;

        private DepositRange GetRangeOfSum(double sum)
        {
            foreach (var depositRange in _depositRanges)
            {
                if (depositRange.IsInDepositRange(sum))
                    return depositRange;
            }

            var max = _depositRanges[0];
            foreach (var depositRange in _depositRanges)
            {
                if (depositRange.GetEndOfRange() > max.GetEndOfRange())
                    max = depositRange;
            }

            return max;
        }
    }
}