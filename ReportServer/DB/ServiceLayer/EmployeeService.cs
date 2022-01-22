using System;
using Microsoft.AspNetCore.Mvc;
using ReportsDAL.Entities;
using ReportServer.DB.PublicAccess;
using ReportServer.DB.Repository;

namespace ReportServer.DB.ServiceLayer
{
    public class EmployeeService : Service<Employee>

    {
        public EmployeeService()
        {
            _repository = DataManager.GetInstance().GetEmployeeRepository();
        }
        
        public Employee[] Get(long id, string name)
        {
            if (id > -1)
                return GetById(Convert.ToUInt32(id));
            if (name != null)
                return GetByName(name);
            
            return GetTable();
        }

        private Employee[] GetTable()
        {
            return _repository.GetFullTable().ToArray();
        }
        
        
        private Employee[] GetByName([FromQuery] string name)
        {
            return _repository.GetByFiledName(FieldName.EmployeeName, name).ToArray();
        }

        
        private Employee[] GetById([FromQuery] uint id)
        {
            return new[]{_repository.GetById(id)};
        }
        
        public Employee AddNewEmployee(string name, long chiefId)
        {
            var employee = new Employee();
            employee.Name = name;
            if (chiefId >= 0)
                employee.ChiefId = (uint)chiefId;
            _repository.Add(employee);
            return employee;
        }
        
        public void Update(Employee employee)
        {
            _repository.Update(employee.Id, employee);
        }
        
        public Employee DeleteById(uint id)
        {
            var employee = _repository.GetById(id);
            _repository.Delete(id);
            return employee;
        }
    }
}