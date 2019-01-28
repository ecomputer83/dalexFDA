using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Acr.UserDialogs;
using dalexFDA.Abstractions;

namespace dalexFDA
{
    public class ErrorManager : IErrorManager
    {
        readonly IUserDialogs Dialog;
        //readonly ILogger Logger;
        public ErrorManager(IUserDialogs dialog)//, ILogger logger)
        {
            Dialog = dialog;
            //Logger = logger;
        }

        public async Task DisplayErrorMessageAsync(Exception ex, string errorMessage = null)
        {
            //log exception
            LogException(ex);


            //handle general network errors
            if (ex.Message.ToLower().Contains("nsurl"))
            {
                await Dialog.AlertAsync("We are unable to connect at this time. Please check your network connection.", "Error");


            }
            else
            {
                if (String.IsNullOrEmpty(errorMessage))
                {
                    await Dialog.AlertAsync(ex.Message, "Error");

                }
                else
                {
                    await Dialog.AlertAsync(errorMessage, "Error");
                }

            }

        }

        public void LogException(Exception ex, bool rethrow = false, [CallerMemberName] string caller = "")
        {
            //log exception

            //Logger.Report(ex);

            if (rethrow)
            {
                throw ex;
            }
        }
    }
}
