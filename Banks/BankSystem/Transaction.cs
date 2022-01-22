using System;

namespace Banks.BankSystem
{
    public class Transaction
    {
        private Guid _transactionId;
        private double _sumOfChanges;
        private Guid _accountId;
        private string _infoMessage;

        public Transaction(Guid transactionId, double sumOfChanges, string infoMessage = "")
        {
            _transactionId = Guid.NewGuid();
            _sumOfChanges = sumOfChanges;
            _accountId = transactionId;
            _infoMessage = infoMessage;
        }

        public Guid GetTransactionId() => _transactionId;
        public double GetSumOfChanges() => _sumOfChanges;
        public Guid GetReceiverId() => _accountId;
        public string GetMessage() => _infoMessage;
    }
}