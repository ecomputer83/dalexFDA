using System;
namespace dalexFDA.Abstractions
{
    public interface IAppService
    {
        object CurrentNavigation { get; set; }
        void StartMainFlow();
        void StartDashboard();
        void StartElectronicFundTransfer();
        void StartManualBankDeposit();
        void StartAccountStatements();
        void StartTransferHistory();
        void StartContactChange();
        void StartNotification();
        void StartRO();
        void StartMyProfile();
        void StartEnquiry();
        void StartFeedback();
        void StartKYCUpdate();
        void Logout();
    }
}
