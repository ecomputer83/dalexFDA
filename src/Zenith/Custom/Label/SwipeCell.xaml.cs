using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;
using Zenith.Abstractions;

namespace Zenith
{
    public partial class SwipeCell : ContentView
    {
        public SwipeCell()
        {
            InitializeComponent();

            TapGestureRecognizer swipe = new TapGestureRecognizer();
            swipe.Tapped += Handle_Swiped;
            this.GestureRecognizers.Add(swipe);
        }

		private void Handle_Swiped(object sender, EventArgs e)
        {
            try
            {
                SwipeDirection = SwipeDirection?.ToLower() == Direction.Up.ToLower() ? Direction.Down : Direction.Up;
                Command.Execute(null);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"====== Handle Swiped {ex.Message} ========");
            }
        }

        #region SwipeDirection

        public static readonly BindableProperty SwipeDirectionProperty = BindableProperty.Create("SwipeDirection", typeof(string), typeof(SwipeCell), "Up");

        public string SwipeDirection
        {
            get { return (string)GetValue(SwipeDirectionProperty); }
            set { SetValue(SwipeDirectionProperty, value); }
        }

        private void SetSwipeDirection()
        {
            try
            {
                if(SwipeDirection.ToLower() == Direction.Up.ToLower())
                {
                    icon.Svg = "res:images.icon-up-arrow";
                    caption.Text = "Swipe up for tips";
                }
                else
                {
                    icon.Svg = "res:images.icon-down-arrow"; 
                    caption.Text = "Swipe down to close tips";
                }
            }
            catch { }
        }

        #endregion

        #region Command

        public static readonly BindableProperty CommandProperty = BindableProperty.Create("Command", typeof(Command), typeof(SwipeCell), null);

        public Command Command
        {
            get { return (Command)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        private void SetCommand()
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

			if (propertyName == SwipeDirectionProperty.PropertyName)
            {
				SetSwipeDirection();
            }
        }
    }
}
