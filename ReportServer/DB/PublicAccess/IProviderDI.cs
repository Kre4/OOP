using ReportsDAL.Entities;
using ReportServer.DB.Repository;

namespace ReportServer.DB.PublicAccess
{
    internal interface IProviderDI
    {
        Repository<Employee> GetEmployeeRepository();
        Repository<Task> GetTaskRepository();
        Repository<EmployeeReport> GetReportRepository();
    }
}