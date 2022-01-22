using Microsoft.AspNetCore.Mvc;
using ReportsDAL.Entities;
using ReportServer.DB.ServiceLayer;

namespace ReportServer.Controllers
{
    [ApiController]
    [Route("/employee")]
    public class EmployeeController : ControllerBase
    {
        private EmployeeService _employeeService = new EmployeeService();

        [HttpGet]
        public Employee[] Get([FromQuery] long id = -1, [FromQuery] string name = null)
        {
            return _employeeService.Get(id, name);
        }
        
        [HttpPost]
        public Employee AddNewEmployee([FromQuery] string name, [FromQuery] long chiefId = -1)
        {
            return _employeeService.AddNewEmployee(name, chiefId);
        }

        [HttpPut]
        public void Update([FromBody] Employee employee)
        {
            _employeeService.Update(employee);
        }

       

        [HttpDelete]
        public Employee DeleteById([FromQuery] uint id)
        {
            return _employeeService.DeleteById(id);
        }
    }
}