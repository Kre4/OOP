using System;
using Microsoft.AspNetCore.Mvc;
using ReportsDAL.Entities;
using ReportServer.DB.PublicAccess;
using ReportServer.DB.Repository;
using ReportServer.DB.ServiceLayer;

namespace ReportServer.Controllers
{
    [ApiController]
    [Route("/task")]
    public class TaskController : ControllerBase
    {
        private TaskService _taskService = new TaskService();

        [HttpGet]
        public Task[] Get([FromQuery] long id = -1, [FromQuery] string creationDate = null)
        {
            return _taskService.Get(id, creationDate);
        }

        
        [HttpPost]
        public Task CreateNewTask([FromQuery] uint responsibleEmployeeId, [FromQuery] string taskText)
        {
            return _taskService.CreateNewTask(responsibleEmployeeId, taskText);
        }

        [HttpPut]
        public Task UpdateTask([FromQuery] uint id, [FromQuery] TaskStatus status, [FromQuery] string taskText, [FromQuery] uint responsibleEmployee)
        {
            return _taskService.UpdateTask(id, status, taskText, responsibleEmployee);
        }

        [HttpDelete]
        public Task DeleteTask([FromQuery] uint id)
        {
            return _taskService.DeleteTask(id);
        }
    }
}