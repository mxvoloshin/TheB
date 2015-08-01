using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Banalyzer.Domain.Common;

namespace Banalyzer.Domain.MoneyTransaction
{
    public class DepositeMoneyTransaction : MoneyTransaction
    {
        public DepositeMoneyTransaction()
        {
            TransactionDate = DateTime.Now;
        }

        [Required]
        public Guid Deposite_Id { get; set; }

        [ForeignKey("Deposite_Id")]
        public virtual Deposite Deposite { get; set; }
    }
}