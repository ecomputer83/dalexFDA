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
	public partial class RedemptionHistoryItemView : ContentView
	{
		public RedemptionHistoryItemView ()
		{
			InitializeComponent ();
		}

        #region Id

        public static readonly BindableProperty IdProperty = BindableProperty.Create("Id", typeof(string), typeof(RedemptionHistoryItemView), default(string));

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

        public static readonly BindableProperty TypeProperty = BindableProperty.Create("Type", typeof(string), typeof(RedemptionHistoryItemView), default(string));

        public string Type
        {
            get { return (string)GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }

        private void SetType()
        {
            try
            {
                type.Text = Type;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"=======Type=====\n{ex.Message}");
            }
        }

        #endregion

        #region RetireDate

        public static readonly BindableProperty RetireDateProperty = BindableProperty.Create("RetireDate", typeof(DateTimeOffset), typeof(RedemptionHistoryItemView), default(DateTimeOffset));

        public DateTimeOffset RetireDate
        {
            get { return (DateTimeOffset)GetValue(RetireDateProperty); }
            set { SetValue(RetireDateProperty, value); }
        }

        private void SetRetireDate()
        {
            try
            {
                retire.Text = $"{RetireDate.ToString("dd/MM/yyyy")}";
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"=======RetireDate=====\n{ex.Message}");
            }
        }

        #endregion

        #region Amount

        public static readonly BindableProperty AmountProperty = BindableProperty.Create("Amount", typeof(double), typeof(RedemptionHistoryItemView), default(double));

        public double Amount
        {
            get { return (double)GetValue(AmountProperty); }
            set { SetValue(AmountProperty, value); }
        }

        private void SetAmount()
        {
            try
            {
                amount.Text = $"GHS {NumberFormatter.FormatAmount(Amount.ToString())}";
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"=======Amount=====\n{ex.Message}");
            }
        }

        #endregion

        #region Duration

        public static readonly BindableProperty DurationProperty = BindableProperty.Create("Duration", typeof(int), typeof(RedemptionHistoryItemView), 0);

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

            if (propertyName == RetireDateProperty.PropertyName)
                SetRetireDate();

            if (propertyName == AmountProperty.PropertyName)
                SetAmount();

            if (propertyName == DurationProperty.PropertyName)
                SetDuration();
        }
    }
}