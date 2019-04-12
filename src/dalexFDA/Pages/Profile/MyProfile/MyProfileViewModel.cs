using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using dalexFDA.Abstractions;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using PropertyChanged;
using Xamarin.Forms;

namespace dalexFDA
{
    [AddINotifyPropertyChangedInterface]
    public class MyProfileViewModel : BaseViewModel
    {
        readonly IErrorManager ErrorManager;
        readonly IAccountService AccountService;
        readonly IUserDialogs Dialog;
        readonly IMedia Media;

        public string DateText { get; set; } = "Date of Birth";
        public string Job { get; set; } = "Occupation";
        public bool isIndividual { get; set; }
        public User Profile { get; set; }
        public string DisplayName { get; set; }

        public Command ChangePhoto { get; set; }
         
        public MyProfileViewModel(IErrorManager ErrorManager, IAccountService AccountService, IUserDialogs Dialog, IMedia Media)
        {
            this.ErrorManager = ErrorManager;
            this.AccountService = AccountService;
            this.Dialog = Dialog;
            this.Media = Media;

            ChangePhoto = new Command(async () => await ExecuteChangePhoto());
        }

        public async override void Init(object initData)
        {
            base.Init(initData);

            try
            {
                using (Dialog.Loading("Please wait..."))
                {
                    Profile = await AccountService.GetUser();
                    isIndividual = (Profile.Group == 1);
                    if(Profile.Group != 1)
                    {
                        DateText = "Date of incorporation";
                        Job = "Business Activities";
                    }
                    DisplayName = Profile != null ? Profile.Name : "";
                    if (Profile != null)
                    {
                        Profile.PhotoUrl = !string.IsNullOrEmpty(Profile.PhotoUrl) ? Profile.PhotoUrl : "DefaultPhoto.png";
                    }                    
                }
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }

        private async Task ExecuteChangePhoto()
        {
            try
            {
                using (UserDialogs.Instance.Loading("Please wait...", null, null, true, MaskType.Gradient))
                {
                    Profile.PhotoUrl = await CapturePhoto(DocumentType.PassportPhotograph);
                }
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
    }
}
