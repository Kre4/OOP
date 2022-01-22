using Banks.DTO;

namespace Banks.UI
{
    public interface IUserInterface
    {
        void ShowMainMenu();
        RegistrationData RequestRegistrationData();

        void TransferMoney();
    }
}