using System;
namespace Zenith.Abstractions
{
    public class DashboardAccountItem
    {
        public DashboardAccountItem()
        {
        }

        public string AccountType { get; set; }
        public string AccountNumber { get; set; }
        public string Amount { get; set; }
        public string AccountName { get; set; }
    }
}
