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
                    pagerdot1.Style = Application.Current.Resources["PagerInactiveButton"] as Style;
                    pagerdot2.Style = pagerdot1.Style;
                    pagerdot3.Style = pagerdot1.Style;

                    switch (item.Id)
                    {
                        case 1:
                            pagerdot1.Style = Application.Current.Resources["PagerActiveButton"] as Style;
                            container.BackgroundColor = (Color)Application.Current.Resources["PalmLeaf"];
                            break;
                        case 2:
                            pagerdot2.Style = Application.Current.Resources["PagerActiveButton"] as Style;
                            container.BackgroundColor = Color.FromHex("#3185FC");
                            break;
                        case 3:
                            pagerdot3.Style = Application.Current.Resources["PagerActiveButton"] as Style;
                            container.BackgroundColor = Color.FromHex("#F9DC5C");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        void HandleNext_Tapped(object sender, System.EventArgs e)
        {
            try
            {
                if (items.Position < 2)
                {
                    items.Position++;
                }
                model.CancelAutomaticPaging = true;
                if (items.Position == 2)
                {
                    //NextButton.IsVisible = SkipButton.IsVisible = false;
                    //GetStartedButton.IsVisible = LoginButton.IsVisible = true;
                }
            }
            catch (Exception ex)
            {

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
                Device.StartTimer(TimeSpan.FromSeconds(3), HandleAutomaticPaging);
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
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
