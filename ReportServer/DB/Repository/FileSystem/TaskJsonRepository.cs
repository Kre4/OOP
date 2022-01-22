using System;
using System.Collections.Generic;
using ReportsDAL.Entities;
using ReportServer.DB.Tools;

namespace ReportServer.DB.Repository.FileSystem
{
    public class TaskJsonRepository : JsonRepository<Task>
    {
        public TaskJsonRepository(string path) : base(path)
        {
        }
        public override List<Task> GetByFiledName(FieldName fieldName, string value)
        {
            switch (fieldName)
            {
                case FieldName.TaskCreationDate:
                    return GetByCreationDate(value);
                default:
                    return GetByUndefinedFiled(value);
            }
        }
        
        private List<Task> GetByCreationDate(string date)
        {
            try
            {
                DateTime creationDate = DateTime.Parse(date);
                return GetFullTable().FindAll(x => x.CreationTime == creationDate);
            }
            catch (Exception)
            {
                throw new DataLayerException("Incorrect date format for filed CreationTime");
            }
           
        }

       
    }
}