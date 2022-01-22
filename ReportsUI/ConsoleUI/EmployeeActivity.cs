using System;
using UIReports.ConsoleUI.Tools;

namespace UIReports.ConsoleUI
{
    public class EmployeeActivity : UserConsoleActivity
    {
        public override void ShowActivityContent()
        {
            Console.WriteLine(Constant.EmployeeActivityMessage);
            int answer = RequestIntChoice(Constant.EmployeeActivityAvailableInterval);
            throw new FrediCatsException("Shouldn't be implemented :)");
        }
    }
}