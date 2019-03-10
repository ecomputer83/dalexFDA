using dalexFDA.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;

namespace dalexFDA.Core
{
    public partial class FormDatePicker : ContentView
    {
        public FormDatePicker()
        {
            InitializeComponent();
        }

        #region Name

        public static readonly BindableProperty NameProperty = BindableProperty.Create("Name", typeof(string), typeof(FormDatePicker), default(string));

        public string Name
        {
            get { return (string)GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }

        private void SetName()
        {
            try
            {
            }
            catch { }
        }

        #endregion

        #region Label

        public static readonly BindableProperty LabelProperty = BindableProperty.Create("Label", typeof(string), typeof(FormDatePicker), default(string));

        public string Label
        {
            get { return (string)GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }

        private void SetLabel()
        {
            try
            {
                label.Text = Label;
            }
            catch { }
        }

        #endregion

        #region Date

        public static readonly BindableProperty DateProperty = BindableProperty.Create("Date", typeof(DateTime), typeof(FormDatePicker), DateTime.Today, BindingMode.TwoWay);

        public DateTime Date
        {
            get { return (DateTime)GetValue(DateProperty); }
            set { SetValue(DateProperty, value); }
        }

        private void SetDate()
        {
            try
            {
                datePicker.Date = Date;
            }
            catch { }
        }

        #endregion

        #region Placeholder

        public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create("Placeholder", typeof(string), typeof(FormDatePicker));

        public string Placeholder
        {
            get { return (string)GetValue(PlaceholderProperty); }
            set { SetValue(PlaceholderProperty, value); }
        }

        private void SetPlaceholder()
        {
            try
            {
                datePicker.Placeholder = Placeholder;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Placeholder ===== {ex.Message}");
            }
        }

        #endregion

        #region HasError

        public static readonly BindableProperty HasErrorProperty = BindableProperty.Create("HasError", typeof(bool), typeof(FormDatePicker), false, BindingMode.TwoWay);

        public bool HasError
        {
            get { return (bool)GetValue(HasErrorProperty); }
            set { SetValue(HasErrorProperty, value); }
        }

        private void SetHasError()
        {
            try
            {
                Color color;
                bool isVisible;

                if (HasError)
                {
                    color = (Color)Application.Current.Resources["Kucrimson"];
                    isVisible = true;
                }
                else
                {
                    color = (Color)Application.Current.Resources["PalmLeaf"];
                    isVisible = false;
                }

                container.BackgroundColor = color;
                caption.IsVisible = isVisible;
            }
            catch { }
        }

        #endregion

        #region ErrorMessage

        public static readonly BindableProperty ErrorMessageProperty = BindableProperty.Create("ErrorMessage", typeof(string), typeof(FormDatePicker), default(string), BindingMode.TwoWay);

        public string ErrorMessage
        {
            get { return (string)GetValue(ErrorMessageProperty); }
            set { SetValue(ErrorMessageProperty, value); }
        }

        private void SetErrorMessage()
        {
            try
            {
                caption.Text = ErrorMessage;
            }
            catch { }
        }

        #endregion

        #region PickerColor

        public static readonly BindableProperty PickerColorProperty = BindableProperty.Create("PickerColor", typeof(Color), typeof(FormDatePicker), default(Color));

        public Color PickerColor
        {
            get { return (Color)GetValue(PickerColorProperty); }
            set { SetValue(PickerColorProperty, value); }
        }

        private void SetPickerColor()
        {
            try
            {
                innerContainer.BackgroundColor = PickerColor;
            }
            catch { }
        }

        #endregion

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == NameProperty.PropertyName)
                SetName();

            if (propertyName == LabelProperty.PropertyName)
                SetLabel();

            if (propertyName == PlaceholderProperty.PropertyName)
                SetPlaceholder();

            if (propertyName == HasErrorProperty.PropertyName)
                SetHasError();

            if (propertyName == ErrorMessageProperty.PropertyName)
                SetErrorMessage();

            if (propertyName == PickerColorProperty.PropertyName)
                SetPickerColor();
        }

        void Handle_DateSelected(object sender, Xamarin.Forms.DateChangedEventArgs e)
        {
            this.Date = datePicker.Date;

            var nav = new ValidationCommandNav { Name = Name };

            if (this.BindingContext is ManualDepositViewModel manualDeposit)
                manualDeposit.Validate.Execute(nav);

            if (this.BindingContext is UpdateKYCAccountViewModel updateProfileViewModel)
                updateProfileViewModel.Validate.Execute(nav);
        }

        void Handle_Tapped(object sender, System.EventArgs e)
        {
            try
            {
                datePicker.Focus();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{ex.Message}");
            }
        }
    }
}
