using Microsoft.AspNetCore.Mvc;
using ReportsDAL.Entities;
using ReportServer.DB.ServiceLayer;

namespace ReportServer.Controllers
{
    [ApiController]
    [Route("/report")]
    public class ReportController : ControllerBase
    {
        private ReportService _reportService = new ReportService();

        [HttpGet]
        public EmployeeReport[] Get([FromQuery] long reportId = -1)
        {
            return _reportService.Get(reportId);
        }
        
        [HttpPut]
        public EmployeeReport UpdateReport([FromQuery] uint id, [FromQuery] uint employeeId,
            [FromQuery] string newText)
        {
            return _reportService.UpdateReport(id, employeeId, newText);
        }

        [HttpDelete]
        public EmployeeReport DeleteReport([FromQuery] uint id)
        {
            return _reportService.DeleteReport(id);
        }
    }
}