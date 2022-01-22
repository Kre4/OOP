using System.Collections.Generic;
using Isu.Services.Groups;
using Isu.Services.Students;
using Isu.Tools;
using IsuExtra.Timetable;

namespace IsuExtra.OGNP
{
    public class Stream
    {
        private GroupWithSchedule _groupWithSchedule;
        private string _name;

        public Stream(string name, GroupWithSchedule groupWithSchedule)
        {
            _groupWithSchedule = groupWithSchedule;
            _name = name;
        }

        public void EnrollNewStudent(Student student)
        {
            _groupWithSchedule.GetGroup.AddStudent(student);
        }

        public void RemoveStudent(Student student)
        {
            _groupWithSchedule.GetGroup.RemoveStudent(student);
        }

        public string GetName() => _name;

        public Group GetGroup() => _groupWithSchedule.GetGroup;

        public Schedule GetSchedule() => _groupWithSchedule.GetSchedule;

        public List<Student> GetStudents() => _groupWithSchedule.GetGroup.GetAllStudents();

        public bool ContainsStudent(int studentId)
        {
            return _groupWithSchedule.GetGroup.FindStudent(studentId) != null;
        }

        private int GetFreeSlots()
        {
            return Constants.MaxStudentsPerGroup - _groupWithSchedule.GetGroup.GetAllStudents().Count;
        }
    }
}