using System;
using System.Collections;
using System.Collections.Generic;
using Backups.FileSystem;
using Backups.MementoPattern;

namespace Backups.Saver
{
    public interface IStorageAlgorithm
    {
        IRestorePoint Save(string fullPath, IEnumerable<string> listOfFilePaths, DateTime creationTime, IFileManager fileManager = null);
    }
}