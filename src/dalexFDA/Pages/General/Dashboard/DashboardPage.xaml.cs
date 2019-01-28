using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;
using dalexFDA.Abstractions;

namespace dalexFDA
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
            var model = e.Item as DashboardItemViewModel;
            if (model != null)
                model.ViewDetail.Execute(null);
            list.SelectedItem = null;
        }






    }
}
