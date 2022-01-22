namespace Shops.Goods
{
    public class ProductForSale
    {
        private Product _product;

        public ProductForSale(Product product, int amount, float price)
        {
            _product = product;
            Amount = amount;
            Price = price;
        }

        public ProductForSale(string name, int amount, float price)
            : this(new Product(name), amount, price)
        {
        }

        public int Amount { get; set; }
        public float Price { get; set; }

        public int GetId()
        {
            return _product.GetId();
        }

        public Product GetProduct()
        {
            return _product;
        }
    }
}