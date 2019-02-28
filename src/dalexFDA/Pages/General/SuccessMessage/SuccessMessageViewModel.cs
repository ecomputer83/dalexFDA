using dalexFDA.Abstractions;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace dalexFDA
{
    [AddINotifyPropertyChangedInterface]
    public class SuccessMessageViewModel : BaseViewModel
    {
        readonly IErrorManager ErrorManager;

        public string Message { get; set; }

        public Command Close { get; set; }

        Nav Data;
        public class Nav
        {
            public string Message { get; set; }
        }

        public SuccessMessageViewModel(IErrorManager ErrorManager)
        {
            this.ErrorManager = ErrorManager;

            Close = new Command(async () => await ExecuteClose());
        }

        public async override void Init(object initData)
        {
            base.Init(initData);

            try
            {
                Data = initData as Nav;
                if (Data != null)
                    Message = Data.Message;
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }

        private async Task ExecuteClose()
        {
            try
            {
                await CoreMethods.PopPageModel(true);
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }
    }
}
