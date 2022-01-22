using System;
using Isu.Services;
using Isu.Services.Groups;
using Isu.Tools;

namespace Isu
{
    internal class Program
    {
        private static void Main()
        {
            var isuService = new IsuService();
            Group m3203 = isuService.AddGroup("M3203");
            for (int i = 0; i < 20; ++i)
            {
                isuService.AddStudent(m3203, "Student with bla-bla name" + i.ToString());
            }

            Group m3204 = isuService.AddGroup("M3204");
            var student = isuService.AddStudent(m3204, "Big Boii");
            isuService.ChangeStudentGroup(student, m3203);
            if (isuService.FindStudent("Big Boii").GetGroup().Equals(m3203))
                Console.WriteLine("lol");
        }
    }
}
