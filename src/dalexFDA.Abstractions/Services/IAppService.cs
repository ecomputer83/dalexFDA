using System;
namespace dalexFDA.Abstractions
{
    public interface IAppService
    {
        object CurrentNavigation { get; set; }
        void StartMainFlow();
        void StartElectronicFundTransfer();
        void StartManualBankDeposit();
        void StartAccountStatements();
        void StartTransferHistory();

        void Logout();
    }
}
