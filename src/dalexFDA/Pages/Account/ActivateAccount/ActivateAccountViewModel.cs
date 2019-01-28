using System;
using System.Threading.Tasks;
using PropertyChanged;

namespace dalexFDA
{
    public partial class ActivateAccountViewModel : BaseViewModel
    {
        public int LogoRow { get; set; }
        public int FormRow { get; set; }
        public int FormRowSpan { get; set; }
        public bool IsTipsVisible { get; set; }

        public async override void Init(object initData)
        {
            base.Init(initData);

            try
            {
                LogoRow = 0;
                FormRow = 1;
                FormRowSpan = 1;
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }

        private async Task ExecuteActivate()
        {
            try
            {
                await this.CoreMethods.DisplayAlert("Activate", "Activate", "OK");
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }

        private async Task ExecuteBack()
        {
            try
            {
                await this.CoreMethods.PopPageModel();
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }

        private async Task ExecuteSwipe()
        {
            try
            {
                FormRow = FormRow == 0 ? 1 : 0;
                FormRowSpan = FormRow == 0 ? 2 : 1;
                IsTipsVisible = FormRow == 0;
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }
    }
}
