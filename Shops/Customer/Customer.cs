using Shops.Tools;

namespace Shops.Customer
{
    public class Customer : ICustomer
    {
        private string _name;
        private float _balance;

        public Customer(string name, float starterBalance)
        {
            _name = name;
            SetCorrectBalanceOrThrowException(starterBalance);
        }

        public void Pay(float money)
        {
            SetCorrectBalanceOrThrowException(_balance - money);
        }

        public float GetBalance()
        {
            return _balance;
        }

        private void SetCorrectBalanceOrThrowException(float balance)
        {
            if (balance < 0)
            {
                throw new MoneyException("Balance is below zero");
            }

            _balance = balance;
        }
    }
}