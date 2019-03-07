using System;
using System.Collections.Generic;
using System.Diagnostics;
using dalexFDA.Abstractions;
using Xamarin.Forms;

namespace dalexFDA
{
    public partial class WelcomePage : ContentPage
    {
        WelcomeViewModel model => this.BindingContext as WelcomeViewModel;

        public WelcomePage()
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);

            this.Appearing += Handle_Appearing; ;
        }

        void Handle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            try
            {
                if (e.SelectedItem is WelcomeItem item)
                {
                    switch (item.Id)
                    {
                        case 1:
                            pagerdot1.BackgroundColor = Color.White;
                            pagerdot1.BorderColor = Color.White;
                            pagerdot2.BackgroundColor = Color.Transparent;
                            pagerdot2.BorderColor = Color.White;
                            pagerdot3.BackgroundColor = Color.Transparent;
                            pagerdot3.BorderColor = Color.White;
                            container.BackgroundColor = (Color)Application.Current.Resources["PalmLeaf"];
                            skipLabel.Text = "SKIP";
                            break;
                        case 2:
                            pagerdot1.BackgroundColor = Color.White;
                            pagerdot1.BorderColor = Color.White;
                            pagerdot2.BackgroundColor = Color.White;
                            pagerdot2.BorderColor = Color.White;
                            pagerdot3.BackgroundColor = Color.Transparent;
                            pagerdot3.BorderColor = Color.White;
                            container.BackgroundColor = Color.FromHex("#3185FC");
                            skipLabel.Text = "SKIP";
                            break;
                        case 3:
                            pagerdot1.BackgroundColor = Color.White;
                            pagerdot1.BorderColor = Color.White;
                            pagerdot2.BackgroundColor = Color.White;
                            pagerdot2.BorderColor = Color.White;
                            pagerdot3.BackgroundColor = Color.White;
                            pagerdot3.BorderColor = Color.White;
                            container.BackgroundColor = Color.FromHex("#F9DC5C");
                            skipLabel.Text = "NEXT";
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        void pagerdot1_Clicked(object sender, System.EventArgs e)
        {
            try
            {
                items.Position = 0;
                model.CancelAutomaticPaging = true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        void pagerdot2_Clicked(object sender, System.EventArgs e)
        {
            try
            {
                items.Position = 1;
                model.CancelAutomaticPaging = true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        void pagerdot3_Clicked(object sender, System.EventArgs e)
        {
            try
            {
                items.Position = 2;
                model.CancelAutomaticPaging = true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        void Handle_Appearing(object sender, EventArgs e)
        {
            try
            {
                Device.StartTimer(TimeSpan.FromSeconds(5), HandleAutomaticPaging);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        bool HandleAutomaticPaging()
        {
            Debug.WriteLine("timer called for scrolling pager");
            bool retVal = true;

            if (model != null)
            {
                if (model.CancelAutomaticPaging) return false;
            }

            try
            {
                items.Position = (items.Position == 2) ? 0 : items.Position + 1;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return retVal;
        }

        private void Skip_Tapped(object sender, EventArgs e)
        {
            try
            {
                items.Position = 2;
                model.CancelAutomaticPaging = true;
                model.SkipCommand.Execute(null);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
