using System.Collections.Generic;
using Backups.MementoPattern;
using BackupsExtra.Strategy;

namespace BackupsExtra.LimitHandlers
{
    public class NoLimitHandler : RestorePointLimitHandler
    {
        public NoLimitHandler(AbstractStrategy strategy)
            : base(strategy)
        {
        }

        public override bool NeedToUpdate(List<IRestorePoint> restorePoint, int index)
        {
            return false;
        }
    }
}