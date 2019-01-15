using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Zenith
{
    public partial class ManualDepositPage : ContentPage
    {
        public ManualDepositPage()
        {
            InitializeComponent();
        }

        void Handle_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            var model = this.BindingContext as ManualDepositViewModel;
            if (model != null)
                model.ChangeBank.Execute(null);
        }

        void OnDateSelected(object sender, Xamarin.Forms.DateChangedEventArgs e)
        {

        }
    }
}
