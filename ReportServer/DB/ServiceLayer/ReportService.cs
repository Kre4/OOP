using System;
using Microsoft.AspNetCore.Mvc;
using ReportsDAL.Entities;
using ReportServer.DB.PublicAccess;

namespace ReportServer.DB.ServiceLayer
{
    public class ReportService : Service<EmployeeReport>
    {
        public ReportService()
        {
            _repository = DataManager.GetInstance().GetReportRepository();
        }
        
        
        public EmployeeReport[] Get( long reportId = -1)
        {
            if (reportId > -1)
                return GetById(Convert.ToUInt32(reportId));

            return GetTable();
        }

        private EmployeeReport[] GetTable()
        {
            return _repository.GetFullTable().ToArray();
        }

        private EmployeeReport[] GetById(uint id)
        {
            return new[] {_repository.GetById(id)};
        }
        
        public EmployeeReport UpdateReport(uint id, uint employeeId,
            [FromQuery] string newText)
        {
            var report = _repository.GetById(id);
            report.ResponsibleEmployeeId = employeeId;
            report.ReportText = newText;
            _repository.Update(id, report);
            return report;
        }
        
        public EmployeeReport DeleteReport(uint id)
        {
            var report = _repository.GetById(id);
            _repository.Delete(id);
            return report;
        }
    }
}