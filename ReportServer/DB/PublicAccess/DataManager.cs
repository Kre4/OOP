using ReportsDAL.Entities;
using ReportServer.DB.Repository;

namespace ReportServer.DB.PublicAccess
{
    public class DataManager
    {
        private static DataManager _instance;
        private IProviderDI _providerDI = new ProviderDI();
        private Repository<Employee> _employeeRepository;
        private Repository<Task> _taskRepository;
        private Repository<EmployeeReport> _employeeReportRepository;

        private DataManager()
        {
            _employeeRepository = _providerDI.GetEmployeeRepository();
            _taskRepository = _providerDI.GetTaskRepository();
            _employeeReportRepository = _providerDI.GetReportRepository();
        }

        public Repository<Employee> GetEmployeeRepository() => _employeeRepository;
        public Repository<Task> GetTaskRepository() => _taskRepository;
        public Repository<EmployeeReport> GetReportRepository() => _employeeReportRepository;

        public static DataManager GetInstance()
        {
            if (_instance == null)
            {
                _instance = new DataManager();
            }

            return _instance;
        }
    }
}