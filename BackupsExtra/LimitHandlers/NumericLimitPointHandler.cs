using System.Collections.Generic;
using Backups.MementoPattern;
using BackupsExtra.Strategy;

namespace BackupsExtra.LimitHandlers
{
    public class NumericLimitPointHandler : RestorePointLimitHandler
    {
        private readonly uint _numericLimit;
        public NumericLimitPointHandler(AbstractStrategy strategy, uint numericLimit)
            : base(strategy)
        {
            _numericLimit = numericLimit;
        }

        public override bool NeedToUpdate(List<IRestorePoint> restorePoint, int index)
        {
            return _numericLimit <= index;
        }
    }
}