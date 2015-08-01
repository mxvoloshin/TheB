using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Banalyzer.Domain.MoneyTransaction;
using Domain.DAL;

namespace Banalyzer.Domain.Common
{
    public class Wallet : DomainEntity<Guid>
    {
        [Required]
        [MaxLength(50)]
        public String Name { get; set; }
        [MaxLength(100)]
        public String Description { get; set; }
        [Required]
        public Double Amount { get; set; }
        [Required]
        public virtual Currency Currency { get; set; }
        public virtual IList<WalletExpense> Expenses { get; set; }
        public virtual IList<WalletMoneyTransaction> MoneyTransactions { get; set; } 
    }
}