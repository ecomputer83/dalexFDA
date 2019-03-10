using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace dalexFDA.Core
{
    public partial class MenuItem : ContentView
    {
        public MenuItem()
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
                if (this.Command != null)
                {
                    this.Command.Execute(null);
                }
            }
            catch { }
        }


        #region Text
        public static readonly BindableProperty TextProperty = BindableProperty.Create("Text", typeof(string), typeof(MenuItem), default(string));

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        private void SetText()
        {
            try
            {
                button.Text = Text;
            }
            catch { }
        }

        #endregion

        #region HasSubItem

        public static readonly BindableProperty HasSubItemProperty = BindableProperty.Create("HasSubItem", typeof(bool), typeof(MenuItem), false);

        public bool HasSubItem
        {
            get { return (bool)GetValue(HasSubItemProperty); }
            set { SetValue(HasSubItemProperty, value); }
        }

        private void SetHasSubItem()
        {
            try
            {
                if (HasSubItem)
                {
                    caret.Svg = "res:images.icon-right-arrow";
                    caret.IsVisible = true;
                }
            }
            catch { }
        }

        #endregion

        #region IsOpen

        public static readonly BindableProperty IsOpenProperty = BindableProperty.Create("IsOpen", typeof(bool), typeof(MenuItem), false);

        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }

        private void SetIsOpen()
        {
            try
            {
                if (!IsOpen)
                {
                    caret.Svg = "res:images.icon-right-arrow";
                }
                else
                {
                    caret.Svg = "res:images.icon-down-arrow";
                }
            }
            catch { }
        }

        #endregion

        #region Command

        public static readonly BindableProperty CommandProperty = BindableProperty.Create("Command", typeof(Command), typeof(MenuItem), default(Command));

        public Command Command
        {
            get { return (Command)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        private void SetCommand()
        {
            try
            {
                icon.Command = Command;
            }
            catch { }
        }

        #endregion

        #region Icon

        public static readonly BindableProperty IconProperty = BindableProperty.Create("Icon", typeof(string), typeof(MenuItem), default(string));

        public string Icon
        {
            get { return (string)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        private void SetIcon()
        {
            try
            {
                icon.Svg = Icon;
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

            if (propertyName == IconProperty.PropertyName)
            {
                SetIcon();
            }

            if (propertyName == HasSubItemProperty.PropertyName)
            {
                SetHasSubItem();
            }

            if (propertyName == IsOpenProperty.PropertyName)
            {
                SetIsOpen();
            }

            if (propertyName == CommandProperty.PropertyName)
            {
                SetCommand();
            }
        }
    }
}
