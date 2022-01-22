using System;

using Isu.Tools;

namespace Isu.Services.Groups
{
    public class GroupName
    {
        public GroupName(string groupName)
        {
            CheckIfNameIsCorrect(groupName);
        }

        public string Name { get; private set; }
        public CourseNumber CourseNumber { get; private set; }
        private void CheckIfNameIsCorrect(string groupName)
        {
            if (groupName.Length != Constants.LengthOfGroupName || !char.IsLetter(groupName[0]) || groupName[1] != Constants.FirstDigitInGroupName)
                throw new IsuException("Incorrect group name");
            try
            {
                CourseNumber = new CourseNumber(Convert.ToInt32(groupName.Substring(2, 1)));
            }
            catch (Exception)
            {
                throw new IsuException("Incorrect group name");
            }

            Name = groupName;
        }
    }
}