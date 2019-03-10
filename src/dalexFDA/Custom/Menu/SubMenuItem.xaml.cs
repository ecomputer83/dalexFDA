using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace dalexFDA.Core
{
    public partial class SubMenuItem : ContentView
    {
        public SubMenuItem()
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

        public static readonly BindableProperty TextProperty = BindableProperty.Create("Text", typeof(string), typeof(SubMenuItem), default(string));

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

        #region Command

        public static readonly BindableProperty CommandProperty = BindableProperty.Create("Command", typeof(Command), typeof(SubMenuItem), default(Command));

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

        public static readonly BindableProperty IconProperty = BindableProperty.Create("Icon", typeof(string), typeof(SubMenuItem), default(string));

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

            if (propertyName == CommandProperty.PropertyName)
            {
                SetCommand();
            }
        }
    }
}
