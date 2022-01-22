using System;

using IsuExtra.OGNP;
using IsuExtra.Service;
using IsuExtra.Timetable;
using IsuExtra.Tools;
using NUnit.Framework;

namespace IsuExtra.Tests
{
    
    public class IsuExtraServiceTest
    {
        private IIsuExtraService _isuExtraService;
        
        [SetUp]
        public void SetUp()
        {
            _isuExtraService = new IsuExtraService();
        }

        [Test]
        public void AddStream_StreamAdded()
        {
            var ognp = _isuExtraService.AddOgnp('M');
            var groupWithSchedule = _isuExtraService.AddGroup("M3213", GenerateSchedule(DateTime.Now));
            var stream = new Stream("Cyber 2.0", groupWithSchedule);
            ognp.AddStream(stream);
            Assert.AreEqual(_isuExtraService.FindOgnp(ognp).GetStreamByName("Cyber 2.0").GetName(), "Cyber 2.0");
        }   
        
        [Test]
        public void EnrollStudent_StudentEnrolled()
        {
            var ognp = _isuExtraService.AddOgnp('M');
            var groupWithSchedule = _isuExtraService.AddGroup("M3213", GenerateSchedule(DateTime.Now));
            var stream = new Stream("Cyber 2.0", groupWithSchedule);
            ognp.AddStream(stream);
            var groupWithScheduleFromAnotherFaculty =
                _isuExtraService.AddGroup("Y3201", GenerateSchedule(DateTime.Today));
            var student = _isuExtraService.AddStudent(groupWithScheduleFromAnotherFaculty.GetGroup, "Vasia Pupkin");
            _isuExtraService.EnrollStudentInOgnp(student, ognp, "Cyber 2.0");
            Assert.AreEqual(ognp.GetStreamByName("Cyber 2.0").ContainsStudent(student.GetId()), true);
            
        }

        [Test]
        public void EnrollStudentInNonExistentStream()
        {
            var ognp = _isuExtraService.AddOgnp('M');
            var groupWithSchedule = _isuExtraService.AddGroup("M3213", GenerateSchedule(DateTime.Now));
            var stream = new Stream("Cyber 2.0", groupWithSchedule);
            ognp.AddStream(stream);
            var groupWithScheduleFromAnotherFaculty =
                _isuExtraService.AddGroup("Y3201", GenerateSchedule(DateTime.Today));
            var student = _isuExtraService.AddStudent(groupWithScheduleFromAnotherFaculty.GetGroup, "Vasia Pupkin");
            
            Assert.Catch<OgnpException>(() =>
            {
                _isuExtraService.EnrollStudentInOgnp(student, ognp, "Cyber 3.0");
            });
           
        }

        [Test]
        public void RemoveStudent_StudentRemoved()
        {
            var ognp = _isuExtraService.AddOgnp('M');
            var groupWithSchedule = _isuExtraService.AddGroup("M3213", GenerateSchedule(DateTime.Now));
            var stream = new Stream("Cyber 2.0", groupWithSchedule);
            ognp.AddStream(stream);
            var groupWithScheduleFromAnotherFaculty =
                _isuExtraService.AddGroup("Y3201", GenerateSchedule(DateTime.Today));
            var student = _isuExtraService.AddStudent(groupWithScheduleFromAnotherFaculty.GetGroup, "Vasia Pupkin");
            _isuExtraService.EnrollStudentInOgnp(student, ognp, "Cyber 2.0");
            _isuExtraService.RemoveStudentFromOgnp(student, ognp);
            Assert.AreEqual(ognp.ContainsStudent(student.GetId()), false);
        }

        [Test]
        public void GetListOfStudentsWithoutOgnp_ListIsEmpty()
        {
            var ognp = _isuExtraService.AddOgnp('M');
            var groupWithSchedule = _isuExtraService.AddGroup("M3213", GenerateSchedule(DateTime.Now));
            var stream = new Stream("Cyber 2.0", groupWithSchedule);
            ognp.AddStream(stream);
            var groupWithScheduleFromAnotherFaculty =
                _isuExtraService.AddGroup("Y3201", GenerateSchedule(DateTime.Today));
            var student = _isuExtraService.AddStudent(groupWithScheduleFromAnotherFaculty.GetGroup, "Vasia Pupkin");
            _isuExtraService.EnrollStudentInOgnp(student, ognp, "Cyber 2.0");
            Assert.AreEqual(_isuExtraService.GetStudentsWithoutOgnp().Count, 0);
            
        }
        
        [Test]
        public void GetListOfStudentsWithoutOgnp_ListIsNotEmpty()
        {
            var ognp = _isuExtraService.AddOgnp('M');
            var groupWithSchedule = _isuExtraService.AddGroup("M3213", GenerateSchedule(DateTime.Now));
            var stream = new Stream("Cyber 2.0", groupWithSchedule);
            ognp.AddStream(stream);
            var groupWithScheduleFromAnotherFaculty =
                _isuExtraService.AddGroup("Y3201", GenerateSchedule(DateTime.Today));
            var student = _isuExtraService.AddStudent(groupWithScheduleFromAnotherFaculty.GetGroup, "Vasia Pupkin");
            var unenrolledStudent = _isuExtraService.AddStudent(groupWithScheduleFromAnotherFaculty.GetGroup, "Abobus");
            _isuExtraService.EnrollStudentInOgnp(student, ognp, "Cyber 2.0");
            Assert.AreEqual(_isuExtraService.GetStudentsWithoutOgnp().Count, 1);
            Assert.AreEqual(_isuExtraService.GetStudentsWithoutOgnp()[0].GetId(), unenrolledStudent.GetId());
        }

        [Test]
        public void EnrollStudentInSameFacultyOgnp_ThrowsException()
        {
            Assert.Catch<OgnpException>(() => { 
                var ognp = _isuExtraService.AddOgnp('M');
                var groupWithSchedule = _isuExtraService.AddGroup("M3213", GenerateSchedule(DateTime.Now)); 
                var stream = new Stream("Cyber 2.0", groupWithSchedule); 
                ognp.AddStream(stream); 
                var student = _isuExtraService.AddStudent(groupWithSchedule.GetGroup, "Vasia Pupkin"); 
                _isuExtraService.EnrollStudentInOgnp(student, ognp, "Cyber 2.0"); });
           
            
        }

        [Test]
        public void EnrollStudentWithCrossingSchedule_ThrowsException()
        {
            var sameDateTime = new DateTime(2021, 1, 1);
            var ognp = _isuExtraService.AddOgnp('M');
            var groupWithSchedule = _isuExtraService.AddGroup("M3213", GenerateSchedule(sameDateTime));
            var stream = new Stream("Cyber 2.0", groupWithSchedule);
            ognp.AddStream(stream);
            var groupWithScheduleFromAnotherFaculty =
                _isuExtraService.AddGroup("Y3201", GenerateSchedule(sameDateTime));
            var student = _isuExtraService.AddStudent(groupWithScheduleFromAnotherFaculty.GetGroup, "Vasia Pupkin");
            Assert.Catch<OgnpException>(() =>
            {
                _isuExtraService.EnrollStudentInOgnp(student, ognp, "Cyber 2.0");
            });
            
        }

        private Schedule GenerateSchedule(DateTime dateTime)
        {
            
            Lesson newLesson = new Lesson(dateTime, dateTime, 228, "Povishev V.V", "EVM");
            Schedule newSchedule = new Schedule();
            newSchedule.AddClass(newLesson);
            return newSchedule;
        }

    }
}