using dalexFDA.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace dalexFDA
{
    public class UpdateProfileViewModel : BaseViewModel
    {
        readonly IErrorManager ErrorManager;

        public Command AddPhoto { get; set; }
        public Command AddAddressEvidence { get; set; }
        public Command AddValidID { get; set; }

        public UpdateProfileViewModel(IErrorManager ErrorManager)
        {
            this.ErrorManager = ErrorManager;

            AddPhoto = new Command(async () => await ExecuteAddPhoto());
            AddAddressEvidence = new Command(async () => await ExecuteAddAddressEvidence());
            AddValidID = new Command(async () => await ExecuteAddValidID());
        }

        private async Task ExecuteAddPhoto()
        {
            try
            {

            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }

        private async Task ExecuteAddAddressEvidence()
        {
            try
            {

            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }

        private async Task ExecuteAddValidID()
        {
            try
            {

            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }
    }
}
