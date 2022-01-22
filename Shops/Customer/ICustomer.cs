namespace Shops.Customer
{
    public interface ICustomer
    {
        public void Pay(float money);
        public float GetBalance();
    }
}