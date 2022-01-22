using System;
using UIReports.ConsoleUI.Tools;

namespace UIReports.ConsoleUI
{
    public class TasksActivity : UserConsoleActivity
    {
        public override void ShowActivityContent()
        {
            Console.WriteLine(Constant.TasksActivityMessage);
            int answer = this.RequestIntChoice(Constant.TaskActivityAvailableInterval);
            throw new FrediCatsException("Shouldn't be implemented :)");
        }
    }
}