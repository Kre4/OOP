using System.Collections.Generic;
using Isu.Services;
using Isu.Services.Groups;
using Isu.Services.Students;
using IsuExtra.OGNP;
using IsuExtra.Timetable;

namespace IsuExtra.Service
{
    public interface IIsuExtraService : IIsuService
    {
        CourseOgnp AddOgnp(char megaFacultySymbol);

        CourseOgnp FindOgnp(CourseOgnp ognp);
        void EnrollStudentInOgnp(Student student, CourseOgnp ognp, string streamName);
        void RemoveStudentFromOgnp(Student student, CourseOgnp ognp);
        List<Student> GetStudentsFromOgnpGroup(CourseOgnp ognp, GroupName groupName);
        List<Student> GetStudentsFromOgnpGroup(CourseOgnp ognp, string streamName);
        List<Student> GetStudentsFromOgnpGroup(Stream stream);
        List<Student> GetStudentsWithoutOgnp();
        GroupWithSchedule AddGroup(string name, Schedule schedule);
    }
}