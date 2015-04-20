using Banalyzer.Domain.Common;

namespace Banalyzer.Domain.MoneyTransaction
{
    public class DepositeMoneyTransaction : MoneyTransaction
    {
        public virtual Deposite Deposite { get; set; }
    }
}