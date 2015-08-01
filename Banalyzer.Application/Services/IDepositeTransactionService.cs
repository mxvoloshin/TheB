using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Banalyzer.Domain.MoneyTransaction;

namespace Banalyzer.Application.Services
{
    public interface IDepositeTransactionService
    {
        Task AddDepositeTransaction(DepositeMoneyTransaction depositeTransaction);
        Task UpdateDepositeTransaction(DepositeMoneyTransaction depositeTransaction);
        Task RemoveDepositeTransaction(DepositeMoneyTransaction depositeTransaction);
        Task<List<DepositeMoneyTransaction>> GetDepositeTransactions(Domain.Common.Deposite deposite); 
    }
}