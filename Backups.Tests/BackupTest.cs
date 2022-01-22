using System;
using System.Collections.Generic;
using System.Linq;
using Backups.MementoPattern;
using Backups.Repo;
using Backups.Saver;
using Backups.Tools;
using NUnit.Framework;

namespace Backups.Tests
{
    public class Tests
    {
        private BackupJob _backupJob;
        private Repository _repository;

        [SetUp]
        public void Setup()
        {
            InitRepository(new SplitStorageAlgorithm());
        }

        [Test]
        public void SplitStorage_CheckIfFirstRestorePointContainsTwoFiles()
        {
            Assert.AreEqual(_repository.GetRestorePoints().Count, 1);
            Assert.AreEqual(_repository.GetRestorePoints()[0].GetFileNames().Count(), 2);
        }

        [Test]
        public void SplitStorage_DeleteFileCheckSecondRestorePointContainsOneFile()
        {
            _repository.RemoveFile("filePath1");
            _repository.CreateBackup(DateTime.Now);
            Assert.AreEqual(_repository.GetRestorePoints().Count, 2);
            var list = _repository.GetRestorePoints()[1].GetFileNames();
            Assert.AreEqual(list.Count(), 1);
           foreach (string s in list)
           {
               Assert.AreEqual(FileNameManager.GetFileName(s), "filePath2.zip");
           }
        }

        [Test]
        public void SingleStorage_CheckIfFirstRestorePointContainsOneFile()
        {
            InitRepository(new SingleStorageAlgorithm());
            Assert.AreEqual(_repository.GetRestorePoints().Count, 1);
            Assert.AreEqual(_repository.GetRestorePoints()[0].GetFileNames().Count(), 1);
        }
        
        [Test]
        public void SingleStorage_DeleteFileCheckSecondRestorePointContainsOneFile()
        {
            InitRepository(new SingleStorageAlgorithm());
            _repository.RemoveFile("filePath1");
            _repository.CreateBackup(DateTime.Now);
            Assert.AreEqual(_repository.GetRestorePoints().Count, 2);
            
        }

        

        private void InitRepository(IStorageAlgorithm storageAlgorithm)
        {
            string rootDirectory = "rootDirectory";
            _backupJob = new BackupJob(storageAlgorithm, rootDirectory);
            _backupJob.AddFile("filePath1");
            _backupJob.AddFile("filePath2");
            _repository = new Repository("root", _backupJob, fileManager: null);
            _repository.CreateBackup(DateTime.Now);
        }
    }
}