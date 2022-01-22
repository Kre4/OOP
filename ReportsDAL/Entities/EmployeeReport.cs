using ReportsDAL.LayerSuperType;

namespace ReportsDAL.Entities
{
    public class EmployeeReport: Entity
    {
        public string ReportText { get; set; }
        public uint ResponsibleEmployeeId { get; set; }
    }
}