using System.Collections.Generic;
using System.Linq;
using Backups.MementoPattern;

namespace BackupsExtra.Strategy
{
    public class NoStrategy : AbstractStrategy
    {
        public NoStrategy()
            : base(0)
        {
        }

        public override List<IRestorePoint> Update(IEnumerable<IRestorePoint> restorePoints, int index)
        {
            return restorePoints.ToList();
        }
    }
}