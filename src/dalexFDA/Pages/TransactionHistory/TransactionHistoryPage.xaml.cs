using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace dalexFDA.Core
{
    public partial class TransactionHistoryPage : ContentPage
    {
        public TransactionHistoryPage()
        {
            InitializeComponent();
        }

        async void Handle_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            var model = this.BindingContext as TransactionHistoryItemViewModel;
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
