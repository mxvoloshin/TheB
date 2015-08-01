using System;
using Banalyzer.Application.Services;
using Banalyzer.DAL.UnitOfWork;
using Domain.DAL.Repository;

namespace Banalyzer.Application.Common
{
    public interface IServiceFactory
    {
        IBanalyzerUnitOfWork UnitOfWork();

        IDepositeTransactionService DepositeTransactionService();
        IDepositeService DepositeService();
        ICurrencyService CurrencyService();

        IMessagesService MessagesService();
    }
}