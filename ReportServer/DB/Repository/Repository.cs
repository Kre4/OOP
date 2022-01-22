using System.Collections.Generic;
using ReportServer.DB.Tools;

namespace ReportServer.DB.Repository
{
    public abstract class Repository<T>
            where T : ReportsDAL.LayerSuperType.Entity
    {
        protected Repository()
        {}

        public abstract List<T> GetFullTable();
        public abstract T GetById(uint id);
        public abstract List<T> GetByFiledName(FieldName fieldName, string value);
        public abstract void Add(T entity);
        public abstract void Update(uint id, T newObject);

        public void Update(T oldObject, T newObject)
        {
            this.Update(oldObject.Id, newObject);
        }
        public abstract void Delete(uint id);
        
        protected List<T> GetByUndefinedFiled(string value)
        {
            throw new DataLayerException("Undefined field name");
        }
    }
}