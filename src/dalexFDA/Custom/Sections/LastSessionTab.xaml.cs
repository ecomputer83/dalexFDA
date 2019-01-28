using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace dalexFDA
{
    public partial class LastSessionTab : ContentView
    {
        public LastSessionTab()
        {
            InitializeComponent();
        }

        #region LastSession

        public static readonly BindableProperty LastSessionProperty = BindableProperty.Create("LastSession", typeof(string), typeof(LastSessionTab));

        public string LastSession
        {
            get { return (string)GetValue(LastSessionProperty); }
            set { SetValue(LastSessionProperty, value); }
        }

        private void SetLastSession()
        {
            try
            {
                data.Text = LastSession;
            }
            catch { }
        }

        #endregion

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == LastSessionProperty.PropertyName)
            {
                SetLastSession();
            }
        }
    }
}
