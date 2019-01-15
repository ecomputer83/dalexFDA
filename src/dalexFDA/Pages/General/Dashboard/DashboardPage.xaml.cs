using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;
using Zenith.Abstractions;

namespace Zenith
{
    public partial class DashboardPage : ContentPage
    {
        DashboardViewModel model => this.BindingContext as DashboardViewModel;

        public DashboardPage()
        {
            InitializeComponent();
        }

        void Handle_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            var list = sender as ListView;
            list.SelectedItem = null;
        }






    }
}
