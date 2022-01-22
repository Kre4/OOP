using System;
using System.Collections.Generic;
using Backups.MementoPattern;
using Backups.Repo;
using BackupsExtra.FileManagerExtra;
using BackupsExtra.LimitHandlers;
using BackupsExtra.Logger;
using BackupsExtra.Strategy;
using BackupsExtra.Tools;

namespace BackupsExtra.ExtendedRepo
{
    public class RepositoryExtra
    {
        private Repository _repository;
        private ILogWriter _logWriter;
        private RestorePointLimitHandler _limitHandler;

        public RepositoryExtra(string rootPath, BackupJob backupJob, IFileManagerExtra extraFileManager, ILogWriter logWriter, RestorePointLimitHandler removeAlgorithm = null)
        {
            _repository = new Repository(rootPath, backupJob, extraFileManager);
            _logWriter = logWriter;
            _limitHandler = removeAlgorithm ?? new NoLimitHandler(new NoStrategy());
            logWriter.WriteLog("Repository initialized");
        }

        public void CreateBackup(DateTime creationDate)
        {
            _repository.CreateBackup(creationDate);
            string paths = string.Empty;
            _repository.GetBackupJob().GetFilePaths().ForEach(path => paths += path + "\n");
            _repository.SetRestorePoints(_limitHandler.Update(_repository.GetRestorePoints()));
            _logWriter.WriteLog("Create backup from files: " + paths);
        }

        public void AddRange(List<IRestorePoint> jobDirectories)
        {
            _repository.AddRange(jobDirectories);
        }

        public void AddFile(string filePath)
        {
            _repository.AddFile(filePath);
            _logWriter.WriteLog("Backup job updated: new file added " + filePath);
        }

        public void RemoveFile(string filePath)
        {
            _repository.RemoveFile(filePath);
            _logWriter.WriteLog("Backup job updated: file deleted " + filePath);
        }

        public void BackUpToLatestVersion(string location)
        {
            BackUpToVersion(location, _repository.GetRestorePoints().Count - 1);
        }

        public void BackUpToVersion(string location, int index)
        {
            IRestorePoint restorePoint;
            try
            {
                restorePoint = _repository.GetRestorePoints()[index];
            }
            catch (Exception)
            {
                throw new RestorePointsException("Incorrect restore point index");
            }

            var job = _repository.GetBackupJob();
            List<string> listOfFiles = new List<string>();
            foreach (var path in job.GetFilePaths())
            {
                listOfFiles.Add(path);
            }

            foreach (string filePath in listOfFiles)
            {
                _repository.RemoveFile(filePath);
            }

            var rangeOfNewFileNames = new List<string>();
            var fileManager = _repository.GetFileManager() as IFileManagerExtra;
            foreach (string filepath in restorePoint.GetFileNames())
            {
                rangeOfNewFileNames.AddRange(fileManager.GetUnzippedFileNames(filepath, location));
                fileManager.UnzipFile(filepath, location);
            }

            rangeOfNewFileNames.ForEach(_repository.AddFile);
        }

        public List<IRestorePoint> GetRestorePoints() => _repository.GetRestorePoints();

        public IRestorePoint GetRestorePointByIndex(int index) => _repository.GetRestorePointByIndex(index);

        public BackupJob GetBackupJob() => _repository.GetBackupJob();

        public Repository GetRepository() => _repository;
    }
}