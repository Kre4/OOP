using System.Collections.Generic;
using NUnit.Framework;
using Shops.Goods;

using Shops.Services;
using Shops.ShopStructure;
using Shops.Tools;
using Shops.Customer;

namespace Shops.Tests
{
    public class Tests
    {
        private IShopManager _shopManager;
        [SetUp]
        public void Setup()
        {
            _shopManager = new ShopManager();
        }

        [Test]
        public void CreateShopWithGoods_GoodsCanBeBought()
        {
            Shop shop = _shopManager.CreateShop("Red Rabbit", "Pupkina 17");
            var supply = new List<ProductForSale>();
            for (int i = 0; i < 5; ++i)
            {
                supply.Add(_shopManager.RegisterProductForSale("Banana from county " + i, 3, 4*i));
            }
            _shopManager.ReceiveSupply(shop, supply);
            supply.ForEach(productForSale =>
            {
                if (shop.FindProductForSale(productForSale.GetId()) == null)
                    Assert.Fail();
            });
            Order order = new Order();
            supply.ForEach(productForSale =>
            {
                order.AddProducts(productForSale.GetProduct(), 20);
            });
            Assert.Catch<ProductException>(() =>
            {
                _shopManager.CompleteTransaction(new Customer.Customer("Jora", 1000000), shop, order);
            });
        }

        [Test]
        public void ChangePriceForProduct_PriceHasChanged()
        {
            Shop shop = _shopManager.CreateShop("Red Rabbit", "Pupkina 17");
            var supply = new List<ProductForSale>();
            var productForSale = _shopManager.RegisterProductForSale("Big banana ", 3, 200);
            supply.Add(productForSale);
            _shopManager.ReceiveSupply(shop, supply);
            shop.ChangePriceById(productForSale.GetId(), 300);
            if (shop.FindProductForSale(productForSale.GetId()).Price != 300)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void FindShopWithMinimalOrderPrice()
        {
            Shop shopWithHighPrice = _shopManager.CreateShop("Orange Rabbit", "Pupkina 17");
            Shop shopWithLowPrice = _shopManager.CreateShop("Red Rabbit", "Pupkina 17");
            var product = _shopManager.RegisterProduct("Small tomato");
            var productForSaleHighPrice = _shopManager.RegisterProductForSale(product, 3, 200);
            var productForSaleLowPrice = _shopManager.RegisterProductForSale(product, 3, 150);
            shopWithHighPrice.AddProduct(productForSaleHighPrice);
            shopWithLowPrice.AddProduct(productForSaleLowPrice);
            Order order = new Order();
            order.AddProducts(product, 2);
            if (_shopManager.FindShopWithMinimalOrderPrice(order) == shopWithHighPrice)
                Assert.Fail();
            
        }
        
        [Test]
        public void FindShopWithMinimalOrderPrice_NoGoodsFromOrderInAnyShop()
        {
            Shop shopWithHighPrice = _shopManager.CreateShop("Orange Rabbit", "Pupkina 17");
            Shop shopWithLowPrice = _shopManager.CreateShop("Red Rabbit", "Pupkina 17");
            var product = _shopManager.RegisterProduct("Small tomato");
            var fakeProduct = _shopManager.RegisterProduct("Haha Fake");
            var productForSaleHighPrice = _shopManager.RegisterProductForSale(product, 3, 200);
            var productForSaleLowPrice = _shopManager.RegisterProductForSale(product, 3, 150);
            shopWithHighPrice.AddProduct(productForSaleHighPrice);
            shopWithLowPrice.AddProduct(productForSaleLowPrice);
            Order order = new Order();
            order.AddProducts(fakeProduct, 2);
            Assert.Catch<ShopException>(() =>
            {
                var shop = _shopManager.FindShopWithMinimalOrderPrice(order);
            });
            
            
        }

        [Test]
        public void FindShopWithMinimalOrderPrice_NotEnoughAmountOfProducts()
        {
            Shop shopWithHighPrice = _shopManager.CreateShop("Orange Rabbit", "Pupkina 17");
            Shop shopWithLowPrice = _shopManager.CreateShop("Red Rabbit", "Pupkina 17");
            var product = _shopManager.RegisterProduct("Small tomato");
            var productForSaleHighPrice = _shopManager.RegisterProductForSale(product, 3, 200);
            var productForSaleLowPrice = _shopManager.RegisterProductForSale(product, 3, 150);
            shopWithHighPrice.AddProduct(productForSaleHighPrice);
            shopWithLowPrice.AddProduct(productForSaleLowPrice);
            Order order = new Order();
            order.AddProducts(product, 200000);
            Assert.Catch<ShopException>(() =>
            {
                var shop = _shopManager.FindShopWithMinimalOrderPrice(order);
            });
        }

        [Test]
        public void AmountOfProductsAndBalance_HaveChanged() 
        {
            Shop shop = _shopManager.CreateShop("Punk Rabbit", "Lupkina 18");
            var supply = new List<ProductForSale>();
            for (int i = 0; i < 10; ++i)
            {
                supply.Add(_shopManager.RegisterProductForSale("Energizer "+(i+1)*15 + "V", 10, 150));
            }
            _shopManager.ReceiveSupply(shop, supply);
            Order order = new Order();
            supply.ForEach(product => order.AddProducts(product.GetProduct(), 5));
            ICustomer nigger = new Customer.Customer("Nigger", 7500);
            _shopManager.CompleteTransaction(nigger, shop, order);
            if (nigger.GetBalance() != 0)
                Assert.Fail();
            supply.ForEach(product =>
            {
                if (shop.FindProductForSale(product.GetId()).Amount != 5)
                    Assert.Fail();
            });
        }
    }
}