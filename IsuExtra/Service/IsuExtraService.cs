using System;
using System.Collections.Generic;
using Isu.Services;
using Isu.Services.Groups;
using Isu.Services.Students;
using Isu.Tools;
using IsuExtra.OGNP;
using IsuExtra.Timetable;
using IsuExtra.Tools;

namespace IsuExtra.Service
{
    public class IsuExtraService : IIsuExtraService
    {
        private IIsuService _oldIsuService = new IsuService();
        private List<CourseOgnp> _ognps = new List<CourseOgnp>();
        private List<GroupNameAndSchedule> _schedules = new List<GroupNameAndSchedule>();

        // IsuExtraService
        public CourseOgnp AddOgnp(char megaFacultySymbol)
        {
            var ognp = new CourseOgnp(megaFacultySymbol);
            _ognps.Add(ognp);
            return ognp;
        }

        public CourseOgnp FindOgnp(CourseOgnp ognp)
        {
            var result = _ognps.Find(thisOgnp => thisOgnp.GetId() == ognp.GetId());
            if (result == null)
                throw new OgnpException("There is no such ognp");
            return result;
        }

        public void EnrollStudentInOgnp(Student student, CourseOgnp ognp, string streamName)
        {
            var thisOgnp = FindOgnp(ognp);
            if (student.GetGroup().GetMegaFacultySymbol() == ognp.GetMegaFacultySymbol())
            {
                throw new OgnpException("Student can't choose ognp from the same faculty");
            }

            if (AmountOfStudentOgnp(student.GetId()) >= ExtraConstants.MaximumOgnpChoosen)
            {
                throw new OgnpException("Can't enroll in more than 2 ognp");
            }

            if (HasAnyScheduleCrossing(student, ognp.GetStreamByName(streamName).GetSchedule()))
            {
                throw new OgnpException("Schedules have crossing");
            }

            thisOgnp.EnrollNewStudent(student, streamName);
        }

        public void RemoveStudentFromOgnp(Student student, CourseOgnp ognp)
        {
            var thisOgnp = FindOgnp(ognp);
            thisOgnp.RemoveStudent(student);
        }

        public List<Student> GetStudentsFromOgnpGroup(CourseOgnp ognp, GroupName groupName)
        {
            var thisOgnp = FindOgnp(ognp);
            var result = new List<Student>();
            thisOgnp.GetNamesOfStreams().ForEach(streamName =>
            {
                Stream stream = ognp.GetStreamByName(streamName);
                if (stream.GetGroup().GetGroupName().Name == groupName.Name)
                {
                    result.AddRange(stream.GetStudents());
                }
            });
            return result;
        }

        public List<Student> GetStudentsFromOgnpGroup(CourseOgnp ognp, string streamName)
        {
            var thisOgnp = FindOgnp(ognp);
            return ognp.GetStreamByName(streamName).GetStudents();
        }

        public List<Student> GetStudentsFromOgnpGroup(Stream stream)
        {
            return stream.GetStudents();
        }

        public List<Student> GetStudentsWithoutOgnp()
        {
            var result = new List<Student>();
            _schedules.ForEach(groupName =>
            {
                _oldIsuService.FindGroup(groupName.Name).GetAllStudents().ForEach(student =>
                {
                    if (AmountOfStudentOgnp(student.GetId()) == ExtraConstants.ZeroOgnpChoosen)
                    {
                        result.Add(student);
                    }
                });
            });
            return result;
        }

        // IIsuService, ведь переиспользование это хорошо
        public GroupWithSchedule AddGroup(string name, Schedule schedule)
        {
            var group = _oldIsuService.AddGroup(name);
            _schedules.Add(new GroupNameAndSchedule(name, schedule));
            return new GroupWithSchedule(group, schedule);
        }

        public Group AddGroup(string name)
        {
            throw new IsuException("Deprecated");
        }

        public Student AddStudent(Group @group, string name)
        {
            return _oldIsuService.AddStudent(group, name);
        }

        public Student GetStudent(int id)
        {
            return _oldIsuService.GetStudent(id);
        }

        public Student FindStudent(string name)
        {
            return _oldIsuService.FindStudent(name);
        }

        public List<Student> FindStudents(string groupName)
        {
            return _oldIsuService.FindStudents(groupName);
        }

        public List<Student> FindStudents(CourseNumber courseNumber)
        {
            return _oldIsuService.FindStudents(courseNumber);
        }

        public Group FindGroup(string groupName)
        {
            return _oldIsuService.FindGroup(groupName);
        }

        public List<Group> FindGroups(CourseNumber courseNumber)
        {
            return _oldIsuService.FindGroups(courseNumber);
        }

        public void ChangeStudentGroup(Student student, Group newGroup)
        {
            _oldIsuService.ChangeStudentGroup(student, newGroup);
        }

        // Useful functions
        private int AmountOfStudentOgnp(int studentId)
        {
            int counter = 0;
            _ognps.ForEach(ognp =>
            {
                if (ognp.ContainsStudent(studentId))
                {
                    ++counter;
                }
            });
            return counter;
        }

        private bool HasAnyScheduleCrossing(Student student, Schedule ognpSchedule)
        {
            var studentSchedule = _schedules.Find(
                tuple => student.GetGroup().GetGroupName().Name == tuple.Name);
            try
            {
                return studentSchedule.Schedule.HasAnyCrossing(ognpSchedule);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}