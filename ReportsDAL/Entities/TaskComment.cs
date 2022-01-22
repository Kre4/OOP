using System;
using ReportsDAL.LayerSuperType;

namespace ReportsDAL.Entities
{
    public class TaskComment : Entity
    {
        public DateTime LastEditDateTime { get; set; }
        public Employee CommentAuthor { get; set; }
        public string CommentText { get; set; }
    }
}