using System;
using System.Runtime.InteropServices;

namespace Banalyzer.Domain.Common
{
    public class Deposite
    {
        public DateTime OpenedDate { get; set; }
        public DateTime CloseDate { get; set; }
        public String BankName { get; set; }
        public Decimal Amount { get; set; }
        public Currency Currency { get; set; }
        public Single Percent { get; set; }
    }
}