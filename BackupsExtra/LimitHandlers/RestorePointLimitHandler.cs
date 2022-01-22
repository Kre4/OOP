using System.Collections.Generic;
using System.Linq;
using Backups.MementoPattern;
using BackupsExtra.Strategy;
using BackupsExtra.Tools;

namespace BackupsExtra.LimitHandlers
{
    public abstract class RestorePointLimitHandler
    {
        private readonly AbstractStrategy _strategy;

        protected RestorePointLimitHandler(AbstractStrategy strategy)
        {
            _strategy = strategy;
        }

        public IEnumerable<IRestorePoint> Update(IEnumerable<IRestorePoint> restorePoints)
        {
            var restorePointsList = restorePoints.ToList();
            var indexes = new List<int>();
            for (int i = 0; i < restorePointsList.Count; ++i)
            {
                if (NeedToUpdate(restorePointsList, i))
                {
                    indexes.Add(i - indexes.Count);
                }
            }

            if (indexes.Count == restorePointsList.Count)
            {
                throw new RestorePointsException("Cannot delete all restore points");
            }

            indexes.ForEach(index =>
            {
                restorePointsList = _strategy.Update(restorePointsList, index);
            });
            return restorePointsList;
        }

        public abstract bool NeedToUpdate(List<IRestorePoint> restorePoint, int index);

        public AbstractStrategy GetStrategy() => _strategy;
    }
}