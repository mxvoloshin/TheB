using System;
using Banalyzer.Application.Services;
using Banalyzer.DAL.UnitOfWork;
using Domain.DAL.Repository;
using Microsoft.Practices.Unity;

namespace Banalyzer.Application.Common
{
    //1568 V index W
    //parkshina.ua 0505585111 0988100111
    
    //производство и год выпуска

    //0501822947

    public class ServiceFactory : IServiceFactory
    {
        private readonly IUnityContainer _container;
        public ServiceFactory(IUnityContainer container)
        {
            _container = container;
        }
        public IRepository<Domain.Common.Deposite, Guid> DepositeRepository()
        {
            return _container.Resolve<IRepository<Domain.Common.Deposite, Guid>>();
        }

        public IBanalyzerUnitOfWork UnitOfWork()
        {
            return _container.Resolve<IBanalyzerUnitOfWork>();
        }

        public IDepositeTransactionService DepositeTransactionService()
        {
            return _container.Resolve<IDepositeTransactionService>();
        }

        public IDepositeService DepositeService()
        {
            return _container.Resolve<IDepositeService>();
        }

        public ICurrencyService CurrencyService()
        {
            return _container.Resolve<ICurrencyService>();
        }

        public IMessagesService MessagesService()
        {
            return _container.Resolve<IMessagesService>();
        }
    }
}