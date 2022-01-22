using System;
using System.Collections.Generic;
using System.Linq;
using Backups.MementoPattern;
using Backups.Saver;
using BackupsExtra.ExtendedRepo;
using BackupsExtra.FileManagerExtra;
using BackupsExtra.LimitHandlers;
using BackupsExtra.Logger;
using BackupsExtra.Strategy;
using BackupsExtra.Tools;
using NUnit.Framework;

namespace BackupsExtra.Tests
{
    public class BackupsExtraTest
    {
        private RepositoryExtra _repository;
        private BackupJob _job;
        [SetUp]
        public void Setup()
        {
            _job = new BackupJob(new SplitStorageAlgorithm(), "root");
            _job.AddFile(".\file1");
            _job.AddFile(".\file2");
        }

        [Test]
        public void TestMergeStrategyMergeTwoPointsWithDifferentFiles()
        {
            MergeStrategy mergeStrategy = new MergeStrategy(new VirtualFileManager(), _job.GetStorageAlgorithm());
            List<IRestorePoint> restorePoints = new List<IRestorePoint>
            {
                new RestorePoint(DateTime.Now, new List<string> {".\\one", ".\\two"}),
                new RestorePoint(DateTime.Now, new List<string>{".\\three"})
            };
            var list = mergeStrategy.Update(restorePoints, 1);
            Assert.AreEqual(list.Count, 1);
            Assert.AreEqual(list[0].GetFileNames().Count(), 3);
        }
        [Test]
        public void TestRemoveStrategyRemovePoint()
        {
            RemoveStrategy removeStrategy = new RemoveStrategy(new VirtualFileManager());
            List<IRestorePoint> restorePoints = new List<IRestorePoint>
            {
                new RestorePoint(DateTime.Now, new List<string> {".\\one", ".\\two"}),
                new RestorePoint(DateTime.Now, new List<string>{".\\three"})
            };
            var list = removeStrategy.Update(restorePoints, 1);
            Assert.AreEqual(list.Count, 1);
            Assert.AreEqual(list[0].GetFileNames().Count(), 2);
        }

        [Test]
        public void CreateRestorePointAndBackupToIt()
        {
            _repository = new RepositoryExtra("\\roo", _job, new VirtualFileManager(), new NoLog(),
                new NoLimitHandler(new NoStrategy()));
            _repository.CreateBackup(DateTime.Now);
            _repository.AddFile("new file which will disappear in the next line");
            _repository.BackUpToLatestVersion(".\\");
            Assert.AreEqual(_repository.GetBackupJob().GetFilePaths().Count(), 2);
        }

        [Test]
        public void CreateTooManyPointsWithNumericLimitHandler()
        {
            _repository = new RepositoryExtra("\\roo", _job, new VirtualFileManager(), new NoLog(),
                new NumericLimitPointHandler(new RemoveStrategy(new VirtualFileManager()),1 ));
            _repository.CreateBackup(DateTime.Now);
            _repository.AddFile("bla_bla");
            _repository.CreateBackup(DateTime.Now);
            Assert.AreEqual(_repository.GetRestorePoints().Count, 1);
        }

        [Test]
        public void DeleteLastBackupWithDateLimitHandler_ThrowException()
        {
            _repository = new RepositoryExtra("\\roo", _job, new VirtualFileManager(), new NoLog(),
                new DateLimitPointHandler(new RemoveStrategy(new VirtualFileManager()), DateTime.Now ));
            Assert.Catch<RestorePointsException>(() =>
            {
                _repository.CreateBackup(DateTime.Now - new TimeSpan(1, 0, 0, 0));
            });
        }

        [Test]
        public void CreateOutOfDatePointWithDateLimitHandler()
        {
            _repository = new RepositoryExtra("\\roo", _job, new VirtualFileManager(), new NoLog(),
                new DateLimitPointHandler(new RemoveStrategy(new VirtualFileManager()), DateTime.Now - TimeSpan.FromDays(1)));
            _repository.CreateBackup(DateTime.Now);
            _repository.CreateBackup(DateTime.Now - TimeSpan.FromDays(34));
            Assert.AreEqual(_repository.GetRestorePoints().Count, 1);
        }

        [Test]
        public void CreatePointToDeleteWithHybridLimitHandler_ModeSuitsAll()
        {
            _repository = new RepositoryExtra("\\roo", _job, new VirtualFileManager(), new NoLog(),
                new HybridLimitPointHandler(
                    new List<RestorePointLimitHandler>
                    {
                        new DateLimitPointHandler(new RemoveStrategy(new VirtualFileManager()), DateTime.Now - TimeSpan.FromDays(1)),
                        new NumericLimitPointHandler(new RemoveStrategy(new VirtualFileManager()), 1)
                    },
                    HybridLimitPointHandler.HybridMode.SuitsAll
                    ));
            _repository.CreateBackup(DateTime.Now);
            _repository.CreateBackup(DateTime.Now - TimeSpan.FromDays(10));
            Assert.AreEqual(_repository.GetRestorePoints().Count, 1);
            
        }
        
        [Test]
        public void CreatePointToDeleteWithHybridLimitHandler_ModeSuitsOne()
        {
            _repository = new RepositoryExtra("\\roo", _job, new VirtualFileManager(), new NoLog(),
                new HybridLimitPointHandler(
                    new List<RestorePointLimitHandler>
                    {
                        new DateLimitPointHandler(new RemoveStrategy(new VirtualFileManager()), DateTime.Now - TimeSpan.FromDays(1)),
                        new NumericLimitPointHandler(new RemoveStrategy(new VirtualFileManager()), 1)
                    },
                    HybridLimitPointHandler.HybridMode.SuitsOne
                ));
            _repository.CreateBackup(DateTime.Now);
            _repository.CreateBackup(DateTime.Now + TimeSpan.FromDays(10));
            Assert.AreEqual(_repository.GetRestorePoints().Count, 1);
            
        }
    }
}