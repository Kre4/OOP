using System;
using System.Collections;
using System.Collections.Generic;

namespace Backups.MementoPattern
{
    public interface IRestorePoint
    {
        DateTime GetCreationDate();
        IEnumerable<string> GetFileNames();

        void AddFile(string filePath);
    }
}