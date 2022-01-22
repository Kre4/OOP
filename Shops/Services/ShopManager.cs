using System;
using System.Collections.Generic;
using System.Linq;
using Shops.Customer;
using Shops.Goods;
using Shops.ShopStructure;
using Shops.Tools;

namespace Shops.Services
{
    public class ShopManager : IShopManager
    {
        private List<Shop> _shops = new List<Shop>();

        public void ReceiveSupply(Shop shop, List<ProductForSale> supply)
        {
            var currentShop = FindShopOrThrowException(shop);
            supply.ForEach(currentShop.AddProduct);
        }

        public void CompleteTransaction(ICustomer customer, Shop shop, Order order)
        {
            var currentShop = FindShopOrThrowException(shop);
            if (!IsPossibleToFindAllProductsFromOrderWithAmount(currentShop, order))
            {
                throw new ProductException("Not enough product in shop");
            }

            order.GetOrder().ForEach(product =>
            {
                float price = currentShop.FindProductForSale(product.GetId()).Price;
                currentShop.RemoveProductById(product.GetId(), product.Amount);
                customer.Pay(price * product.Amount);
            });
        }

        public Shop FindShopWithMinimalOrderPrice(Order order)
        {
            var list =
                _shops.Where((x) => x.GetPriceForOrder(order) == _shops.Min(shop => shop.GetPriceForOrder(order)))
                    .ToList();
            if (list.Count is 0 or > 1)
            {
                throw new ShopException("Can't find shop with minimal order price");
            }

            return list[0];
        }

        public Shop CreateShop(string name, string address)
        {
            var shop = new Shop(name, address);
            _shops.Add(shop);
            return shop;
        }

        public Product RegisterProduct(string name)
        {
            return new Product(name);
        }

        public ProductForSale RegisterProductForSale(Product product, int amount, float price)
        {
            return new ProductForSale(product, amount, price);
        }

        public ProductForSale RegisterProductForSale(string name, int amount, float price)
        {
            return new ProductForSale(name, amount, price);
        }

        private bool IsPossibleToFindAllProductsFromOrderWithAmount(Shop shop, Order order)
        {
            bool isPossible = true;
            order.GetOrder().ForEach(product =>
            {
                var productForSale = shop.FindProductForSale(product.GetId());
                if (productForSale == null || product.Amount > productForSale.Amount)
                {
                    isPossible = false;
                    return;
                }
            });
            return isPossible;
        }

        private Shop FindShopOrThrowException(Shop shop)
        {
            var resultShop = _shops.Find(currentShop => currentShop.GetId() == shop.GetId());
            if (resultShop == null)
                throw new ShopException("There is no registered shop with id " + shop.GetId());
            return resultShop;
        }
    }
}