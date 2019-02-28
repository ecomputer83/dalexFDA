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
        void StartDashboard();
        void StartTransferHistory();
        void StartContactChange();
        void Logout();
    }
}
