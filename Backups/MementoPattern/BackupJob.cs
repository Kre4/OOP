using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Backups.FileSystem;
using Backups.Repo;
using Backups.Saver;
using Backups.Tools;
using Microsoft.VisualBasic;

namespace Backups.MementoPattern
{
    public class BackupJob
    {
        // aka job objects
        private List<string> _filePaths = new List<string>();

        private IStorageAlgorithm _storageAlgorithm;

        private string _storageDirectory;

        public BackupJob(IStorageAlgorithm storageAlgorithm, string storageDirectory)
        {
            _storageAlgorithm = storageAlgorithm;
            _storageDirectory = storageDirectory;
        }

        public IRestorePoint Save(string fullPath, DateTime creationDate, IFileManager fileManager)
        {
            if (_storageAlgorithm == null)
                throw new BackupException("Storage algorithm can't be null");
            return _storageAlgorithm.Save(fullPath, _filePaths, creationDate, fileManager);
        }

        public void AddFile(string filePath)
        {
            _filePaths.Add(filePath);
        }

        public void RemoveFile(string filePath)
        {
            _filePaths.Remove(filePath);
        }

        public List<string> GetFilePaths() => _filePaths;

        public IStorageAlgorithm GetStorageAlgorithm() => _storageAlgorithm;
        public string GetStorageDirectory() => _storageDirectory;
    }
}