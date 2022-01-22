using System;
using System.Collections.Generic;
using ReportsDAL.Entities;
using ReportServer.DB.Tools;

namespace ReportServer.DB.Repository.Runtime
{
    internal class ReportRuntimeRepository : RuntimeRepository<EmployeeReport>
    {
        public override List<EmployeeReport> GetByFiledName(FieldName fieldName, string value)
        {
            switch (fieldName)
            {
                case FieldName.ReportResponsibleEmployeeId:
                    return GetByResponsibleEmployeeId(value);
                default:
                    return GetByUndefinedFiled(value);
            }
        }

        private List<EmployeeReport> GetByResponsibleEmployeeId(string id)
        {
            try
            {
                uint employeeId = Convert.ToUInt32(id);
                return EntitiesList.FindAll(x => x.ResponsibleEmployeeId == employeeId);
            }
            catch (Exception)
            {
                throw new DataLayerException("Incorrect id format");
            }
        }
    }
}