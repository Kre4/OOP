using System.Collections.Generic;
using Backups.FileSystem;
using Backups.MementoPattern;

namespace BackupsExtra.FileManagerExtra
{
    public interface IFileManagerExtra : IFileManager
    {
        void RemoveFiles(IEnumerable<string> paths);
        void CopyFile(string pathFrom, string pathTo);

        void UnzipFile(string pathFrom, string pathTo);

        List<string> GetUnzippedFileNames(string zipPath, string toLocation);

        void WriteJsonConfig(string path, BackupJob backupJob);

        BackupJob ReadJsonConfig(string fullPath);
    }
}