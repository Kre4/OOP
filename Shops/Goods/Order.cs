using System;
using System.Collections.Generic;

namespace Shops.Goods
{
    public class Order
    {
        private List<ProductForSale> _order = new List<ProductForSale>();
        public Order() { }

        public void AddProducts(Product productForSale, int amount)
        {
            _order.Add(new ProductForSale(new Product(productForSale), amount, 0));
        }

        public List<ProductForSale> GetOrder()
        {
            return _order;
        }
    }
}