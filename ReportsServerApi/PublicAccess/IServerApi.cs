using System;
using ReportsDAL.Entities;

namespace ReportsServerApi.PublicAccess
{
    public interface IServerApi
    {
        public Employee[] GetAllEmployees();
        public Employee GetEmployeeById(uint id);
        public Employee[] GetEmployeeByName(string name);
        public Employee CreateEmployee(string name, long chiefId = -1);
        public Employee DeleteEmployee(uint id);
        public Task[] GetAllTasks();
        public Task GetTaskById(uint id);
        public Task[] GetTaskByCreationDate(DateTime creationDate);
        public Task CreateTask(uint responsibleEmployeeId, string taskText);

        public Task UpdateTask(uint id, TaskStatus status, string taskText, uint responsibleEmployee);

    }
}