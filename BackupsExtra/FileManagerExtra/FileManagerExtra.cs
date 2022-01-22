using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text.Json;
using Backups.MementoPattern;
using BackupsExtra.Tools;

namespace BackupsExtra.FileManagerExtra
{
    public class FileManagerExtra : IFileManagerExtra
    {
        public void CreateOneBackupFile(IEnumerable<string> filePaths, string filePathOut, DateTime creationDate)
        {
            new FileManagerExtra().CreateOneBackupFile(filePaths, filePathOut, creationDate);
        }

        public void CreateOneBackupFile(string filePathIn, string filePathOut, DateTime creationDate)
        {
            new FileManagerExtra().CreateOneBackupFile(filePathIn, filePathOut, creationDate);
        }

        public void RemoveFiles(IEnumerable<string> paths)
        {
            foreach (string path in paths)
            {
                File.Delete(path);
            }
        }

        public void CopyFile(string pathFrom, string pathTo)
        {
            File.Copy(pathFrom, pathTo, true);
        }

        public void UnzipFile(string pathFrom, string pathTo)
        {
            ZipFile.ExtractToDirectory(pathFrom, pathTo);
        }

        public List<string> GetUnzippedFileNames(string zipPath, string toLocation)
        {
            var list = new List<string>();
            using ZipArchive archive = ZipFile.OpenRead(zipPath);
            foreach (ZipArchiveEntry entry in archive.Entries)
            {
                list.Add(toLocation + entry.FullName);
            }

            return list;
        }

        public void WriteJsonConfig(string path, BackupJob backupJob)
        {
            JSONBackupJob jsonJob = JSONBackupJob.CreateJsonBackupJob(backupJob);
            string jsonString = JsonSerializer.Serialize(jsonJob);
            File.WriteAllText(path, jsonString);
        }

        public BackupJob ReadJsonConfig(string fullPath)
        {
            string jsonString = File.ReadAllText(fullPath);
            return JsonSerializer.Deserialize<JSONBackupJob>(jsonString).ToBackupJob();
        }
    }
}