using Shops.Tools;

namespace Shops.Goods
{
    public class Product
    {
        private int _id;
        private string _name;

        public Product(string productName)
        {
            BaseRegistration(productName);
        }

        public Product(Product product)
        {
            _id = product.GetId();
            _name = product.GetName();
        }

        public int GetId()
        {
            return _id;
        }

        public string GetName()
        {
            return _name;
        }

        private void BaseRegistration(string productName)
        {
            _name = productName;
            _id = IdGenerator.GenerateProductId();
        }
    }
}