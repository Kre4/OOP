using System;
using System.Collections.Generic;
using Backups.MementoPattern;
using Backups.Tools;

namespace BackupsExtra.FileManagerExtra
{
    public class VirtualFileManager : IFileManagerExtra
    {
        public void CreateOneBackupFile(IEnumerable<string> filePaths, string filePathOut, DateTime creationDate)
        {
        }

        public void CreateOneBackupFile(string filePathIn, string filePathOut, DateTime creationDate)
        {
        }

        public void RemoveFiles(IEnumerable<string> paths)
        {
        }

        public void CopyFile(string pathFrom, string pathTo)
        {
        }

        public void UnzipFile(string pathFrom, string pathTo)
        {
        }

        public List<string> GetUnzippedFileNames(string zipPath, string toLocation)
        {
            return new List<string> { zipPath };
        }

        public void WriteJsonConfig(string path, BackupJob backupJob)
        {
        }

        public BackupJob ReadJsonConfig(string fullPath)
        {
            throw new BackupException("Can't read config file using virtual file manager");
        }
    }
}