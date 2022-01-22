using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using ReportsDAL.LayerSuperType;
using ReportServer.DB.Tools;

namespace ReportServer.DB.Repository.FileSystem
{
    public abstract class JsonRepository<T> : Repository<T> 
        where T : Entity
    {
        protected string _path;

        protected JsonRepository(string path)
        {
            _path = path;
        }

        public override List<T> GetFullTable()
        {
            return DeserializeObject<T[]>(GetDataBaseContent()).ToList();
        }

        public override T GetById(uint id)
        {
            return GetFullTable().Find(x => x.Id == id);
        }

        public override void Add(T entity)
        {
            if (entity == null)
            {
                throw new DataLayerException("Can't add null entity");
            }
            
            var table = GetFullTable();
            table.Add(entity);
            SetDataBaseContent(table.ToArray());
        }

        public override void Update(uint id, T newObject)
        {
            var table = GetFullTable();
            int index = table.FindIndex(x => x.Id == id);
            table[index] = newObject;
            SetDataBaseContent(table.ToArray());
        }

        public override void Delete(uint id)
        {
            var table = GetFullTable();
            table.Remove(table.Find(x => x.Id == id));
            SetDataBaseContent(table.ToArray());
        }
        
        
        
        protected TS DeserializeObject<TS>(string from)
        {
            return JsonConvert.DeserializeObject<TS>(from);
        }
        
        protected string SerializeObject<TS>(TS obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        protected string GetDataBaseContent()
        {
            return File.ReadAllText(_path);
        }

        protected void SetDataBaseContent(T[] content)
        {
            File.WriteAllText(_path, SerializeObject<T[]>(content));
        }
    }
}