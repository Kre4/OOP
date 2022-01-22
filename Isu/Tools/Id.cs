namespace Isu.Tools
{
    public static class Id
    {
        private static int _id = 100000;

        public static int GetId()
        {
            int id = _id;
            _id++;
            return id;
        }
    }
}