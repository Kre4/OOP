using System;
using System.Collections;
using System.Collections.Generic;

namespace Backups.MementoPattern
{
    public class RestorePoint : IRestorePoint
    {
        private DateTime _creationDate;
        private List<string> _filePaths;

        public RestorePoint(DateTime creationDate, List<string> filePaths = null)
        {
            _creationDate = creationDate;
            _filePaths = new List<string>();
            if (filePaths == null) return;
            foreach (string filePath in filePaths)
            {
                AddFile(filePath);
            }
        }

        public DateTime GetCreationDate() => _creationDate;

        public IEnumerable<string> GetFileNames() => _filePaths;
        public void AddFile(string filePath)
        {
            _filePaths.Add(filePath);
        }
    }
}