using Isu.Services;
using Isu.Services.Groups;
using Isu.Services.Students;
using Isu.Tools;
using NUnit.Framework;

namespace Isu.Tests
{
    public class Tests
    {
        
        private IsuService _isuService;

        [SetUp]
        public void Setup()
        {
            _isuService = new IsuService();
        }

        [Test]
        public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent()
        {
            Group m3203 = _isuService.AddGroup("M3203");
            Student student = _isuService.AddStudent(m3203, "Aleksandr Adolfovich");
            if (student.GetGroup() == null)
                Assert.Fail("Student hasn't got group");
            if (!student.GetGroup().Equals(m3203))
                Assert.Fail("Student has incorrect group");
            if (m3203.FindStudent(student.GetName()) == null)
                Assert.Fail("Student hasn't been added to group");
            
        }

        [Test]
        public void ReachMaxStudentPerGroup_ThrowException()
        {
            Assert.Catch<IsuException>(() =>
            {
                Group m3203 = _isuService.AddGroup("M3203");
                for (int i = 0; i < 31; ++i)
                {
                    _isuService.AddStudent(m3203, "Student with bla-bla name" + i.ToString());
                }
            });
        }

        [Test]
        public void CreateGroupWithInvalidName_ThrowException()
        {
            Assert.Catch<IsuException>(() =>
            {
                Group incorrect = _isuService.AddGroup("M3503");
                
            });
            Assert.Catch<IsuException>(() =>
            {
                Group incorrect = _isuService.AddGroup("33203");
                
            });
        }

        [Test]
        public void TransferStudentToAnotherGroup_GroupChanged()
        {
            Assert.Catch<IsuException>(() =>
            {
                Group m3203 = _isuService.AddGroup("M3203");
                for (int i = 0; i < 31; ++i)
                {
                    _isuService.AddStudent(m3203, "Student with bla-bla name" + i);
                }

                Group m3204 = _isuService.AddGroup("M3204");
                var student = _isuService.AddStudent(m3204, "Big Boii");
                _isuService.ChangeStudentGroup(student, m3203);
            }); 
            
            
                Group m3205 = _isuService.AddGroup("M3205");
                for (int i = 0; i < 2; ++i)
                {
                    _isuService.AddStudent(m3205, "Student with bla-bla name" + i.ToString());
                }

                Group m3202 = _isuService.AddGroup("M3202");
                var student = _isuService.AddStudent(m3202, "Big Girl");
                _isuService.ChangeStudentGroup(student, m3205);
                if (student.GetGroup().Equals(m3202))
                    Assert.Fail("Incorrect group, should be m3202");
                if (m3202.FindStudent("Big Girl") != null)
                    Assert.Fail("Student haven't been added");
            
        }
    }
}