using ReportsServerApi.PublicAccess;

namespace UIReports.ConsoleUI.DI
{
    public interface IProviderDI
    {
        public IServerApi GetServerApi();
    }
}