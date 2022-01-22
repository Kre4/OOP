using ReportsDAL.Entities;
using ReportServer.DB.Repository;
using ReportServer.DB.Repository.FileSystem;
using ReportServer.DB.Repository.Runtime;

namespace ReportServer.DB.PublicAccess
{
    internal class ProviderDI : IProviderDI
    {
        public Repository<Employee> GetEmployeeRepository()
        {
            return new EmployeeJsonRepository("employee.json");
        }

        public Repository<Task> GetTaskRepository()
        {
            return new TaskJsonRepository("task.json");
        }

        public Repository<EmployeeReport> GetReportRepository()
        {
            return new ReportJsonRepository("report.json");
        }
    }
}