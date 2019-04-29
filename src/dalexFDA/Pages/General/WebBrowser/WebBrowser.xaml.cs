using dalexFDA.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace dalexFDA
{
	public partial class WebBrowserPage : ContentPage
	{
        private IErrorManager ErrorManager;
        public WebBrowserPage ()
		{
			InitializeComponent ();

            ErrorManager = FreshMvvm.FreshIOC.Container.Resolve<IErrorManager>();
		}

        private void Handle_Close(object sender, EventArgs e)
        {
            var palmLeaf = (Color)Application.Current.Resources["PalmLeaf"];
            var white = Color.FromHex("#FFF");
            cancelButton.TextColor = palmLeaf;
            cancelButton.BackgroundColor = white;

            var model = this.BindingContext as WebBrowserViewModel;
            model.Close.Execute(null);
        }

        private void Handle_Done(object sender, EventArgs e)
        {
            var palmLeaf = (Color)Application.Current.Resources["PalmLeaf"];
            var white = Color.FromHex("#FFF");
            doneButton.TextColor = palmLeaf;
            doneButton.BackgroundColor = white;

            var model = this.BindingContext as WebBrowserViewModel;
            model.Done.Execute(null);
        }

        private async void WebView_Navigated(object sender, WebNavigatedEventArgs e)
        {
            try
            {
                if (
                    (e.Result == WebNavigationResult.Failure) ||
                    (e.Result == WebNavigationResult.Failure)
                )
                {
                    App.DisplayNetworkErrorPrompt(this);
                }
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }
    }
}