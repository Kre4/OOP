using System;
using System.Collections;
using System.Collections.Generic;
using ReportsServerApi.PublicAccess;
using UIReports.ConsoleUI.DI;


namespace UIReports.ConsoleUI.Tools
{
    public static class Constant
    {
        public const string GoodByeMessage = "Good luck!";

        public const string StarterActivityMessage =
            "Hello buddy!\nChoose an option\n1) Employees    2) Tasks    3) Reports";

        public static readonly Interval StarterActivityAvailableInterval = new Interval(1, 3);

        public const string EmployeeActivityMessage = 
            "1) Get All     2) Get employee by id    3) Create     4) Update   5) Delete";

        public static readonly Interval EmployeeActivityAvailableInterval = new Interval(1, 5);

        public const string TasksActivityMessage =
            "1) Get All     2) Get by   3) Create   4) Update   5) Delete";

        public static readonly Interval TaskActivityAvailableInterval = new Interval(1, 5);

        public const string ReportsActivityMessage =
            "1) Create weekly   2) Get tasks    3) Get weekly   4) Update weekly";

        public static readonly Interval ReportsActivityAvailableInterval = new Interval(1, 4);
        public static readonly IServerApi ServerApi = new ProviderDI().GetServerApi();
        public static IReadOnlyCollection<string> QuitKeywords = new string[]{"q", "quit", "close"};
    }
}