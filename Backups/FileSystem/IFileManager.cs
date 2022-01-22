using System;
using System.Collections;
using System.Collections.Generic;
using Backups.Repo;

namespace Backups.FileSystem
{
    public interface IFileManager
    {
        void CreateOneBackupFile(IEnumerable<string> filePaths, string filePathOut, DateTime creationDate);

        void CreateOneBackupFile(string filePathIn, string filePathOut, DateTime creationDate);
    }
}