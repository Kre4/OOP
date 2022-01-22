using System.Dynamic;

namespace IsuExtra.Tools
{
    public static class OgnpIdGenerator
    {
        private static int _id = 0;

        public static int GetId() => _id++;
    }
}