using System;
using Microsoft.AspNetCore.Mvc;
using ReportsDAL.Entities;
using ReportServer.DB.PublicAccess;
using ReportServer.DB.Repository;

namespace ReportServer.DB.ServiceLayer
{
    public class TaskService : Service<Task>
    {
        public TaskService()
        {
            _repository = DataManager.GetInstance().GetTaskRepository();
        }
        
        public Task[] Get( long id = -1,  string creationDate = null)
        {
            if (id > -1)
                return GetById(Convert.ToUInt32(id));
            if (creationDate != null)
                return GetByCreationDate(creationDate);
            return GetTable();
        }

        private Task[] GetTable()
        {
            return _repository.GetFullTable().ToArray();
        }

        
        private Task[] GetById(uint id)
        {
            return new []{_repository.GetById(id)};
        }

        
        private Task[] GetByCreationDate(string creationDate)
        {
            return _repository.GetByFiledName(FieldName.TaskCreationDate, creationDate).ToArray();
        }
        
        public Task CreateNewTask(uint responsibleEmployeeId, string taskText)
        {
            var task = new Task();
            task.ResponsibleEmployeeId = responsibleEmployeeId;
            task.TaskText = taskText;
            return task;
        }

        [HttpPut]
        public Task UpdateTask(uint id, TaskStatus status, string taskText, uint responsibleEmployee)
        {
            var task = _repository.GetById(id);
            if (status != 0)
                task.Status = status;
            if (taskText != string.Empty)
                task.TaskText = taskText;
            if (responsibleEmployee != 0)
                task.ResponsibleEmployeeId = responsibleEmployee;
            _repository.Update(id, task);
            return task;
        }
        
        public Task DeleteTask(uint id)
        {
            var task = _repository.GetById(id);
            _repository.Delete(id);
            return task;
        }
    }
}