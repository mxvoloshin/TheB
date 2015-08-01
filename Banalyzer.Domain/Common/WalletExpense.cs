using System;
using System.ComponentModel.DataAnnotations;
using Banalyzer.Domain.Category;
using Domain.DAL;

namespace Banalyzer.Domain.Common
{
    public class WalletExpense : DomainEntity<Guid>
    {
        [Required]
        public DateTime ExpenseDate { get; set; }
        [Required]
        public Double Amount { get; set; }
        [Required]
        public virtual Wallet SourceWallet { get; set; }
        [Required]
        public virtual ExpenseSection Section { get; set; }
        [Required]
        public virtual ExpenseSubCategory SubCategory { get; set; }
        public virtual ExpenseTag Tag { get; set; }
    }
}