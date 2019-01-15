using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;

namespace Zenith
{
    public partial class BeneficiaryPopupDialog : PopupPage
    {
        TaskCompletionSource<Response> _taskCompletionSource;

        public BeneficiaryPopupDialog()
        {
            InitializeComponent();
        }

        public BeneficiaryPopupDialog(List<Abstractions.Beneficiary> beneficiary)
        {
            InitializeComponent();

            listView.ItemsSource = beneficiary;
        }

        public static async Task<Response> ShowDialog(INavigation navigation, List<Abstractions.Beneficiary> beneficiary)
        {
            var page = new BeneficiaryPopupDialog(beneficiary);

            await NavigationExtension.PushPopupAsync(navigation, page);

            var response = await page.GetResponse();

            await NavigationExtension.RemovePopupPageAsync(navigation, page);

            return response;
        }

        void Handle_Tapped(object sender, System.EventArgs e)
        {
            try
            {
                var list = sender as ListView;

                var item = list.SelectedItem;

                SetResponse(true, item);
            }
            catch (Exception ex) { }
        }

        private Task<Response> GetResponse()
        {
            _taskCompletionSource = new TaskCompletionSource<Response>();
            return _taskCompletionSource.Task;
        }
        void SetResponse(bool okSelected, object item)
        {
            try
            {
                if (_taskCompletionSource != null)
                {
                    var response = new Response
                    {
                        Ok = okSelected,
                        Item = item
                    };
                    _taskCompletionSource.SetResult(response);
                    _taskCompletionSource = null;
                }
            }
            catch (Exception ex) { }
        }

        public class Response
        {
            public bool Ok { get; set; }

            public object Item { get; set; }
        }
    
    }
}
