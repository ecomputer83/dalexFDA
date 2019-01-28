// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using PropertyChanged;
using Xamarin.Forms;

namespace dalexFDA
{
    [AddINotifyPropertyChangedInterface]
    public partial class ActivateViewModel
    {
        //default services
        internal readonly dalexFDA.Abstractions.IErrorManager ErrorManager;

        //other services

        //commands
        public Command Back { get; private set; }
        public Command DebitCard { get; private set; }
        public Command HardwareToken { get; private set; }
        public Command OpenAccount { get; private set; }
        public Command QuickAccess { get; private set; }
        public Command RegisterNewDevice { get; private set; }
        public Command TermsAndConditions { get; private set; }

        //properties
        public string AccountNumber { get; set; }

        public ActivateViewModel
        (
           dalexFDA.Abstractions.IErrorManager ErrorManager
        )
        {
            //setup default services
            this.ErrorManager = ErrorManager;

            //setup other services

            //setup commands
            Back = new Command(async () => await ExecuteBack());
            DebitCard = new Command(async () => await ExecuteDebitCard());
            HardwareToken = new Command(async () => await ExecuteHardwareToken());
            OpenAccount = new Command(async () => await ExecuteOpenAccount());
            QuickAccess = new Command(async () => await ExecuteQuickAccess());
            RegisterNewDevice = new Command(async () => await ExecuteRegisterNewDevice());
            TermsAndConditions = new Command(async () => await ExecuteTermsAndConditions());
  
            Setup();
        }

 

    }
}
