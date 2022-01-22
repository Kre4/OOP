using System;
using System.Collections.Generic;
using System.Dynamic;
using ReportsDAL.LayerSuperType;

namespace ReportsDAL.Entities
{
    public class Task : Entity
    {
        public string TaskText { get; set; }
        public DateTime CreationTime { get; set; }
        public List<TaskComment> Comments{ get; set; }
        public uint ResponsibleEmployeeId { get; set; }
        public TaskStatus Status { get; set; }

        public Task()
            : base()
        {
            Status = TaskStatus.Open;
            CreationTime = DateTime.Now;
            Comments = new List<TaskComment>();
        }
        
        
    }
    
    
}