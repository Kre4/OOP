using System.Collections.Generic;
using Backups.MementoPattern;

namespace BackupsExtra.Strategy
{
    public abstract class AbstractStrategy
    {
        private int _id;

        protected AbstractStrategy(int id)
        {
            _id = id;
        }

        public abstract List<IRestorePoint> Update(IEnumerable<IRestorePoint> restorePoints, int index);
        public bool Equals(AbstractStrategy otherStrategy) => otherStrategy.GetStrategyId() == _id;
        public int GetStrategyId() => _id;
    }
}