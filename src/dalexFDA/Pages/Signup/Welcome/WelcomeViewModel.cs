using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using dalexFDA.Abstractions;
using PropertyChanged;
using Xamarin.Forms;

namespace dalexFDA
{
    [AddINotifyPropertyChangedInterface]
    public class WelcomeViewModel : BaseViewModel
    {
        public IErrorManager ErrorManager;

        public ObservableCollection<WelcomeItem> WelcomeItems { get; set; }
        public bool CancelAutomaticPaging { get; set; }

        public Command MakeAnotherCommand { get; set; }
        public Command SkipCommand { get; set; }

        public WelcomeViewModel(IErrorManager errorManager)
        {
            ErrorManager = errorManager;

            MakeAnotherCommand = new Command(async () => await ExecuteMakeAnotherCommand());
            SkipCommand = new Command(async () => await ExecuteSkipCommand());
        }

        public async override void Init(object initData)
        {
            base.Init(initData);

            try
            {
                SetupWelcomeItems();
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }

        private async Task ExecuteMakeAnotherCommand()
        {
            try
            {
                await CoreMethods.PushPageModel<LoginViewModel>();
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }

        private async Task ExecuteSkipCommand()
        {
            try
            {
                await CoreMethods.PushPageModel<LoginViewModel>();
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }

        private void SetupWelcomeItems()
        {
            WelcomeItems = new ObservableCollection<WelcomeItem>
            {
                new WelcomeItem
                {
                    Id=1,
                    Header="Control Your Investments",
                    Image="CollectInvestment.png",
                    Detail="360 degree view of how your investments are performing along with other useful info"
                },
                new WelcomeItem
                {
                    Id=2,
                    Header="Best Interest Rates",
                    Image="Report.png",
                    Detail="We offer the best interest rates in Africa. Our portfolio speaks much about it."
                },
                new WelcomeItem
                {
                    Id=3,
                    Header="Real-time Reports",
                    Image="Realtime.png",
                    Detail="Get instant notification on every update on your investments right on your phone."
                }
            };
        }
    }
}
