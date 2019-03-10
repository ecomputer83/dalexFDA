using Acr.UserDialogs;
using dalexFDA.Abstractions;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace dalexFDA.Core
{
    public class UpdateKYCAccountViewModel : BaseViewModel
    {
        readonly IErrorManager ErrorManager;
        readonly IUserDialogs Dialog;
        readonly IMedia Media;
        readonly IAccountService AccountService;
        readonly IAppService AppService;
        readonly ISession SessionService;

        public bool IsPassportComplete { get { return !string.IsNullOrEmpty(PassportPhotographID); } }
        public string PassportPhotographButtonText { get { return IsPassportComplete ? complete_passport_photograph_text : default_passport_photograph_text; } }
        public string PassportPhotographID { get; set; }

        public DateTime DOB { get; set; } 
        public bool DOBHasError { get; set; }
        public string DOBErrorMessage { get; set; }

        public string POB { get; set; }
        public bool POBHasError { get; set; }
        public string POBErrorMessage { get; set; }

        public bool IsEvidenceOfAddressComplete { get { return !string.IsNullOrEmpty(EvidenceOfAddressID); } }
        public string EvidenceOfAddressButtonText { get { return IsEvidenceOfAddressComplete ? complete_evidence_of_address_text : default_evidence_of_address_text; } }
        public string EvidenceOfAddressID { get; set; }

        public string Address { get; set; }
        public bool AddressHasError { get; set; }
        public string AddressErrorMessage { get; set; }

        public string HomeTown { get; set; }
        public bool HomeTownHasError { get; set; }
        public string HomeTownErrorMessage { get; set; }

        public string PostalAddress { get; set; }
        public bool PostalAddressHasError { get; set; }
        public string PostalAddressErrorMessage { get; set; }

        public string Nationality { get; set; }
        public bool NationalityHasError { get; set; }
        public string NationalityErrorMessage { get; set; }

        public bool IsValidIDCardComplete { get { return !string.IsNullOrEmpty(ValidIDCardID); } }
        public string ValidIdCardButtonText { get { return IsValidIDCardComplete ? complete_valid_id_card_text : default_valid_id_card_text; } }
        public string ValidIDCardID { get; set; }

        public DateTime ExpiryDate { get; set; }
        public bool ExpiryDateHasError { get; set; }
        public string ExpiryDateErrorMessage { get; set; }

        public Style NextButtonStyle
        {
            get
            {
                return IsPassportComplete && IsEvidenceOfAddressComplete && IsValidIDCardComplete ? (Style)Application.Current.Resources["PrimaryButton"]
                                       : (Style)Application.Current.Resources["DisabledButton"];
            }
        }

        public Command AddPhoto { get; set; }
        public Command AddAddressEvidence { get; set; }
        public Command AddValidID { get; set; }
        public Command Next { get; set; }
        public Command Validate { get; private set; }

        private const string default_passport_photograph_text = "ADD PASSPORT PHOTOGRAPH";
        private const string default_evidence_of_address_text = "ADD EVIDENCE OF ADDRESS";
        private const string default_valid_id_card_text = "ADD VALID ID CARD";
        private const string complete_passport_photograph_text = "PASSPORT PHOTOGRAPH ADDED";
        private const string complete_evidence_of_address_text = "EVIDENCE OF ADDRESS ADDED";
        private const string complete_valid_id_card_text = "VALID ID CARD ADDED";
        private const string dob_error_message = "Please select your date of birth";
        private const string pob_error_message = "Please enter your place of birth";
        private const string address_error_message = "Please enter your home address";
        private const string home_town_error_message = "Please enter your home town";
        private const string postal_address_error_message = "Please enter your postal address";
        private const string nationality_error_message = "Please enter your nationality";
        private const string expiry_date_error_message = "Please select an expiry date";

        public UpdateKYCAccountViewModel(IErrorManager ErrorManager, IUserDialogs Dialog, IMedia Media, IAccountService AccountService,
                                        IAppService AppService, ISession SessionService)
        {
            this.ErrorManager = ErrorManager;
            this.Dialog = Dialog;
            this.Media = Media;
            this.AccountService = AccountService;
            this.AppService = AppService;
            this.SessionService = SessionService;

            AddPhoto = new Command(async () => await ExecuteAddPhoto());
            AddAddressEvidence = new Command(async () => await ExecuteAddAddressEvidence());
            AddValidID = new Command(async () => await ExecuteAddValidID());
            Next = new Command(async () => await ExecuteNext());
            Validate = new Command<ValidationCommandNav>(async (obj) => await ExecuteValidate(obj));
        }

        public async override void Init(object initData)
        {
            base.Init(initData);

            try
            {
                SessionService.CurrentUser = await AccountService.GetUser();

                var kycresponse = await AccountService.GetApplication();

                if(kycresponse?.Application_Status.ToLower() != "completed")
                {
                    await CoreMethods.DisplayAlert("KYC Update", "KYC Application is in progress, We will get back to you shortly.", "Ok");
                    AppService.StartAccountStatements();
                }

                PassportPhotographID = SessionService.CurrentUser?.PhotoUrl;
                EvidenceOfAddressID = SessionService.CurrentUser?.ProofOfResUtilityBill;
                ValidIDCardID = SessionService.CurrentUser?.CopyOfValidId;
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }

        private async Task ExecuteAddPhoto()
        {
            try
            {
                if (IsPassportComplete) return;

                using (UserDialogs.Instance.Loading("Please wait...", null, null, true, MaskType.Gradient))
                {
                    PassportPhotographID = await CapturePhoto(DocumentType.PassportPhotograph);
                }
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
                if (IsEvidenceOfAddressComplete) return;

                using (UserDialogs.Instance.Loading("Please wait...", null, null, true, MaskType.Black))
                {
                    EvidenceOfAddressID = await CapturePhoto(DocumentType.EvidenceOfAddress);
                }
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
                if (IsValidIDCardComplete) return;

                using (UserDialogs.Instance.Loading("Please wait...", null, null, true, MaskType.Clear))
                {
                    ValidIDCardID = await CapturePhoto(DocumentType.ValidIDCard);
                }
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }

        private async Task ExecuteNext()
        {
            try
            {
                if (!IsPassportComplete || !IsEvidenceOfAddressComplete || !IsValidIDCardComplete) return;
                if (PerformValidation()) return;

                using (Dialog.Loading())
                {
                    var updateRequest = new KYCProfileRequest
                    {
                        BirthDate = DOB,
                        PlaceOfBirth = POB,
                        ProofOfResUtilityBill = EvidenceOfAddressID,
                        Address = Address,
                        HomeTown = HomeTown,
                        PostalAddress = PostalAddress,
                        Nationality = Nationality,
                        CopyOfValidId = ValidIDCardID,
                        ExpiryDateOfId = ExpiryDate
                    };
                    var response = await AccountService.UpdateKYCAccount(updateRequest);

                    if (response)
                    {
                        await CoreMethods.DisplayAlert("KYC Update", "Your request submitted successfully. We will get back to you shortly.", "Ok");
                        AppService.StartAccountStatements();
                    }     
                    else
                    {
                        await CoreMethods.DisplayAlert("KYC Update", "Unable to update your account. Please try again.", "Ok");
                    }
                }
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }

        private async Task ExecuteValidate(ValidationCommandNav obj)
        {
            try
            {
                ValidateControls(obj?.Name);
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }

        private async Task<string> CapturePhoto(DocumentType fileType)
        {
            try
            {
                string retVal = "";
                string message = "Upload Image";
                MediaFile file = null;
                await Media.Initialize();

                var result = await Dialog.ActionSheetAsync(message, "Cancel", null, default(System.Threading.CancellationToken), "Camera", "Gallery");

                if (!string.IsNullOrEmpty(result) && result.Contains("Camera"))
                {
                    var options = new StoreCameraMediaOptions();
                    options.Directory = "temp";
                    options.Name = Guid.NewGuid().ToString() + ".jpg";
                    options.PhotoSize = PhotoSize.Custom;
                    options.CustomPhotoSize = 90; //Resize to 90% of original

                    if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                    {
                        await ErrorManager.DisplayErrorMessageAsync(new Exception("No Camera Available"));
                    }
                    else
                    {
                        var cameraStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);
                        if (cameraStatus != PermissionStatus.Granted)
                        {
                            await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Camera);

                            var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Permission.Camera, Permission.Storage });
                            cameraStatus = results[Permission.Camera];
                        }

                        if (cameraStatus == PermissionStatus.Denied)
                        {
                            await Dialog.AlertAsync("Kindly grant camera access from Privacy Settings", "Permission Denied");
                            //PhoneSettings.OpenSettings();
                            return "";
                        }

                        if (cameraStatus == PermissionStatus.Granted)
                            file = await Media.TakePhotoAsync(options);                        
                    }
                }
                else if (!string.IsNullOrEmpty(result) && result.Contains("Gallery"))
                {
                    if (!CrossMedia.Current.IsPickPhotoSupported)
                    {
                        await ErrorManager.DisplayErrorMessageAsync(new Exception(), "Gallery not supported on your phone");
                    }
                    else
                    {
                        var PhotoStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Photos);
                        if (PhotoStatus == PermissionStatus.Denied)
                        {
                            await Dialog.AlertAsync("Kindly grant gallery access from Privacy Settings", "Permission Denied");
                            //PhoneSettings.OpenSettings();
                            return "";
                        }
                        if (PhotoStatus != PermissionStatus.Granted)
                        {
                            var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Permission.Photos });
                            PhotoStatus = results[Permission.Photos];
                        }
                        if (PhotoStatus == PermissionStatus.Granted)
                            file = await Media.PickPhotoAsync(new PickMediaOptions());                        
                    }
                }

                if (file != null)
                {
                    byte[] data = StreamToBytes(file.GetStream());
                    file.Dispose();
                    var request = new DocumentRequest { FileType = (int)fileType, File = data };
                    retVal = await AccountService.AddDocument(request);
                }
                return await Task.FromResult(retVal);
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
                return "";
            }
        }

        byte[] StreamToBytes(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        private void ValidateControls(string name)
        {
            switch (name)
            {
                case "DOB":
                    DOBHasError = false;
                    break;
                case "POB":
                    POBHasError = string.IsNullOrEmpty(POB);
                    POBErrorMessage = address_error_message;
                    break;
                case "Address":
                    AddressHasError = string.IsNullOrEmpty(Address);
                    AddressErrorMessage = address_error_message;
                    break;
                case "HomeTown":
                    HomeTownHasError = string.IsNullOrEmpty(HomeTown);
                    HomeTownErrorMessage = home_town_error_message;
                    break;
                case "PostalAddress":
                    PostalAddressHasError = string.IsNullOrEmpty(PostalAddress);
                    PostalAddressErrorMessage = postal_address_error_message;
                    break;
                case "Nationality":
                    NationalityHasError = string.IsNullOrEmpty(Nationality);
                    NationalityErrorMessage = nationality_error_message;
                    break;
                case "ExpiryDate":
                    ExpiryDateHasError = false;
                    break;
            }
        }

        private bool PerformValidation()
        {
            DOBHasError = DOB == DateTime.MinValue;
            DOBErrorMessage = dob_error_message;

            POBHasError = string.IsNullOrEmpty(POB);
            POBErrorMessage = address_error_message;

            AddressHasError = string.IsNullOrEmpty(Address);
            AddressErrorMessage = address_error_message;

            HomeTownHasError = string.IsNullOrEmpty(HomeTown);
            HomeTownErrorMessage = home_town_error_message;

            PostalAddressHasError = string.IsNullOrEmpty(PostalAddress);
            PostalAddressErrorMessage = postal_address_error_message;

            NationalityHasError = string.IsNullOrEmpty(Nationality);
            NationalityErrorMessage = nationality_error_message;

            ExpiryDateHasError = ExpiryDate == DateTime.MinValue;
            ExpiryDateErrorMessage = expiry_date_error_message;

            return DOBHasError || POBHasError || AddressHasError || HomeTownHasError || PostalAddressHasError || NationalityHasError || ExpiryDateHasError;
        }
    }
}
