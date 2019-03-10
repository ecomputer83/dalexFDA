using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace dalexFDA.Core
{
    public partial class CustomButton : ContentView
    {
        public CustomButton()
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
                SelectCommand.Execute(null);
            }
            catch { }
        }

        #region Text

        public static readonly BindableProperty TextProperty = BindableProperty.Create("Text", typeof(string), typeof(CustomButton), default(string), BindingMode.TwoWay);

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

        #region Icon

        public static readonly BindableProperty IconProperty = BindableProperty.Create("Icon", typeof(string), typeof(CustomButton), default(string), BindingMode.TwoWay);

        public string Icon
        {
            get { return (string)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        private void SetIcon()
        {
            try
            {
                icon.Svg = $"res:images.{Icon}";
            }
            catch { }
        }

        #endregion

        #region IconColor

        public static readonly BindableProperty IconColorProperty = BindableProperty.Create("IconColor", typeof(string), typeof(CustomButton), default(string), BindingMode.TwoWay);

        public string IconColor
        {
            get { return (string)GetValue(IconColorProperty); }
            set { SetValue(IconColorProperty, value); }
        }

        private void SetIconColor()
        {
            try
            {
                icon.ColorMapping = $"000000={IconColor}";
            }
            catch { }
        }

        #endregion

        #region IsComplete

        public static readonly BindableProperty IsCompleteProperty = BindableProperty.Create("IsComplete", typeof(bool), typeof(FormDatePicker), false, BindingMode.TwoWay);

        public bool IsComplete
        {
            get { return (bool)GetValue(IsCompleteProperty); }
            set { SetValue(IsCompleteProperty, value); }
        }

        private void SetIsComplete()
        {
            try
            {
                container.BackgroundColor = IsComplete ? (Color)Application.Current.Resources["PalmLeaf"] 
                                                        : (Color)Application.Current.Resources["White"];
                icon.Svg = IsComplete ? "res:images.icon-success" : "res:images.icon-add";
                icon.ColorMapping = IsComplete ? "000000=FFFFFF" : "000000=6D9B36";
                data.TextColor = IsComplete ? (Color)Application.Current.Resources["White"]
                                                        : (Color)Application.Current.Resources["PalmLeaf"];
            }
            catch { }
        }

        #endregion

        #region SelectCommand

        public static readonly BindableProperty SelectCommandProperty = BindableProperty.Create("SelectCommand", typeof(Command), typeof(CustomButton), null);

        public Command SelectCommand
        {
            get { return (Command)GetValue(SelectCommandProperty); }
            set { SetValue(SelectCommandProperty, value); }
        }

        private void SetSelectCommand()
        {
            try
            {

            }
            catch { }
        }

        #endregion

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == TextProperty.PropertyName)
                SetText();

            if (propertyName == IsCompleteProperty.PropertyName)
                SetIsComplete();
        }
    }
}