using Isu.Services.Groups;

namespace IsuExtra.Timetable
{
    public class GroupWithSchedule
    {
        private Group _group;
        private Schedule _schedule;

        public GroupWithSchedule(Group group, Schedule schedule)
        {
            _group = group;
            _schedule = schedule;
        }

        public Group GetGroup => _group;
        public Schedule GetSchedule => _schedule;
    }
}