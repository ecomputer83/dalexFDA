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

            if (propertyName == PlaceholderProperty.PropertyName)
            {
                SetPlaceholder();
            }

            if (propertyName == HorizontalTextAlignmentProperty.PropertyName)
            {
                SetHorizontalTextAlignment();
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
