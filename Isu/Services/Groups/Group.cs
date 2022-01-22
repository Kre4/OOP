using System;
using System.Collections.Generic;
using Isu.Services.Students;
using Isu.Tools;

namespace Isu.Services.Groups
{
    public class Group
        : IEquatable<Group>
    {
        private readonly GroupName _groupName;
        private List<Student> _students = new List<Student>();

        public Group(GroupName groupName)
        {
            _groupName = groupName;
        }

        public CourseNumber GetCourseNumber()
        {
            return _groupName.CourseNumber;
        }

        public List<Student> GetAllStudents()
        {
            return _students;
        }

        public GroupName GetGroupName()
        {
            return _groupName;
        }

        public char GetMegaFacultySymbol()
        {
            return _groupName.Name[0];
        }

        public Student AddStudent(string name)
        {
            var student = new Student(name, null);
            AddStudent(student);
            return student;
        }

        public Student FindStudent(int id)
        {
            return _students.Find(student => student.GetId() == id);
        }

        public Student FindStudent(string name)
        {
            return _students.Find(student => student.GetName() == name);
        }

        public bool Equals(Group other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(_groupName.Name, other._groupName.Name);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Group)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_groupName.Name);
        }

        public void AddStudent(Student student)
        {
            CheckAmountOfStudents();
            if (!_students.Contains(student))
            {
                _students.Add(student);
            }

            student.SetGroup(this);
            }

        public void RemoveStudent(Student student)
        {
            _students.Remove(student);
        }

        private void CheckAmountOfStudents()
        {
            if (_students.Count >= Constants.MaxStudentsPerGroup)
                throw new IsuException("Maximum students in a group");
        }
    }
}