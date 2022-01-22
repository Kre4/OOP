using System;
using System.Collections.Generic;
using ReportsDAL.LayerSuperType;

namespace ReportsDAL.Entities
{
    public class Employee : Entity
    {
        public uint ChiefId { get; set;}
        
        public string Name { get; set; }
        public List<uint> Tasks { get; set; }
        public uint Report { get; set; }
        public List<uint> SubordinatesId { get; set;}

        public Employee()
            : base()
        {
            Tasks = new List<uint>();
            SubordinatesId = new List<uint>();
        }
    }
}