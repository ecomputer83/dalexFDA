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
	public partial class ConsolidationHistoryItemView : ContentView
	{
		public ConsolidationHistoryItemView ()
		{
			InitializeComponent ();
		}

        #region Id

        public static readonly BindableProperty IdProperty = BindableProperty.Create("Id", typeof(string), typeof(ConsolidationHistoryItemView), default(string));

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

        #region Principal

        public static readonly BindableProperty PrincipalProperty = BindableProperty.Create("Principal", typeof(string), typeof(ConsolidationHistoryItemView), default(string));

        public string Principal
        {
            get { return (string)GetValue(PrincipalProperty); }
            set { SetValue(PrincipalProperty, value); }
        }

        private void SetPrincipal()
        {
            try
            {
                principal.Text = $"Principal: GHS {NumberFormatter.FormatAmount(Principal)}";
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"=======Principal=====\n{ex.Message}");
            }
        }

        #endregion

        #region ConsolidatedAmount

        public static readonly BindableProperty ConsolidatedAmountProperty = BindableProperty.Create("ConsolidatedAmount", typeof(string), typeof(ConsolidationHistoryItemView), default(string));

        public string ConsolidatedAmount
        {
            get { return (string)GetValue(ConsolidatedAmountProperty); }
            set { SetValue(ConsolidatedAmountProperty, value); }
        }

        private void SetConsolidatedAmount()
        {
            try
            {
                consolidatedAmount.Text = $"Consolidated: GHS {NumberFormatter.FormatAmount(ConsolidatedAmount)}"; ;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"=======ConsolidatedAmount=====\n{ex.Message}");
            }
        }

        #endregion

        #region InterestEarned

        public static readonly BindableProperty InterestEarnedProperty = BindableProperty.Create("InterestEarned", typeof(double), typeof(ConsolidationHistoryItemView), default(double));

        public double InterestEarned
        {
            get { return (double)GetValue(InterestEarnedProperty); }
            set { SetValue(InterestEarnedProperty, value); }
        }

        private void SetInterestEarned()
        {
            try
            {
                interest.Text = $"Interest: GHS {NumberFormatter.FormatAmount(InterestEarned.ToString())}";
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"=======InterestEarned=====\n{ex.Message}");
            }
        }

        #endregion

        #region InterestRate

        public static readonly BindableProperty InterestRateProperty = BindableProperty.Create("InterestRate", typeof(int), typeof(ConsolidationHistoryItemView), 0);

        public int InterestRate
        {
            get { return (int)GetValue(InterestRateProperty); }
            set { SetValue(InterestRateProperty, value); }
        }

        private void SetInterestRate()
        {
            try
            {
                rate.Text = $"Rate: {InterestRate} %";
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

            if (propertyName == PrincipalProperty.PropertyName)
                SetPrincipal();

            if (propertyName == ConsolidatedAmountProperty.PropertyName)
                SetConsolidatedAmount();

            if (propertyName == InterestEarnedProperty.PropertyName)
                SetInterestEarned();

            if (propertyName == InterestRateProperty.PropertyName)
                SetInterestRate();
        }
    }
}