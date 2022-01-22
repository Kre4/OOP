using System;
using UIReports.ConsoleUI.Tools;
using UIReports.UI;

namespace UIReports.ConsoleUI
{
    public class StarterActivity : UserConsoleActivity
    {
        public override void ShowActivityContent()
        {
            Console.WriteLine(Constant.StarterActivityMessage);
            int answer = this.RequestIntChoice(Constant.StarterActivityAvailableInterval);
            switch (answer)
            {
                case 1:
                    this.StartActivity(new EmployeeActivity());
                    break;
                case 2:
                    this.StartActivity(new TasksActivity());
                    break;
                case 3:
                    this.StartActivity(new ReportsActivity());
                    break;
            }
        }

        
    }
}