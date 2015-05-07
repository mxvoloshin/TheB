using System;
using Banalyzer.Application.Services;
using Domain.DAL.Repository;

namespace Banalyzer.Application.Common
{
    public interface IServiceFactory
    {
        IRepository<Domain.Common.Deposite, Guid> DepositeRepository();
    }
}