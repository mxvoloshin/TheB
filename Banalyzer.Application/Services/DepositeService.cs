using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Banalyzer.Application.Common;

namespace Banalyzer.Application.Services
{
    public class DepositeService : IDepositeService
    {
        private readonly IServiceFactory _serviceFactory;

        public DepositeService(IServiceFactory factory)
        {
            _serviceFactory = factory;
        }

        public Task AddNewDeposite(Domain.Common.Deposite entity)
        {
            using (var uof = _serviceFactory.UnitOfWork())
            {
                var repository = uof.Repository<Domain.Common.Deposite, Guid>();
                repository.Add(entity);

                return uof.CommitTransaction();
            }
        }

        public Task UpdateDeposite(Domain.Common.Deposite entity)
        {
            using (var uof = _serviceFactory.UnitOfWork())
            {
                var repository = uof.Repository<Domain.Common.Deposite, Guid>();
                repository.Update(entity);

                return uof.CommitTransaction();
            }
        }

        public Task RemoveDeposite(Domain.Common.Deposite entity)
        {
            using (var uof = _serviceFactory.UnitOfWork())
            {
                var repository = uof.Repository<Domain.Common.Deposite, Guid>();
                repository.Remove(entity);

                return uof.CommitTransaction();
            }
        }

        public Task<IReadOnlyCollection<Domain.Common.Deposite>> GetDeposites()
        {
            return Task.Run<IReadOnlyCollection<Domain.Common.Deposite>>(() =>
            {
                using (var uof = _serviceFactory.UnitOfWork())
                {
                    return uof.Repository<Domain.Common.Deposite, Guid>().All()
                              .Include(x=>x.Currency).ToList();
                }
            });
        }
    }
}