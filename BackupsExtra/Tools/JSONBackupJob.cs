using System;
using System.Linq;
using Backups.MementoPattern;
using Backups.Saver;
using Backups.Tools;

namespace BackupsExtra.Tools
{
    [Serializable]
    public class JSONBackupJob
    {
        public string[] FilePaths { get; set; }

        public int StorageAlgorithm { get; set; }

        public string StorageDirectory { get; set; }

        public static JSONBackupJob CreateJsonBackupJob(BackupJob backupJob)
        {
            var job = new JSONBackupJob();
            job.FilePaths = backupJob.GetFilePaths().ToArray();
            job.StorageDirectory = backupJob.GetStorageDirectory();
            var algorithm = backupJob.GetStorageAlgorithm();
            switch (algorithm)
            {
                case SingleStorageAlgorithm algo:
                    job.StorageAlgorithm = 0;
                    break;
                case SplitStorageAlgorithm algo:
                    job.StorageAlgorithm = 1;
                    break;
            }

            return job;
        }

        public BackupJob ToBackupJob()
        {
            IStorageAlgorithm algorithm;
            switch (StorageAlgorithm)
            {
                case 0:
                    algorithm = new SingleStorageAlgorithm();
                    break;
                case 1:
                    algorithm = new SplitStorageAlgorithm();
                    break;
                default:
                    throw new BackupException("Incorrect config file");
            }

            var job = new BackupJob(algorithm, StorageDirectory);
            foreach (string filePath in FilePaths)
            {
                job.AddFile(filePath);
            }

            return job;
        }
    }
}