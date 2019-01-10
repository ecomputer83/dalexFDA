using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PropertyChanged;
using Xamarin.Forms;
using Zenith.Abstractions;

namespace Zenith
{
    [AddINotifyPropertyChangedInterface]
    public class ManualDepositViewModel : BaseViewModel
    {
        internal readonly IErrorManager ErrorManager;

        readonly IDepositService DepositService;

        public List<string> Banks { get; set; }
        public string BankName { get; set; }
        public string BankCode { get; set; }

        List<Bank> AllBanks { get; set; }

        public Command ChangeBank { get; set; }

        public ManualDepositViewModel(IErrorManager ErrorManager, IDepositService DepositService)
        {
            this.ErrorManager = ErrorManager;
            this.DepositService = DepositService;

            ChangeBank = new Command(async () => await ExecuteChangeBank());
        }

        public async override void Init(object initData)
        {
            base.Init(initData);

            try
            {
                AllBanks = await DepositService.GetBanksAsync();
                Banks = AllBanks.Select(x => x.BankName).OrderBy(x => x).ToList();
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }

        private async Task ExecuteChangeBank()
        {
            try
            {
                BankCode = AllBanks.Where(x => x.BankName == BankName).Select(x => x.BankCode.ToString()).FirstOrDefault();
            }
            catch (Exception ex)
            {
                await ErrorManager.DisplayErrorMessageAsync(ex);
            }
        }
    }
}
