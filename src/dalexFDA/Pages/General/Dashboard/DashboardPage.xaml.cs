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

        void Handle_Appearing(object sender, System.EventArgs e)
        {
            try
            {
                var cell = sender as ViewCell;
                var customcontrol = cell.View as InvestmentView;
                var model = cell.BindingContext as DashboardItemViewModel;

                //InnerSection.TranslateTo(0, 10, 40, Easing.SinOut);

                Debug.WriteLine($"Appearing InnerSection.X: {InnerSection.X}");
                Debug.WriteLine($"Appearing InnerSection.Y: {InnerSection.Y}");

                Debug.WriteLine("Cell {0} is coming onscreen", model.Investment.Id);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }


        void Handle_Disappearing(object sender, System.EventArgs e)
        {
            try
            {
                var cell = sender as ViewCell;
                var customcontrol = cell.View as InvestmentView;
                var model = cell.BindingContext as DashboardItemViewModel;

                //InnerSection.TranslateTo(1, 0, 30, Easing.SinOut);

                Debug.WriteLine($"Disappearing InnerSection.X: {InnerSection.X}");
                Debug.WriteLine($"Disappearing InnerSection.Y: {InnerSection.Y}");

                Debug.WriteLine("Cell {0} is going offscreen", model.Investment.Id);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
