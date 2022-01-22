using System;

namespace ReportsDAL.LayerSuperType
{
    public class Entity
    {
        public uint Id { get; set;}

        public Entity()
        {
            Id = IdGenerator.GetNewId();
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Id == ((Entity) obj).Id;
        }
    }
}