using System;
using System.Collections.Generic;
using Backups.MementoPattern;
using BackupsExtra.Strategy;

namespace BackupsExtra.LimitHandlers
{
    public class DateLimitPointHandler : RestorePointLimitHandler
    {
        private readonly DateTime _dataLimit;
        public DateLimitPointHandler(AbstractStrategy strategy, DateTime dataLimit)
            : base(strategy)
        {
            _dataLimit = dataLimit;
        }

        public override bool NeedToUpdate(List<IRestorePoint> restorePoint, int index)
        {
            return restorePoint[index].GetCreationDate() < _dataLimit;
        }
    }
}