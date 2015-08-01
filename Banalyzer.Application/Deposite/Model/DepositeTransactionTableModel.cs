using System;
using System.ComponentModel.DataAnnotations;
using Banalyzer.Domain.MoneyTransaction;

namespace Banalyzer.Application.Deposite.Model
{
    public class DepositeTransactionTableModel
    {
        public Int32 Id { get; set; }
        public String TransactionType { get; set; }
        public DateTime TransactionDate { get; set; }
        public Double Amount { get; set; }
        public String Comment { get; set; }
    }
}