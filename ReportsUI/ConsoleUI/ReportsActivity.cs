using System;
using UIReports.ConsoleUI.Tools;
using Constant = UIReports.ConsoleUI.Tools.Constant;

namespace UIReports.ConsoleUI
{
    public class ReportsActivity : UserConsoleActivity
    {
        public override void ShowActivityContent()
        {
            Console.WriteLine(Constant.ReportsActivityMessage);
            int answer = this.RequestIntChoice(Constant.ReportsActivityAvailableInterval);
            throw new FrediCatsException("Shouldn't be implemented :)");
        }
    }
}