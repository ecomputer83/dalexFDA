using dalexFDA.Abstractions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;

namespace dalexFDA.Core
{
    public partial class FormPicker : ContentView
    {
        public FormPicker()
        {
            InitializeComponent();
        }

        #region Name

        public static readonly BindableProperty NameProperty = BindableProperty.Create("Name", typeof(string), typeof(FormPicker), default(string));

        public string Name
        {
            get { return (string)GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }

        private void SetName()
        {
            try
            {
            }
            catch { }
        }

        #endregion

        #region SelectedIndex

        public static readonly BindableProperty SelectedIndexProperty = BindableProperty.Create("SelectedIndex", typeof(int?), typeof(FormPicker), null, BindingMode.TwoWay);

        public int? SelectedIndex
        {
            get { return (int?)GetValue(SelectedIndexProperty); }
            set { SetValue(SelectedIndexProperty, value); }
        }

        private void SetSelectedIndex()
        {
            try
            {
                if (SelectedIndex != null)
                    picker.SelectedIndex = SelectedIndex.GetValueOrDefault();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Placeholder ===== {ex.Message}");
            }
        }

        #endregion

        #region Label

        public static readonly BindableProperty LabelProperty = BindableProperty.Create("Label", typeof(string), typeof(FormPicker), default(string));

        public string Label
        {
            get { return (string)GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }

        private void SetLabel()
        {
            try
            {
                label.Text = Label;
            }
            catch { }
        }

        #endregion

        #region ItemsSource

        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create("ItemsSource", typeof(IList), typeof(FormPicker), default(IList), BindingMode.TwoWay);

        public IList ItemsSource
        {
            get { return (IList)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        private void SetItemsSource()
        {
            try
            {
                picker.ItemsSource = ItemsSource;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"ItemsSource ===== {ex.Message}");
            }
        }

        #endregion

        #region Placeholder

        public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create("Placeholder", typeof(string), typeof(FormPicker));

        public string Placeholder
        {
            get { return (string)GetValue(PlaceholderProperty); }
            set { SetValue(PlaceholderProperty, value); }
        }

        private void SetPlaceholder()
        {
            try
            {
                picker.Title = SelectedIndex != null ? Placeholder : "";
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Placeholder ===== {ex.Message}");
            }
        }

        #endregion

        #region HasError

        public static readonly BindableProperty HasErrorProperty = BindableProperty.Create("HasError", typeof(bool), typeof(FormPicker), false, BindingMode.TwoWay);

        public bool HasError
        {
            get { return (bool)GetValue(HasErrorProperty); }
            set { SetValue(HasErrorProperty, value); }
        }

        private void SetHasError()
        {
            try
            {
                Color color;
                bool isVisible;

                if (HasError)
                {
                    color = (Color)Application.Current.Resources["Kucrimson"];
                    isVisible = true;
                }
                else
                {
                    color = (Color)Application.Current.Resources["PalmLeaf"];
                    isVisible = false;
                }

                container.BackgroundColor = color;
                caption.IsVisible = isVisible;
            }
            catch { }
        }

        #endregion

        #region ErrorMessage

        public static readonly BindableProperty ErrorMessageProperty = BindableProperty.Create("ErrorMessage", typeof(string), typeof(FormPicker), default(string), BindingMode.TwoWay);

        public string ErrorMessage
        {
            get { return (string)GetValue(ErrorMessageProperty); }
            set { SetValue(ErrorMessageProperty, value); }
        }

        private void SetErrorMessage()
        {
            try
            {
                caption.Text = ErrorMessage;
            }
            catch { }
        }

        #endregion

        #region PickerColor

        public static readonly BindableProperty PickerColorProperty = BindableProperty.Create("PickerColor", typeof(Color), typeof(FormPicker), default(Color));

        public Color PickerColor
        {
            get { return (Color)GetValue(PickerColorProperty); }
            set { SetValue(PickerColorProperty, value); }
        }

        private void SetPickerColor()
        {
            try
            {
                innerContainer.BackgroundColor = PickerColor;
            }
            catch { }
        }

        #endregion

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == NameProperty.PropertyName)
            {
                SetName();
            }

            if (propertyName == LabelProperty.PropertyName)
            {
                SetLabel();
            }

            if (propertyName == ItemsSourceProperty.PropertyName)
            {
                SetItemsSource();
            }

            if (propertyName == SelectedIndexProperty.PropertyName)
            {
                SetSelectedIndex();
            }


            if (propertyName == PlaceholderProperty.PropertyName)
            {
                SetPlaceholder();
            }

            if (propertyName == HasErrorProperty.PropertyName)
            {
                SetHasError();
            }

            if (propertyName == ErrorMessageProperty.PropertyName)
            {
                SetErrorMessage();
            }

            if (propertyName == PickerColorProperty.PropertyName)
            {
                SetPickerColor();
            }
        }

        void Handle_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            try
            {
                SelectedIndex = picker.SelectedIndex;

                if (this.BindingContext is ManualDepositViewModel manualDeposit)
                {
                    var nav = new ValidationCommandNav { Name = Name };
                    manualDeposit.Validate.Execute(nav);
                }

                if (this.BindingContext is UpdateContactInfoViewModel updateContactInfoViewModel)
                {
                    updateContactInfoViewModel.ContactTypeChanged.Execute(null);
                }

                if (this.BindingContext is AccountStatementsViewModel accountSummaryViewModel)
                {
                    accountSummaryViewModel.Validate.Execute(null);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{ex.Message}");
            }
        }

        void Handle_Tapped(object sender, System.EventArgs e)
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{ex.Message}");
            }
        }
    }
}
