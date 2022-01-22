using System.Collections.Generic;
using ReportsDAL.LayerSuperType;
using ReportServer.DB.Tools;

namespace ReportServer.DB.Repository.Runtime
{
    internal abstract class RuntimeRepository<T> : Repository<T>
            where T : Entity
    {
        protected List<T> EntitiesList = new List<T>();
        
        public override List<T> GetFullTable()
        {
            return EntitiesList;
        }

        public override T GetById(uint id)
        {
            return EntitiesList.Find(x => x.Id == id);
        }
        public override void Add(T entity)
        {
            if (entity == null)
            {
                throw new DataLayerException("Can't add null entity");
            }
            
            EntitiesList.Add(entity);
        }

        public override void Update(uint id, T newObject)
        {
            int index = EntitiesList.FindIndex(x => x.Id == id);
            EntitiesList[index] = newObject;
        }

        public override void Delete(uint id)
        {
            EntitiesList.Remove(EntitiesList.Find(x => x.Id == id));
        }
    }
}