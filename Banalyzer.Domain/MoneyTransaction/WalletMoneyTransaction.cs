using Banalyzer.Domain.Common;

namespace Banalyzer.Domain.MoneyTransaction
{
    public class WalletMoneyTransaction : MoneyTransaction
    {
        public virtual Wallet Wallet { get; set; }
    }
}