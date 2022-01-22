namespace BackupsExtra.Tools
{
    public static class Extension
    {
        public static string GetFileFolder(this string fullPath)
        {
            int indexOfSlash = fullPath.LastIndexOf('\\');
            return fullPath.Substring(0, indexOfSlash);
        }
    }
}