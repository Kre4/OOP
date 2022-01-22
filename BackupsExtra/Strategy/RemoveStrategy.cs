using System.Collections.Generic;
using System.Linq;
using Backups.MementoPattern;
using BackupsExtra.FileManagerExtra;

namespace BackupsExtra.Strategy
{
    public class RemoveStrategy : AbstractStrategy
    {
        private IFileManagerExtra _fileManager;

        public RemoveStrategy(IFileManagerExtra fileManager)
        : base(2)
        {
            _fileManager = fileManager;
        }

        public override List<IRestorePoint> Update(IEnumerable<IRestorePoint> restorePoints, int index)
        {
            var restorePointsList = restorePoints.ToList();
            _fileManager.RemoveFiles(restorePointsList[index].GetFileNames());
            restorePointsList.RemoveAt(index);
            return restorePointsList;
        }
    }
}