using System.Collections.Generic;
using System.Linq;
using Backups.MementoPattern;
using Backups.Tools;

namespace BackupsExtra.LimitHandlers
{
    public class HybridLimitPointHandler : RestorePointLimitHandler
    {
        private List<RestorePointLimitHandler> _limitHandlers;
        private HybridMode _mode;

        public HybridLimitPointHandler(List<RestorePointLimitHandler> limitHandlers, HybridMode mode)
            : base(limitHandlers.First().GetStrategy())
        {
            _limitHandlers = limitHandlers;
            _mode = mode;
            foreach (RestorePointLimitHandler limitHandler in limitHandlers)
            {
                if (!limitHandler.GetStrategy().Equals(this.GetStrategy()))
                {
                    throw new BackupException("Can't make Hybrid Algorithm with different strategy");
                }
            }
        }

        public enum HybridMode
        {
            SuitsAll,
            SuitsOne,
        }

        public override bool NeedToUpdate(List<IRestorePoint> restorePoint, int index)
        {
            bool result;
            switch (_mode)
            {
                case HybridMode.SuitsAll:
                    result = true;
                    _limitHandlers.ForEach(limit =>
                    {
                        result = result && limit.NeedToUpdate(restorePoint, index);
                    });
                    return result;
                case HybridMode.SuitsOne:
                    result = false;
                    _limitHandlers.ForEach(limit =>
                    {
                        result = result || limit.NeedToUpdate(restorePoint, index);
                    });
                    return result;
                default:
                    return false;
            }
        }
    }
}