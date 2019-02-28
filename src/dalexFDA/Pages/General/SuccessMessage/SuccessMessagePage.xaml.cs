using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace dalexFDA
{
	public partial class SuccessMessagePage : ContentPage
	{
		public SuccessMessagePage ()
		{
			InitializeComponent ();
		}

        private void Handle_Close(object sender, EventArgs e)
        {
            var palmLeaf = (Color)Application.Current.Resources["PalmLeaf"];
            var white = Color.FromHex("#FFF");
            closeButton.TextColor = palmLeaf;
            closeButton.BackgroundColor = white;

            var model = this.BindingContext as SuccessMessageViewModel;
            model.Close.Execute(null);
        }
    }
}