using System;
using System.Collections.Generic;
using System.Diagnostics;
using dalexFDA.Abstractions;
using Xamarin.Forms;

namespace dalexFDA
{
    public partial class FormEditor : ContentView
    {
        public FormEditor()
        {
            InitializeComponent();

            var tap = new TapGestureRecognizer();
            tap.Tapped += Tap_Tapped;
            this.GestureRecognizers.Add(tap);
        }

        void Tap_Tapped(object sender, EventArgs e)
        {
            try
            {
                if (!data.IsFocused)
                {
                    Device.BeginInvokeOnMainThread(() => data.Focus());
                }
            }
            catch { }
        }

        #region Name

        public static readonly BindableProperty NameProperty = BindableProperty.Create("Name", typeof(string), typeof(FormEditor), default(string));

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

        public static readonly BindableProperty LabelProperty = BindableProperty.Create("Label", typeof(string), typeof(FormEditor), default(string));

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

        #region Text

        public static readonly BindableProperty TextProperty = BindableProperty.Create("Text", typeof(string), typeof(FormEditor), default(string), BindingMode.TwoWay);

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        private void SetText()
        {
            try
            {
                data.Text = Text;
            }
            catch { }
        }

        #endregion

        #region Property - Amount

        public const string AmountPropertyName = "Amount";

        public double Amount
        {
            get
            {
                return (double)GetValue(AmountProperty);
            }
            set
            {
                SetValue(AmountProperty, value);
            }
        }

        public static readonly BindableProperty AmountProperty = BindableProperty.Create(
            AmountPropertyName,
               typeof(double),
               typeof(FormEditor),
                0.00,
            BindingMode.TwoWay, propertyChanging: (bindable, oldValue, newValue) =>
            {
                var control = (FormEditor)bindable;
                if (control != null && newValue != null)
                {
                    control.Amount = (double)newValue;
                }
            }
        );

        #endregion

        #region Placeholder

        public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create("Placeholder", typeof(string), typeof(FormEditor), default(string));

        public string Placeholder
        {
            get { return (string)GetValue(PlaceholderProperty); }
            set { SetValue(PlaceholderProperty, value); }
        }

        private void SetPlaceholder()
        {
            try
            {
                data.Placeholder = Placeholder;
            }
            catch { }
        }

        #endregion

        #region EntryWidth

        public static readonly BindableProperty EntryWidthProperty = BindableProperty.Create("EntryWidth", typeof(double), typeof(FormEditor), null);

        public double EntryWidth
        {
            get { return (double)GetValue(EntryWidthProperty); }
            set { SetValue(EntryWidthProperty, value); }
        }

        private void SetEntryWidth()
        {
            try
            {
                data.WidthRequest = EntryWidth;
            }
            catch { }
        }

        #endregion

        #region HasError

        public static readonly BindableProperty HasErrorProperty = BindableProperty.Create("HasError", typeof(bool), typeof(FormEditor), false, BindingMode.TwoWay);

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

                if (HasError)
                    color = (Color)Application.Current.Resources["Kucrimson"];
                else
                    color = (Color)Application.Current.Resources["PalmLeaf"];

