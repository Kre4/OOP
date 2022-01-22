using System;
using System.Collections.Generic;
using Backups.FileSystem;
using Backups.MementoPattern;
using Backups.Tools;

namespace Backups.Saver
{
    public class SingleStorageAlgorithm : IStorageAlgorithm
    {
        public IRestorePoint Save(string fullPath, IEnumerable<string> listOfFilePaths, DateTime creationTime, IFileManager fileManager = null)
        {
            var point = new RestorePoint(creationTime);
            fileManager?.CreateOneBackupFile(listOfFilePaths, fullPath + "\\folder.zip", creationTime);
            point.AddFile(fullPath + "\\folder.zip");

            return point;
        }
    }
}