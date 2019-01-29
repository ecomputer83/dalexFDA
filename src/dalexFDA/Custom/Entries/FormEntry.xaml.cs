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

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == TextProperty.PropertyName)
            {
                SetText();
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
        }

        void Handle_TextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            try
            {
                this.Text = e.NewTextValue;
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
    }
}
