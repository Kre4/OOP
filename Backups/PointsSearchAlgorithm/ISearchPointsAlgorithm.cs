using System.Collections.Generic;
using Backups.MementoPattern;
using Backups.Repo;

namespace Backups.PointsSearchAlgorithm
{
    public interface ISearchPointsAlgorithm
    {
        List<IRestorePoint> FindRestorePoints(string rootPath);
    }
}