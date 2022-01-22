using System.Collections.Generic;
using Shops.Customer;
using Shops.Goods;
using Shops.ShopStructure;

namespace Shops.Services
{
    public interface IShopManager
    {
        public void ReceiveSupply(Shop shop, List<ProductForSale> supply);
        public void CompleteTransaction(ICustomer customer, Shop shop, Order order);
        public Shop FindShopWithMinimalOrderPrice(Order order);
        public Shop CreateShop(string name, string address);
        public Product RegisterProduct(string name);
        public ProductForSale RegisterProductForSale(Product product, int amount, float price);
        public ProductForSale RegisterProductForSale(string name, int amount, float price);
    }
}