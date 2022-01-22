using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using ReportsDAL.Entities;
using UIReports.ConsoleUI;
using UIReports.ConsoleUI.Tools;

namespace UIReports
{
    class Program
    {
        static void Main(string[] args)
        {
            
            var activity = new StarterActivity();
            activity.ShowActivityContent();
        }
    }
}