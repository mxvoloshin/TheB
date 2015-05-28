using System;
using Domain.DAL.Repository;
using Microsoft.Practices.Unity;

namespace Banalyzer.Application.Common
{
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
    }
}