using System;

namespace IsuExtra.OGNP
{
    public class Lesson
    {
        private DateTime _beginningTime;
        private DateTime _endingTime;
        private int _auditoryNumber;
        private string _teacherName;
        private string _className;

        public Lesson(DateTime beginning, DateTime ending, int auditoryNumber, string teacherName, string className)
        {
            _beginningTime = beginning;
            _endingTime = ending;
            _auditoryNumber = auditoryNumber;
            _teacherName = teacherName;
            _className = className;
        }

        public DateTime GetBeginningTime() => _beginningTime;
    }
}