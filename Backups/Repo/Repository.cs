using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Backups.FileSystem;
using Backups.MementoPattern;
using Backups.PointsSearchAlgorithm;
using Backups.Saver;
using Backups.Tools;

namespace Backups.Repo
{
    public class Repository
    {
        private BackupJob _backupJob;
        private string _root;
        private List<IRestorePoint> _jobDirectories = new List<IRestorePoint>();
        private IFileManager _fileManager;

        public Repository(string rootPath, BackupJob backupJob, IFileManager fileManager)
        {
            _root = rootPath;
            _fileManager = fileManager;
            _backupJob = backupJob;
        }

        public Repository(string rootPath, BackupJob backupJob, ISearchPointsAlgorithm searchPointsAlgorithm)
        {
            _root = rootPath;
            _backupJob = backupJob;
            if (searchPointsAlgorithm != null)
                   _jobDirectories = searchPointsAlgorithm.FindRestorePoints(rootPath);
        }

        public void CreateBackup(DateTime creationDate)
        {
            var path = FileNameManager.CreateFileNameForBackupDirectory(_root, _jobDirectories.Count);
            var jobDirectory = _backupJob.Save(path, creationDate, _fileManager);

            AddJobDirectory(jobDirectory);
        }

        public void AddRange(List<IRestorePoint> jobDirectories)
        {
            jobDirectories.ForEach(AddJobDirectory);
        }

        public void AddFile(string filePath)
        {
            _backupJob.AddFile(filePath);
        }

        public void RemoveFile(string filePath)
        {
            _backupJob.RemoveFile(filePath);
        }

        public List<IRestorePoint> GetRestorePoints() => _jobDirectories;

        public void SetRestorePoints(IEnumerable<IRestorePoint> restorePoints)
        {
            _jobDirectories = restorePoints.ToList();
        }

        public IRestorePoint GetRestorePointByIndex(int index)
        {
            try
            {
                return _jobDirectories[index];
            }
            catch (Exception)
            {
                throw new BackupException("Out of range restore points");
            }
        }

        public BackupJob GetBackupJob() => _backupJob;

        public IFileManager GetFileManager() => _fileManager;

        private void AddJobDirectory(IRestorePoint jobDirectory)
        {
            _jobDirectories.Add(jobDirectory);
        }
    }
}