using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Banalyzer.Application.Common;
using Banalyzer.Domain.MoneyTransaction;

namespace Banalyzer.Application.Services
{
    public class DepositeTransactionService : IDepositeTransactionService
    {
        private readonly IServiceFactory _serviceFactory;

        public DepositeTransactionService(IServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }

        public Task AddDepositeTransaction(DepositeMoneyTransaction depositeTransaction)
        {
            using (var uof = _serviceFactory.UnitOfWork())
            {
                uof.Repository<DepositeMoneyTransaction, int>().Add(depositeTransaction);
                return uof.CommitTransaction();
            }
        }

        public Task UpdateDepositeTransaction(DepositeMoneyTransaction depositeTransaction)
        {
            using (var uof = _serviceFactory.UnitOfWork())
            {
                uof.Repository<DepositeMoneyTransaction, int>().Update(depositeTransaction);
                return uof.CommitTransaction();
            }
        }

        public Task RemoveDepositeTransaction(DepositeMoneyTransaction depositeTransaction)
        {
            using (var uof = _serviceFactory.UnitOfWork())
            {
                uof.Repository<DepositeMoneyTransaction, int>().Remove(depositeTransaction);
                return uof.CommitTransaction();
            }
        }

        public async Task<List<DepositeMoneyTransaction>> GetDepositeTransactions(Domain.Common.Deposite deposite)
        {
            using (var uof = _serviceFactory.UnitOfWork())
            {
                Expression<Func<DepositeMoneyTransaction, bool>> expression = (transaction) => transaction.Deposite_Id == deposite.Id;

                return await uof.Repository<DepositeMoneyTransaction, int>().
                    FindAll(expression).ToListAsync();
            }
        }
    }
}