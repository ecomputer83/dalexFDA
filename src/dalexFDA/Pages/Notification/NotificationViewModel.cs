using Acr.UserDialogs;
using dalexFDA.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace dalexFDA
{

    public class NotificationViewModel : BaseViewModel
    {
        internal readonly IErrorManager ErrorManager;
        readonly IAccountService AccountService;
        readonly IUserDialogs Dialog;

        public TransactionHistory TransactionHistory { get; set; }

        public ObservableCollection<NotificationItemViewModel> NotificationItems { get; set; }
        public List<Notification> Notifications { get; set; }

        public NotificationViewModel(IErrorManager ErrorManager, IAccountService AccountService, IUserDialogs Dialog)
        {
            this.ErrorManager = ErrorManager;
            this.AccountService = AccountService;
            this.Dialog = Dialog;
        }

        public async override void Init(object initData)
        {
            base.Init(initData);

            try
            {
                var list = new List<NotificationItemViewModel>();
                using (Dialog.Loading("Loading..."))
                {
                    Notifications = await AccountService.GetNotification();

                    if (Notifications != null)
                        foreach (var item in Notifications)
                            list.Add(new NotificationItemViewModel(this, item));

                    NotificationItems = new ObservableCollection<NotificationItemViewModel>(list);
                }
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }
    }
}