                container.BackgroundColor = color;
                caption.IsVisible = HasError;
            }
            catch { }
        }

        #endregion

        #region ErrorMessage

        public static readonly BindableProperty ErrorMessageProperty = BindableProperty.Create("ErrorMessage", typeof(string), typeof(FormEditor), default(string), BindingMode.TwoWay);

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
                //caption.IsVisible = !string.IsNullOrEmpty(ErrorMessage);
            }
            catch { }
        }

        #endregion

 

        #region ShouldFormat

        public static readonly BindableProperty ShouldFormatProperty = BindableProperty.Create("ShouldFormat", typeof(bool), typeof(FormEditor), false);

        public bool ShouldFormat
        {
            get { return (bool)GetValue(ShouldFormatProperty); }
            set { SetValue(ShouldFormatProperty, value); }
        }

        private void SetShouldFormat()
        {
            try
            {

            }
            catch { }
        }

        #endregion

        #region Keyboard

        public static readonly BindableProperty KeyboardProperty = BindableProperty.Create("Keyboard", typeof(Keyboard), typeof(FormEditor), default(Keyboard));

        public Keyboard Keyboard
        {
            get { return (Keyboard)GetValue(KeyboardProperty); }
            set { SetValue(KeyboardProperty, value); }
        }

        private void SetKeyboard()
        {
            try
            {
                data.Keyboard = Keyboard;
            }
            catch { }
        }

        #endregion

        #region EntryColor

        public static readonly BindableProperty EntryColorProperty = BindableProperty.Create("EntryColor", typeof(Color), typeof(FormEditor), default(Color));

        public Color EntryColor
        {
            get { return (Color)GetValue(EntryColorProperty); }
            set { SetValue(EntryColorProperty, value); }
        }

        private void SetEntryColor()
        {
            try
            {
                innerContainer.BackgroundColor = EntryColor;
            }
            catch { }
        }

        #endregion

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            
            if (propertyName == TextProperty.PropertyName)
                SetText();

            if (propertyName == LabelProperty.PropertyName)
                SetLabel();

            if (propertyName == KeyboardProperty.PropertyName)
                SetKeyboard();

            if (propertyName == HasErrorProperty.PropertyName)
                SetHasError();

            if (propertyName == ErrorMessageProperty.PropertyName)
                SetErrorMessage();

            if (propertyName == PlaceholderProperty.PropertyName)
                SetPlaceholder();

            if (propertyName == EntryWidthProperty.PropertyName)
                SetEntryWidth();

            if (propertyName == NameProperty.PropertyName)
                SetName();

            if (propertyName == EntryColorProperty.PropertyName)
                SetEntryColor();

            if (propertyName == ShouldFormatProperty.PropertyName)
                SetShouldFormat();
        }

        void Handle_TextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            try
            {
                this.Text = e.NewTextValue;

                var nav = new ValidationCommandNav { Name = Name };
                if (this.BindingContext is ExistingUserSignupViewModel existingUserSignupViewModel)
                    existingUserSignupViewModel.Validate.Execute(nav);

                if (this.BindingContext is NewUserSignupViewModel newUserSignupViewModel)
                    newUserSignupViewModel.Validate.Execute(nav);

                if (this.BindingContext is LoginViewModel loginViewModel)
                    loginViewModel.Validate.Execute(nav);

                if (this.BindingContext is RedemptionRequestViewModel redemptionRequestViewModel)
                    redemptionRequestViewModel.Validate.Execute(nav);

                if (this.BindingContext is RolloverRequestViewModel rolloverRequestViewModel)
                    rolloverRequestViewModel.Validate.Execute(nav);

                if (this.BindingContext is ManualDepositViewModel manualDepositViewModel)
                    manualDepositViewModel.Validate.Execute(nav);

                if (this.BindingContext is DepositPaymentViewModel depositPaymentViewModel)
                    depositPaymentViewModel.Validate.Execute(nav);

                if (this.BindingContext is CardPaymentDetailsViewModel cardPaymentDetailsViewModel)
                    cardPaymentDetailsViewModel.Validate.Execute(nav);

                if (this.BindingContext is DepositInvestmentDetailsViewModel depositInvestmentDetailsViewModel)
                    depositInvestmentDetailsViewModel.Validate.Execute(nav);

                if (this.BindingContext is UpdateKYCAccountViewModel updateProfileViewModel)
                    updateProfileViewModel.Validate.Execute(nav);

                if (this.BindingContext is ResetPinViewModel resetPinViewModel)
                    resetPinViewModel.Validate.Execute(nav);

                if (this.BindingContext is UpdateContactInfoViewModel updateContactInfoViewModel)
                    updateContactInfoViewModel.Validate.Execute(nav);

                if (ShouldFormat)
                {
                    if (sender is Entry entry)
                    {
                        var text = NumberFormatter.ExtractNumber(entry.Text);
                        double.TryParse(text, out double amount);

                        this.Amount = amount;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception: {ex.Message}");
            }
        }

        public void SetEntryFocus()
        {
            try
            {
                if (!data.IsFocused)
                    data.Focus();
            }
            catch { }
        }

        void Handle_Focused(object sender, Xamarin.Forms.FocusEventArgs e)
        {
            if (ShouldFormat)
            {
                if (sender is Editor entry)
                {
                    entry.Text = NumberFormatter.ExtractNumber(entry.Text);
                }
            }
        }

        void Handle_Unfocused(object sender, Xamarin.Forms.FocusEventArgs e)
        {
            if (ShouldFormat)
            {
                if (sender is Editor entry)
                {
                    var amount = !string.IsNullOrEmpty(entry.Text) ? entry.Text : "0";
                    var retVal = NumberFormatter.FormatAmount(amount);
                    entry.Text = retVal;
                }
            }
        }
    }
}
