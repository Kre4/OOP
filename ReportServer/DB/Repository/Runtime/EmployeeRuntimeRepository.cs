using System;
using System.Collections.Generic;
using ReportsDAL.Entities;
using ReportServer.DB.Tools;

namespace ReportServer.DB.Repository.Runtime
{
    internal class EmployeeRuntimeRepository : RuntimeRepository<Employee>
    {
        public override List<Employee> GetByFiledName(FieldName fieldName, string value)
        {
            switch (fieldName)
            {
                case FieldName.EmployeeName:
                    return GetEmployeesByChiefId(value);
                case FieldName.EmployeeChiefId:
                    return GetByName(value);
                default:
                    return GetByUndefinedFiled(value);
            }
        }
        
        private List<Employee> GetEmployeesByChiefId(string id)
        {
            try
            {
                uint chiefId = Convert.ToUInt32(id);
                return EntitiesList.FindAll(x => x.ChiefId == chiefId);
            }
            catch (Exception)
            {
                throw new DataLayerException("Incorrect chief id format");
            }
           
        }
        private List<Employee> GetByName(string employeeName)
        {
            return EntitiesList.FindAll(x => x.Name == employeeName);
        }
    }
}