using System.Collections.Generic;
using System.Linq;
using Backups.MementoPattern;
using Backups.Saver;
using Backups.Tools;
using BackupsExtra.FileManagerExtra;
using BackupsExtra.Tools;

namespace BackupsExtra.Strategy
{
    public class MergeStrategy : AbstractStrategy
    {
        private IFileManagerExtra _fileManager;
        private IStorageAlgorithm _storageAlgorithm;

        public MergeStrategy(IFileManagerExtra fileManager, IStorageAlgorithm storageAlgorithm)
            : base(1)
        {
            _fileManager = fileManager;
            _storageAlgorithm = storageAlgorithm;
        }

        public override List<IRestorePoint> Update(IEnumerable<IRestorePoint> restorePoints, int index)
        {
            switch (_storageAlgorithm)
            {
                case SingleStorageAlgorithm algorithm:
                    return new RemoveStrategy(_fileManager).Update(restorePoints, index);
                case SplitStorageAlgorithm algorithm:
                    if (index == 0)
                        index += 1;
                    var list = restorePoints.ToList();
                    list[index] = MergePoints(list[index - 1], list[index]);
                    return new RemoveStrategy(_fileManager).Update(list, index - 1);
                default:
                    throw new BackupException("Incorrect storage algorithm in Merge Strategy");
            }
        }

        private IRestorePoint MergePoints(IRestorePoint from, IRestorePoint to)
        {
            List<string> filePathsOfNewRestorePoint = new List<string>();
            filePathsOfNewRestorePoint.AddRange(to.GetFileNames());
            foreach (string fileFullPath in from.GetFileNames())
            {
                string folderPath = string.Empty;
                foreach (string name in to.GetFileNames())
                {
                    if (FileNameManager.GetFileName(fileFullPath) == FileNameManager.GetFileName(name))
                    {
                        continue;
                    }

                    if (folderPath == string.Empty)
                        folderPath = name.GetFileFolder();
                }

                _fileManager.CopyFile(fileFullPath, folderPath);
                filePathsOfNewRestorePoint.Add(folderPath + "\\" + FileNameManager.GetFileName(fileFullPath));
            }

            return new RestorePoint(to.GetCreationDate(), filePathsOfNewRestorePoint);
        }
    }
}