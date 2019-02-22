using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace dalexFDA
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


        }

    }
}