using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;

namespace dalexFDA
{
    public partial class FormEntry : ContentView
    {
        public FormEntry()
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

        public static readonly BindableProperty NameProperty = BindableProperty.Create("Name", typeof(string), typeof(FormEntry), default(string));

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

        public static readonly BindableProperty LabelProperty = BindableProperty.Create("Label", typeof(string), typeof(FormEntry), default(string));

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

        #region Text

        public static readonly BindableProperty TextProperty = BindableProperty.Create("Text", typeof(string), typeof(FormEntry), default(string), BindingMode.TwoWay);

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

        #region Placeholder

        public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create("Placeholder", typeof(string), typeof(FormEntry), default(string));

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

        #region HorizontalTextAlignment

        public static readonly BindableProperty HorizontalTextAlignmentProperty = BindableProperty.Create("HorizontalTextAlignment", typeof(TextAlignment), typeof(FormEntry), TextAlignment.Start);

        public TextAlignment HorizontalTextAlignment
        {
            get { return (TextAlignment)GetValue(HorizontalTextAlignmentProperty); }
            set { SetValue(HorizontalTextAlignmentProperty, value); }
        }

        private void SetHorizontalTextAlignment()
        {
            try
            {
                data.HorizontalTextAlignment = HorizontalTextAlignment;
            }
            catch { }
        }

        #endregion

        #region EntryWidth

        public static readonly BindableProperty EntryWidthProperty = BindableProperty.Create("EntryWidth", typeof(double), typeof(FormEntry), null);

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

        public static readonly BindableProperty HasErrorProperty = BindableProperty.Create("HasError", typeof(bool), typeof(FormEntry), false, BindingMode.TwoWay);

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
                Color textColor;
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

        public static readonly BindableProperty ErrorMessageProperty = BindableProperty.Create("ErrorMessage", typeof(string), typeof(FormEntry), default(string), BindingMode.TwoWay);

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

        #region IsPassword

        public static readonly BindableProperty IsPasswordProperty = BindableProperty.Create("IsPassword", typeof(bool), typeof(FormEntry), false);

        public bool IsPassword
        {
            get { return (bool)GetValue(IsPasswordProperty); }
            set { SetValue(IsPasswordProperty, value); }
        }

        private void SetIsPassword()
        {
            try
            {
                data.IsPassword = IsPassword;
            }
            catch { }
        }

        #endregion

        #region IsNumeric

        public static readonly BindableProperty IsNumericProperty = BindableProperty.Create("IsNumeric", typeof(bool), typeof(FormEntry), false);

        public bool IsNumeric
        {
            get { return (bool)GetValue(IsNumericProperty); }
            set { SetValue(IsNumericProperty, value); }
        }

        private void SetIsNumeric()
        {
            try
            {
                numericBehavior.IsNumeric = IsNumeric;
            }
            catch { }
        }

        #endregion

        #region ShouldFormat

        public static readonly BindableProperty ShouldFormatProperty = BindableProperty.Create("ShouldFormat", typeof(bool), typeof(FormEntry), false);

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

        #region MaxLength

        public static readonly BindableProperty MaxLengthProperty = BindableProperty.Create("MaxLength", typeof(int), typeof(FormEntry), 0);

        public int MaxLength
        {
            get { return (int)GetValue(MaxLengthProperty); }
            set { SetValue(MaxLengthProperty, value); }
        }

        private void SetMaxLength()
        {
            try
            {
                maxLengthBehavior.MaxLength = MaxLength;
            }
            catch { }
        }

        #endregion

        #region Keyboard

        public static readonly BindableProperty KeyboardProperty = BindableProperty.Create("Keyboard", typeof(Keyboard), typeof(FormEntry), default(Keyboard));

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

        public static readonly BindableProperty EntryColorProperty = BindableProperty.Create("EntryColor", typeof(Color), typeof(FormEntry), default(Color));

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
            {
                SetText();
            }

            if (propertyName == LabelProperty.PropertyName)
            {
                SetLabel();
            }

            if (propertyName == IsPasswordProperty.PropertyName)
            {
                SetIsPassword();
            }

            if (propertyName == KeyboardProperty.PropertyName)
            {
                SetKeyboard();
            }

            if (propertyName == HasErrorProperty.PropertyName)
            {
                SetHasError();
            }

            if (propertyName == ErrorMessageProperty.PropertyName)
            {
                SetErrorMessage();
            }

            if (propertyName == PlaceholderProperty.PropertyName)
            {
                SetPlaceholder();
            }

            if (propertyName == HorizontalTextAlignmentProperty.PropertyName)
            {
                SetHorizontalTextAlignment();
            }

            if (propertyName == EntryWidthProperty.PropertyName)
            {
                SetEntryWidth();
            }

            if (propertyName == NameProperty.PropertyName)
            {
                SetName();
            }

            if (propertyName == EntryColorProperty.PropertyName)
            {
                SetEntryColor();
            }

            if (propertyName == IsNumericProperty.PropertyName)
            {
                SetIsNumeric();
            }

            if (propertyName == ShouldFormatProperty.PropertyName)
            {
                SetShouldFormat();
            }

            if (propertyName == MaxLengthProperty.PropertyName)
            {
                SetMaxLength();
            }
        }

        void Handle_TextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            try
            {
                this.Text = e.NewTextValue;

                if (this.BindingContext is ExistingUserSignupViewModel model)
                {
                    var nav = new ExistingUserSignupViewModel.CommandNav { Name = Name };
                    model.Validate.Execute(nav);
                }

                if (this.BindingContext is NewUserSignupViewModel newUserSignupViewModel)
                {
                    var nav = new NewUserSignupViewModel.CommandNav { Name = Name };
                    newUserSignupViewModel.Validate.Execute(nav);
                }

                if (this.BindingContext is LoginViewModel loginViewModel)
                {
                    var nav = new LoginViewModel.CommandNav { Name = Name };
                    loginViewModel.Validate.Execute(nav);
                }

                if (this.BindingContext is RedemptionRequestViewModel redemptionRequestViewModel)
                {
                    var nav = new RedemptionRequestViewModel.CommandNav { Name = Name };
                    redemptionRequestViewModel.Validate.Execute(nav);
                }

                if (this.BindingContext is RolloverRequestViewModel rolloverRequestViewModel)
                {
                    var nav = new RolloverRequestViewModel.CommandNav { Name = Name };
                    rolloverRequestViewModel.Validate.Execute(nav);
                }

                if (ShouldFormat)
                {
                    if (sender is Entry entry)
                    {
                        var text = NumberFormatter.ExtractNumber(entry.Text);
                        double.TryParse(text, out double amount);

                        this.Text = amount.ToString();
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
                if (sender is Entry entry)
                {
                    entry.Text = NumberFormatter.ExtractNumber(entry.Text);
                }
            }
        }

        void Handle_Unfocused(object sender, Xamarin.Forms.FocusEventArgs e)
        {
            if (Name == "PhoneExtension" || Name == "PhoneNumber")
            {

                if (this.BindingContext is ExistingUserSignupViewModel model)
                {
                    model.GetUserDetailsFromPhoneNumber.Execute(null);
                }
            }

            if (ShouldFormat)
            {
                if (sender is Entry entry)
                {
                    var retVal = NumberFormatter.FormatToCurrency(entry.Text);
                    data.Text = retVal;
                }
            }
        }
    }
}
