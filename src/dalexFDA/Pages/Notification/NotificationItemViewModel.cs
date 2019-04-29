using dalexFDA.Abstractions;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace dalexFDA
{
    public class NotificationItemViewModel
    {
        NotificationViewModel Parent;
        public Notification Data { get; set; }

        public Command ViewDetail { get; private set; }

        public NotificationItemViewModel(NotificationViewModel Parent, object item)
        {
            this.Parent = Parent;
            Data = item as Notification;

        }

        private async Task ExecuteViewDetail()
        {
            try
            {

            }
            catch (Exception ex)
            {
                await Parent.ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }
    }
}
