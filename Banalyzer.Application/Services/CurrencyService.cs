using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Banalyzer.Application.Common;
using Banalyzer.Domain.Common;

namespace Banalyzer.Application.Services
{
    public class CurrencyService : ICurrencyService
    {
        private readonly IServiceFactory _serviceFactory;

        public CurrencyService(IServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }

        public Task<IReadOnlyCollection<Currency>> GetCurrencies
        {
            get
            {
                return Task.Run<IReadOnlyCollection<Currency>>(() =>
                {
                    using (var uof = _serviceFactory.UnitOfWork())
                    {
                        return uof.Repository<Currency, Int32>().All().ToList();
                    }
                });
            }
        }
    }
}