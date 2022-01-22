namespace Shops.Tools
{
    public static class IdGenerator
    {
        private static int _shopId = 0;
        private static int _productId = 0;

        public static int GenerateShopId()
        {
            return _shopId++;
        }

        public static int GenerateProductId()
        {
            return _productId++;
        }
    }
}