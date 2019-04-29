using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace dalexFDA
{
    public partial class NotificationPage : ContentPage
    {
        public NotificationPage()
        {
            InitializeComponent();
        }

        async void Handle_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            var model = this.BindingContext as NotificationItemViewModel;
            try
            {
                list.SelectedItem = null;
            }
            catch (Exception ex)
            {
                if (model != null)
                {
                    //await model.ErrorManager.DisplayErrorMessageAsync(ex);
                }
            }
        }
    }
}
