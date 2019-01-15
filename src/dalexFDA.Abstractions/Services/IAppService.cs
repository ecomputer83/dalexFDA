using System;
namespace Zenith.Abstractions
{
    public interface IAppService
    {
        object CurrentNavigation { get; set; }
        void StartMainFlow();
        void StartOverviewFlow();
        void StartTransferFlow();
        void Logout();
    }
}
