using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;

namespace Zenith
{
    public partial class InvestmentView : ContentView
    {
        public InvestmentView()
        {
            InitializeComponent();
        }

        #region Id

        public static readonly BindableProperty IdProperty = BindableProperty.Create("Id", typeof(string), typeof(InvestmentView), default(string));

        public string Id
        {
            get { return (string)GetValue(IdProperty); }
            set { SetValue(IdProperty, value); }
        }

        private void SetId()
        {
            try
            {
                id.Text = Id;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"=======ProductProperty=====\n{ex.Message}");
            }
        }

        #endregion


        #region Principal

        public static readonly BindableProperty PrincipalProperty = BindableProperty.Create("Principal", typeof(string), typeof(InvestmentView), default(string));

        public string Principal
        {
            get { return (string)GetValue(PrincipalProperty); }
            set { SetValue(PrincipalProperty, value); }
        }

        private void SetPrincipal()
        {
            try
            {
                principal.Text = Principal;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"=======ProductProperty=====\n{ex.Message}");
            }
        }

        #endregion

        #region Days

        public static readonly BindableProperty DaysProperty = BindableProperty.Create("Days", typeof(string), typeof(InvestmentView), default(string));

        public string Days
        {
            get { return (string)GetValue(DaysProperty); }
            set { SetValue(DaysProperty, value); }
        }

        private void SetDays()
        {
            try
            {
                //days.Text = Days;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"=======OrderNumberProperty=====\n{ex.Message}");
            }
        }

        #endregion

        #region Rate

        public static readonly BindableProperty RateProperty = BindableProperty.Create("Rate", typeof(string), typeof(InvestmentView), default(string));

        public string Rate
        {
            get { return (string)GetValue(RateProperty); }
            set { SetValue(RateProperty, value); }
        }

        private void SetRate()
        {
            try
            {
                rate.Text = Rate;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"=======TerminalProperty=====\n{ex.Message}");
            }
        }

        #endregion

        #region StartDate

        public static readonly BindableProperty StartDateProperty = BindableProperty.Create("StartDate", typeof(string), typeof(InvestmentView), default(string));

        public string StartDate
        {
            get { return (string)GetValue(StartDateProperty); }
            set { SetValue(StartDateProperty, value); }
        }

        private void SetStartDate()
        {
            try
            {
                //sdate.Text = StartDate;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"=======TerminalDetailProperty=====\n{ex.Message}");
            }
        }

        #endregion

        #region Maturity

        public static readonly BindableProperty MaturityProperty = BindableProperty.Create("Maturity", typeof(string), typeof(InvestmentView), default(string));

        public string Maturity
        {
            get { return (string)GetValue(MaturityProperty); }
            set { SetValue(MaturityProperty, value); }
        }

        private void SetMaturity()
        {
            try
            {
                maturity.Text = Maturity;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"=======DestinationProperty=====\n{ex.Message}");
            }
        }
        #endregion

        #region Command

        public Command Command
        {
            get { return (Command)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static readonly BindableProperty CommandProperty = BindableProperty.Create(
            nameof(Command),
            typeof(Command),
            typeof(InvestmentView),
            default(Command),
            BindingMode.TwoWay
        );

        #endregion

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == IdProperty.PropertyName)
                SetId();

            if (propertyName == PrincipalProperty.PropertyName)
                SetPrincipal();

            if (propertyName == DaysProperty.PropertyName)
                SetDays();

            if (propertyName == RateProperty.PropertyName)
                SetRate();

            if (propertyName == MaturityProperty.PropertyName)
                SetMaturity();

            if (propertyName == StartDateProperty.PropertyName)
                SetStartDate();

        }

        void Handle_Tapped(object sender, System.EventArgs e)
        {
            try
            {
                if (Command != null)
                {

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"=======Handle_Tapped=====\n{ex.Message}");
            }
        }
    }
}
