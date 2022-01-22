using Isu.Services.Groups;
using Isu.Tools;

namespace Isu.Services.Students
{
    public class Student
    {
        private readonly string _name;
        private readonly int _id;
        private Group _group;

        public Student(string name, Group group)
        {
            _name = name;
            _id = Id.GetId();
            _group = group;
        }

        public string GetName()
        {
            return _name;
        }

        public int GetId()
        {
            return _id;
        }

        public Group GetGroup()
        {
            return _group;
        }

        public void SetGroup(Group newGroup)
        {
            _group = newGroup;
        }
    }
}