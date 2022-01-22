using Isu.Tools;

namespace Isu.Services.Groups
{
    public class CourseNumber
    {
        public CourseNumber(int number)
        {
            if (number > Constants.MaxCourseNumber || number < Constants.MinCourseNumber)
            {
                throw new IsuException("Incorrect course number");
            }

            Course = number;
        }

        public int Course { get; private set; }
    }
}