using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PropertyChanged;
using Xamarin.Forms;
using dalexFDA.Abstractions;
using Acr.UserDialogs;

namespace dalexFDA
{
    [AddINotifyPropertyChangedInterface]
    public class ManualDepositViewModel : BaseViewModel
    {
        internal readonly IErrorManager ErrorManager;
        readonly ISession SessionService;
        readonly IInvestmentService IInvestmentService;
        readonly IUserDialogs Dialog;

        readonly IDepositService DepositService;

        public bool IsSuccessful { get; set; }
        public bool IsNotSuccesful { get { return !IsSuccessful; } }

        public DateTime TransactionDate { get; set; }
        public bool TransactionDateHasError { get; set; }
        public string TransactionDateErrorMessage { get; set; }

        public double Deposit { get; set; }
        public bool DepositHasError { get; set; }
        public string DepositErrorMessage { get; set; }

        public List<Bank> Banks { get; set; }
        public int? SelectedBankIndex { get; set; }
        public Bank SelectedBank => SelectedBankIndex != null ? Banks?.ToList()[SelectedBankIndex.GetValueOrDefault()] : null;
        public bool BankHasError { get; set; }
        public string BankErrorMessage { get; set; }

        public string TellerNumber { get; set; }
        public bool TellerNumberHasError { get; set; }
        public string TellerNumberErrorMessage { get; set; }

        public double InvestmentAmount { get; set; }
        public bool InvestmentAmountHasError { get; set; }
        public string InvestmentAmountErrorMessage { get; set; }

        public string Duration { get; set; }
        public bool DurationHasError { get; set; }
        public string DurationErrorMessage { get; set; }

        public string SecurityQuestion { get; set; }

        public string SecurityAnswer { get; set; }
        public bool SecurityAnswerHasError { get; set; }
        public string SecurityAnswerErrorMessage { get; set; }

        public Command Negotiate { get; set; }
        public Command Validate { get; private set; }

        public class CommandNav
        {
            public string Name { get; set; }
        }

        private const string duration_error_message = "Please enter a valid number of days.";
        private const string security_answer_error_message = "Please enter the answer to the question.";
        private const string wrong_security_answer_error_message = "Incorrect answer. Please try again";

        public ManualDepositViewModel(IErrorManager ErrorManager, IDepositService DepositService, ISession SessionService, IInvestmentService IInvestmentService, 
                                    IUserDialogs Dialog)
        {
            this.ErrorManager = ErrorManager;
            this.SessionService = SessionService;
            this.IInvestmentService = IInvestmentService;
            this.Dialog = Dialog;
            this.DepositService = DepositService;

            Negotiate = new Command(async () => await ExecuteNegotiate());
            Validate = new Command<CommandNav>(async (obj) => await ExecuteValidate(obj));
        }

        public async override void Init(object initData)
        {
            base.Init(initData);

            try
            {
                var banks = await DepositService.GetBanks();

                Banks = banks.ToList();

                Duration = "0";
                SecurityQuestion = SessionService?.CurrentUser?.SecurityQuestion?.ToUpper();
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }

        private async Task ExecuteNegotiate()
        {
            try
            {
                if (PerformValidation()) return;

                bool response;

                using (Dialog.Loading("Loading..."))
                {
                    var request = new InvestmentManualDeposit
                    {
                        DepositDate = TransactionDate,
                        DepositAmount = Deposit,
                        BankName = SelectedBank?.Name,
                        chequeNumber = TellerNumber,
                        InvestmentAmount = InvestmentAmount,
                        Duration = Convert.ToInt32(Duration),
                        SecurityAnswer = SecurityAnswer
                    };
                    response = await IInvestmentService.DepositManualInvestment(request);
                }

                if (response)
                    IsSuccessful = true;
                else
                    await CoreMethods.DisplayAlert("Oops", "Operation failed. Please try again", "Ok");
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }

        private async Task ExecuteValidate(CommandNav obj)
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

        private void ValidateControls(string name)
        {
            switch (name)
            {
                case "Bank":
                    BankHasError = SelectedBank == null;
                    BankErrorMessage = bank_error_message;
                    break;
                case "TellerNumber":
                    TellerNumberHasError = string.IsNullOrEmpty(TellerNumber);
                    TellerNumberErrorMessage = teller_number_error_message;
                    break;
                case "Duration":
                    DurationHasError = string.IsNullOrEmpty(Duration);
                    DurationErrorMessage = duration_error_message;
                    break;
                case "SecurityAnswer":
                    SecurityAnswerHasError = string.IsNullOrEmpty(SecurityAnswer);
                    SecurityAnswerErrorMessage = security_answer_error_message;
                    break;
            }
        }

        private const string bank_error_message = "Please select a bank.";
        private const string teller_number_error_message = "Please enter a teller / cheque number.";

        public bool PerformValidation()
        {
            BankHasError = SelectedBank == null;
            BankErrorMessage = bank_error_message;

            TellerNumberHasError = string.IsNullOrEmpty(TellerNumber);
            TellerNumberErrorMessage = teller_number_error_message;

            DurationHasError = string.IsNullOrEmpty(Duration) || Convert.ToInt32(Duration) <= 0;
            DurationErrorMessage = duration_error_message;

            SecurityAnswerHasError = string.IsNullOrEmpty(SecurityAnswer);
            SecurityAnswerErrorMessage = security_answer_error_message;

            if (!SecurityAnswerHasError)
            {
                if (SecurityAnswer.ToLower() != SessionService?.CurrentUser?.SecurityAnswer.ToLower())
                {
                    SecurityAnswerHasError = true;
                    SecurityAnswerErrorMessage = wrong_security_answer_error_message;
                }
            }

            return SecurityAnswerHasError || DurationHasError;
        }
    }
}
