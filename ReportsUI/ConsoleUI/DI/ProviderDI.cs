using ReportsServerApi.PublicAccess;

namespace UIReports.ConsoleUI.DI
{
    public class ProviderDI : IProviderDI
    {
        public IServerApi GetServerApi() => new ServerApi();
    }
}