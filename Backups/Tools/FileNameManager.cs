using System;
using Backups.Saver;

namespace Backups.Tools
{
    public static class FileNameManager
    {
        public static string CreateFileNameForBackupDirectory(string rootDirectory, int backupVersion)
        {
            return rootDirectory + "\\Backup " + backupVersion;
        }

        public static string GetFileName(string fullPath)
        {
            int lastIndexOf = fullPath.LastIndexOf('\\');
            return fullPath.Substring(lastIndexOf + 1);
        }
    }
}