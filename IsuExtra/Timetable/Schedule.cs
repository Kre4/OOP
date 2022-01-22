using System.Collections.Generic;
using IsuExtra.OGNP;

namespace IsuExtra.Timetable
{
    public class Schedule
    {
        private readonly List<Lesson> _schedule = new List<Lesson>();

        public void AddClass(Lesson newLesson)
        {
            _schedule.Add(newLesson);
        }

        public List<Lesson> GetSchedule() => _schedule;

        public bool HasAnyCrossing(Schedule other)
        {
            var result = false;

            _schedule.ForEach(lesson =>
            {
                other._schedule.ForEach(otherLesson =>
                {
                    if (lesson.GetBeginningTime().Equals(otherLesson.GetBeginningTime()))
                    {
                        result = true;
                    }
                });
            });
            return result;
        }
    }
}