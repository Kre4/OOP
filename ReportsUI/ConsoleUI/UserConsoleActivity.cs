using System;
using System.Linq;
using UIReports.ConsoleUI.Tools;
using UIReports.UI;

namespace UIReports.ConsoleUI
{
    public abstract class UserConsoleActivity : UserActivity
    {
        protected int RequestIntChoice(Interval range)
        {
            string answer = Console.ReadLine();
            if (Constant.QuitKeywords.Contains(answer))
            {
                if (this is StarterActivity)
                {
                    Console.WriteLine(Constant.GoodByeMessage);
                    Environment.Exit(0);
                }

                this.StartActivity(new StarterActivity());
            }

            int intAnswer = -1;
            try
            {
                intAnswer = Convert.ToInt32(answer);
                if (intAnswer <= range.From && intAnswer >= range.To)
                {
                    
                }
            }
            catch (Exception e)
            {
                this.Refresh();
            }

            return intAnswer;
        }
    }
}