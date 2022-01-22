using System.Collections.Generic;

using Isu.Services.Groups;
using Isu.Services.Students;

namespace Isu.Services
{
    public class IsuService
        : IIsuService
    {
        private List<Group> _groupsList = new List<Group>();
        public Group AddGroup(string name)
        {
            var current = new Group(new GroupName(name));
            if (!_groupsList.Contains(current))
                _groupsList.Add(current);
            return current;
        }

        public Student AddStudent(Group group, string name)
        {
            return _groupsList.Find(cur_group => cur_group.Equals(group)).AddStudent(name);
        }

        public Student GetStudent(int id)
        {
            foreach (Group group in _groupsList)
            {
                Student current = group.FindStudent(id);
                if (current != null)
                    return current;
            }

            return null;
        }

        public Student FindStudent(string name)
        {
            foreach (Group group in _groupsList)
            {
                Student current = group.FindStudent(name);
                if (current != null)
                    return current;
            }

            return null;
        }

        public List<Student> FindStudents(string groupName)
        {
            Group group = _groupsList.Find(group => group.GetGroupName().Name == groupName);
            return group == null ? new List<Student>() : group.GetAllStudents();
        }

        public List<Student> FindStudents(CourseNumber courseNumber)
        {
            var nCourseStudents = new List<Student>();
            FindGroups(courseNumber).ForEach(group => nCourseStudents.AddRange(group.GetAllStudents()));
            return nCourseStudents;
        }

        public Group FindGroup(string groupName)
        {
            return _groupsList.Find(group => group.Equals(new Group(new GroupName(groupName))));
        }

        public List<Group> FindGroups(CourseNumber courseNumber)
        {
            var nCourseGroups = new List<Group>();
            foreach (Group group in _groupsList)
            {
                if (group.GetCourseNumber().Course == courseNumber.Course)
                {
                    nCourseGroups.Add(group);
                }
            }

            return nCourseGroups;
        }

        public void ChangeStudentGroup(Student student, Group newGroup)
        {
            Group oldGroup = student.GetGroup();
            newGroup.AddStudent(student);
            oldGroup.RemoveStudent(student);
        }
    }
}