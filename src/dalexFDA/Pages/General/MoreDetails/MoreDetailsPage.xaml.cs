using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace dalexFDA.Core
{
	public partial class MoreDetailsPage : ContentPage
	{
		public MoreDetailsPage ()
		{
			InitializeComponent ();
		}

        private void Handle_Close(object sender, EventArgs e)
        {
            var palmLeaf = (Color)Application.Current.Resources["PalmLeaf"];
            var white = Color.FromHex("#FFF");
            closeButton.TextColor = palmLeaf;
            closeButton.BackgroundColor = white;

            var model = this.BindingContext as MoreDetailsViewModel;
            model.Close.Execute(null);
        }
	}
}