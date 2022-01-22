using ReportsDAL.LayerSuperType;
using ReportServer.DB.PublicAccess;

namespace ReportServer.DB.ServiceLayer
{
    public abstract class Service<T>
    where T : Entity
    {
        protected Repository.Repository<T> _repository;

        protected Service()
        {
        }
    }
}