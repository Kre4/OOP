using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using Backups.Tools;

namespace Backups.FileSystem
{
    public class FileManager : IFileManager
    {
        public void CreateOneBackupFile(IEnumerable<string> filePaths, string filePathOut, DateTime creationDate)
        {
            string tempDirectory = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Directory.CreateDirectory(tempDirectory);
            foreach (string filePath in filePaths)
            {
                File.Copy(filePath, tempDirectory);
            }

            ZipFile.CreateFromDirectory(tempDirectory, filePathOut);
            SetCreationDate(filePathOut, creationDate);
            Directory.Delete(tempDirectory, true);
        }

        public void CreateOneBackupFile(string filePathIn, string filePathOut, DateTime creationDate)
        {
            using var fileStream = new FileStream(filePathOut, FileMode.Create);
            using (var zipArchive = new ZipArchive(fileStream, ZipArchiveMode.Create))
            {
                zipArchive.CreateEntryFromFile(filePathIn, FileNameManager.GetFileName(filePathIn));
            }

            SetCreationDate(filePathOut, creationDate);
        }

        private void SetCreationDate(string filePath, DateTime creationDate)
        {
            File.SetCreationTime(filePath, creationDate);
            File.SetLastWriteTime(filePath, creationDate);
            File.SetLastAccessTime(filePath, creationDate);
        }
    }
}