using System;
using System.Collections.Generic;
using Shops.Goods;
using Shops.Tools;

namespace Shops.ShopStructure
{
    public class Shop
    {
        private readonly List<ProductForSale> _productsForSale = new List<ProductForSale>();
        private string _name;
        private string _address;
        private int _id;

        public Shop(string name, string address)
        {
            _name = name;
            _address = address;
            _id = IdGenerator.GenerateShopId();
        }

        public void AddProduct(ProductForSale productForSale)
        {
            var product = FindProductForSale(productForSale.GetId());
            if (product == null)
            {
                _productsForSale.Add(productForSale);
            }
            else
            {
                product.Price = productForSale.Price;
                product.Amount += productForSale.Amount;
            }
        }

        public void RemoveProductById(int idOfProduct, int amount)
        {
            var product = FindProductForSale(idOfProduct);
            if (product == null)
            {
                throw new IdException("No product with id " + idOfProduct);
            }

            product.Amount -= amount;
        }

        public void ChangePriceById(int idOfProduct, float newPrice)
        {
            var productForSale = FindProductForSale(idOfProduct);
            if (productForSale == null)
            {
                throw new IdException("No product with id " + idOfProduct);
            }

            productForSale.Price = newPrice;
        }

        public ProductForSale FindProductForSale(int id)
        {
            return _productsForSale.Find(product => product.GetId() == id);
        }

        public float GetPriceForOrder(Order order)
        {
            float currentPrice = 0;
            foreach (ProductForSale product in order.GetOrder())
            {
                try
                {
                    ProductForSale productFromShop = FindProductForSale(product.GetId());
                    if (productFromShop == null)
                        throw new IdException("No product with id " + product.GetId());
                    if (product.Amount > productFromShop.Amount)
                        throw new ProductException("Not enough products");
                    currentPrice += productFromShop.Price * product.Amount;
                }
                catch (Exception)
                {
                    currentPrice = float.MaxValue;
                    break;
                }
            }

            return currentPrice;
        }

        public int GetId()
        {
            return _id;
        }
    }
}