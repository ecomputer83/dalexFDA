using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace dalexFDA
{
	public partial class RolloverHistoryItemView : ContentView
	{
		public RolloverHistoryItemView ()
		{
			InitializeComponent ();
		}

        #region Id

        public static readonly BindableProperty IdProperty = BindableProperty.Create("Id", typeof(string), typeof(RolloverHistoryItemView), default(string));

        public string Id
        {
            get { return (string)GetValue(IdProperty); }
            set { SetValue(IdProperty, value); }
        }

        private void SetId()
        {
            try
            {
                number.Text = Id;
                number.TextDecorations = TextDecorations.Underline;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"=======IdProperty=====\n{ex.Message}");
            }
        }

        #endregion

        #region Type

        public static readonly BindableProperty TypeProperty = BindableProperty.Create("Type", typeof(string), typeof(RolloverHistoryItemView), default(string));

        public string Type
        {
            get { return (string)GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }

        private void SetType()
        {
            try
            {
                type.Text = Type?.ToUpper();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"=======Type=====\n{ex.Message}");
            }
        }

        #endregion

        #region Status

        public static readonly BindableProperty StatusProperty = BindableProperty.Create("Status", typeof(string), typeof(RolloverHistoryItemView), default(string));

        public string Status
        {
            get { return (string)GetValue(StatusProperty); }
            set { SetValue(StatusProperty, value); }
        }

        private void SetStatus()
        {
            try
            {
                status.Text = $"Type: {Status}"; ;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"=======Status=====\n{ex.Message}");
            }
        }

        #endregion

        #region Amount

        public static readonly BindableProperty AmountProperty = BindableProperty.Create("Amount", typeof(double), typeof(RolloverHistoryItemView), default(double));

        public double Amount
        {
            get { return (double)GetValue(AmountProperty); }
            set { SetValue(AmountProperty, value); }
        }

        private void SetAmount()
        {
            try
            {
                amount.Text = $"Amount: GHS {NumberFormatter.FormatAmount(Amount.ToString())}";
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"=======Amount=====\n{ex.Message}");
            }
        }

        #endregion

        #region Duration

        public static readonly BindableProperty DurationProperty = BindableProperty.Create("Duration", typeof(int), typeof(RolloverHistoryItemView), 0);

        public int Duration
        {
            get { return (int)GetValue(DurationProperty); }
            set { SetValue(DurationProperty, value); }
        }

        private void SetDuration()
        {
            try
            {
                duration.Text = $"{Duration} days";
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"=======Amount=====\n{ex.Message}");
            }
        }

        #endregion

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == IdProperty.PropertyName)
                SetId();

            if (propertyName == TypeProperty.PropertyName)
                SetType();

            if (propertyName == StatusProperty.PropertyName)
                SetStatus();

            if (propertyName == AmountProperty.PropertyName)
                SetAmount();

            if (propertyName == DurationProperty.PropertyName)
                SetDuration();
        }
    }
}