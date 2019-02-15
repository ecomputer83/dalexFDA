using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;

namespace dalexFDA
{
    public partial class DepositHistoryItemView : ContentView
    {
        public DepositHistoryItemView()
        {
            InitializeComponent();
        }

        #region Id

        public static readonly BindableProperty IdProperty = BindableProperty.Create("Id", typeof(string), typeof(DepositHistoryItemView), default(string));

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

        #region PaymentType

        public static readonly BindableProperty PaymentTypeProperty = BindableProperty.Create("PaymentType", typeof(string), typeof(DepositHistoryItemView), default(string));

        public string PaymentType
        {
            get { return (string)GetValue(PaymentTypeProperty); }
            set { SetValue(PaymentTypeProperty, value); }
        }

        private void SetPaymentType()
        {
            try
            {
                paymentType.Text = PaymentType?.ToUpper();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"=======PaymentType=====\n{ex.Message}");
            }
        }

        #endregion

        #region DocumentType

        public static readonly BindableProperty DocumentTypeProperty = BindableProperty.Create("DocumentType", typeof(string), typeof(DepositHistoryItemView), default(string));

        public string DocumentType
        {
            get { return (string)GetValue(DocumentTypeProperty); }
            set { SetValue(DocumentTypeProperty, value); }
        }

        private void SetDocumentType()
        {
            try
            {
                documentType.Text = $"Type: {DocumentType}"; ;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"=======DocumentType=====\n{ex.Message}");
            }
        }

        #endregion

        #region Amount

        public static readonly BindableProperty AmountProperty = BindableProperty.Create("Amount", typeof(double), typeof(DepositHistoryItemView), default(double));

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

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == IdProperty.PropertyName)
                SetId();

            if (propertyName == PaymentTypeProperty.PropertyName)
                SetPaymentType();

            if (propertyName == DocumentTypeProperty.PropertyName)
                SetDocumentType();

            if (propertyName == AmountProperty.PropertyName)
                SetAmount();
        }
    }
}
