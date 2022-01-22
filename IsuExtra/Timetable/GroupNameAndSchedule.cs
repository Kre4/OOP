namespace IsuExtra.Timetable
{
    public class GroupNameAndSchedule
    {
        public GroupNameAndSchedule(string name, Schedule schedule)
        {
            Name = name;
            Schedule = schedule;
        }

        public string Name { get; }
        public Schedule Schedule { get; }
    }
}