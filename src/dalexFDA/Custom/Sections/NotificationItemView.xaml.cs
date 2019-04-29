using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;

namespace dalexFDA
{
    public partial class NotificationItemView : ContentView
    {
        public NotificationItemView()
        {
            InitializeComponent();
        }

        #region Title

        public static readonly BindableProperty TitleProperty = BindableProperty.Create("Title", typeof(string), typeof(NotificationItemView), default(string));

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        private void SetTitle()
        {
            try
            {
                title.Text = Title;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"=======IdProperty=====\n{ex.Message}");
            }
        }

        #endregion

        #region Ndate

        public static readonly BindableProperty NdateProperty = BindableProperty.Create("Ndate", typeof(DateTime), typeof(NotificationItemView), default(DateTime));

        public DateTime Ndate
        {
            get { return (DateTime)GetValue(NdateProperty); }
            set { SetValue(NdateProperty, value); }
        }

        private void SetNdate()
        {
            try
            {
                ndate.Text = Ndate.ToShortDateString();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"=======PaymentType=====\n{ex.Message}");
            }
        }

        #endregion

        #region Description

        public static readonly BindableProperty DescriptionProperty = BindableProperty.Create("Description", typeof(string), typeof(NotificationItemView), default(string));

        public string Description
        {
            get { return (string)GetValue(DescriptionProperty); }
            set { SetValue(DescriptionProperty, value); }
        }

        private void SetDescription()
        {
            try
            {
                desc.Text = Description;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"=======DocumentType=====\n{ex.Message}");
            }
        }

        #endregion


        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == TitleProperty.PropertyName)
                SetTitle();

            if (propertyName == NdateProperty.PropertyName)
                SetNdate();

            if (propertyName == DescriptionProperty.PropertyName)
                SetDescription();
        }
    }
}
