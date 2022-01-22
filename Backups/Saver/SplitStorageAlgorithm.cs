using System;
using System.Collections;
using System.Collections.Generic;
using Backups.FileSystem;
using Backups.MementoPattern;
using Backups.Tools;

namespace Backups.Saver
{
    public class SplitStorageAlgorithm : IStorageAlgorithm
    {
        public IRestorePoint Save(string fullPath, IEnumerable<string> listOfFilePaths, DateTime creationTime, IFileManager fileManager)
        {
            var point = new RestorePoint(creationTime);
            foreach (string filePath in listOfFilePaths)
            {
                var fileName = FileNameManager.GetFileName(filePath);
                fileManager?.CreateOneBackupFile(filePath, fullPath + "\\" + fileName + ".zip", creationTime);
                point.AddFile(fullPath + "\\" + fileName + ".zip");
            }

            return point;
        }
    }
}