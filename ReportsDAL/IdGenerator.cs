namespace ReportsDAL
{
    internal static class IdGenerator
    {
        private static uint _id = 1;

        public static uint GetNewId() => _id++;

    }
}