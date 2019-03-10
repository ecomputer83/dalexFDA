using System;
using System.Collections.Generic;
using System.Diagnostics;
using dalexFDA.Abstractions;
using Xamarin.Forms;

namespace dalexFDA.Core
{
    public partial class FormPhoneEntry : ContentView
    {
        public FormPhoneEntry()
        {
            InitializeComponent();            
        }

        private const string phone_extension = "PhoneExtension";
        private const string phone_number = "PhoneNumber";

        #region Label

        public static readonly BindableProperty LabelProperty = BindableProperty.Create("Label", typeof(string), typeof(FormPhoneEntry), default(string));

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
                label.IsVisible = !string.IsNullOrEmpty(Label);
            }
            catch { }
        }

        #endregion

        #region PhoneExtension

        public static readonly BindableProperty PhoneExtensionProperty = BindableProperty.Create("PhoneExtension", typeof(string), typeof(FormPhoneEntry), default(string), BindingMode.TwoWay);

        public string PhoneExtension
        {
            get { return (string)GetValue(PhoneExtensionProperty); }
            set { SetValue(PhoneExtensionProperty, value); }
        }

        private void SetPhoneExtension()
        {
            try
            {
                phoneExt.Text = PhoneExtension;
            }
            catch { }
        }

        #endregion

        #region PhoneNumber

        public static readonly BindableProperty PhoneNumberProperty = BindableProperty.Create("PhoneNumber", typeof(string), typeof(FormPhoneEntry), default(string), BindingMode.TwoWay);

        public string PhoneNumber
        {
            get { return (string)GetValue(PhoneNumberProperty); }
            set { SetValue(PhoneNumberProperty, value); }
        }

        private void SetPhoneNumber()
        {
            try
            {
                phoneNum.Text = PhoneNumber;
            }
            catch { }
        }

        #endregion

        #region PhoneExtensionHasError

        public static readonly BindableProperty PhoneExtensionHasErrorProperty = BindableProperty.Create("PhoneExtensionHasError", typeof(bool), typeof(FormPhoneEntry), false, BindingMode.TwoWay);

        public bool PhoneExtensionHasError
        {
            get { return (bool)GetValue(PhoneExtensionHasErrorProperty); }
            set { SetValue(PhoneExtensionHasErrorProperty, value); }
        }

        private void SetPhoneExtensionHasError()
        {
            try
            {
                extContainer.BackgroundColor = PhoneExtensionHasError ? (Color)Application.Current.Resources["Kucrimson"] : (Color)Application.Current.Resources["PalmLeaf"];
                caption.IsVisible = PhoneExtensionHasError;
            }
            catch { }
        }

        #endregion

        #region PhoneNumberHasError

        public static readonly BindableProperty PhoneNumberHasErrorProperty = BindableProperty.Create("PhoneNumberHasError", typeof(bool), typeof(FormPhoneEntry), false, BindingMode.TwoWay);

        public bool PhoneNumberHasError
        {
            get { return (bool)GetValue(PhoneNumberHasErrorProperty); }
            set { SetValue(PhoneNumberHasErrorProperty, value); }
        }

        private void SetPhoneNumberHasError()
        {
            try
            {
                numContainer.BackgroundColor = PhoneNumberHasError ? (Color)Application.Current.Resources["Kucrimson"] : (Color)Application.Current.Resources["PalmLeaf"];
                caption.IsVisible = PhoneNumberHasError;
            }
            catch { }
        }

        #endregion

        #region ErrorMessage

        public static readonly BindableProperty ErrorMessageProperty = BindableProperty.Create("ErrorMessage", typeof(string), typeof(FormPhoneEntry), default(string), BindingMode.TwoWay);

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
                caption.IsVisible = !string.IsNullOrEmpty(ErrorMessage);
            }
            catch { }
        }

        #endregion

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            
            if (propertyName == PhoneExtensionProperty.PropertyName)
                SetPhoneExtension();

            if (propertyName == PhoneNumberProperty.PropertyName)
                SetPhoneNumber();
             
            if (propertyName == LabelProperty.PropertyName)
                SetLabel();

            if (propertyName == PhoneExtensionHasErrorProperty.PropertyName)
                SetPhoneExtensionHasError();
            
            if (propertyName == PhoneNumberHasErrorProperty.PropertyName)
                SetPhoneNumberHasError();
            
            if (propertyName == ErrorMessageProperty.PropertyName)
                SetErrorMessage();
        }

        private void PhoneExt_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                this.PhoneExtension = e.NewTextValue;

                var nav = new ValidationCommandNav { Name = phone_extension };

                if (this.BindingContext is ExistingUserSignupViewModel existingUserSignupViewModel)
                    existingUserSignupViewModel.Validate.Execute(nav);

                if (this.BindingContext is NewUserSignupViewModel newUserSignupViewModel)
                    newUserSignupViewModel.Validate.Execute(nav);

                if (this.BindingContext is LoginViewModel loginViewModel)
                    loginViewModel.Validate.Execute(nav);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception: {ex.Message}");
            }
        }
        
        void PhoneNum_TextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            try
            {
                this.PhoneNumber = e.NewTextValue;

                var nav = new ValidationCommandNav { Name = phone_number };

                if (this.BindingContext is ExistingUserSignupViewModel existingUserSignupViewModel)
                    existingUserSignupViewModel.Validate.Execute(nav);
                
                if (this.BindingContext is NewUserSignupViewModel newUserSignupViewModel)
                    newUserSignupViewModel.Validate.Execute(nav);
                
                if (this.BindingContext is LoginViewModel loginViewModel)
                    loginViewModel.Validate.Execute(nav);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception: {ex.Message}");
            }
        }

        void Handle_Unfocused(object sender, Xamarin.Forms.FocusEventArgs e)
        {
            try
            {
                if (this.BindingContext is ExistingUserSignupViewModel model)
                    model.GetUserDetailsFromPhoneNumber.Execute(null);

                if (this.BindingContext is NewUserSignupViewModel newUserSignupViewModel)
                    newUserSignupViewModel.GetUserWithPhoneNumber.Execute(null);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception: {ex.Message}");
            }
        }
    }
}
