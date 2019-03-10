using System;
using System.Linq;
using FreshMvvm;
using PropertyChanged;

namespace dalexFDA.Core
{
    [AddINotifyPropertyChangedInterface]
    public class BaseViewModel : FreshBasePageModel
    {
        public string Title { get; set; }
        public bool IsBusy { get; set; }

        protected virtual void Setup()
        {

        }

        public bool ValidatePassword(string password)
        {
            if (string.IsNullOrEmpty(password))
                return false;

            var retVal = true;
            bool isLengthyPassword = password.Length >= 8;
            bool hasLetter = password.Any(x => char.IsLetter(x));
            bool hasDigit = password.Any(x => char.IsDigit(x));
            bool hasUnicodeCharacter = password.Any(x => !char.IsDigit(x) && !char.IsLetter(x));
            bool hasSlash = password.Contains("\\") || password.Contains("/");

            if (!isLengthyPassword || !hasLetter || !hasDigit || !hasUnicodeCharacter || hasSlash)
            {
                retVal = false;
            }
            return retVal;
        }

    }
}
