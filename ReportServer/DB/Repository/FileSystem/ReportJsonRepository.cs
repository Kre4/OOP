using System;
using System.Collections.Generic;
using ReportsDAL.Entities;
using ReportServer.DB.Tools;

namespace ReportServer.DB.Repository.FileSystem
{
    public class ReportJsonRepository : JsonRepository<EmployeeReport>
    {
        public ReportJsonRepository(string path) : base(path)
        {
        }
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
                return GetFullTable().FindAll(x => x.ResponsibleEmployeeId == employeeId);
            }
            catch (Exception)
            {
                throw new DataLayerException("Incorrect id format");
            }
        }

       
    }
}