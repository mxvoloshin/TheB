using System.Collections.Generic;
using System.Threading.Tasks;

namespace Banalyzer.Application.Services
{
    public interface IDepositeService
    {
        Task AddNewDeposite(Domain.Common.Deposite entity);
        Task UpdateDeposite(Domain.Common.Deposite entity);
        Task RemoveDeposite(Domain.Common.Deposite entity);
        Task<IReadOnlyCollection<Domain.Common.Deposite>> GetDeposites();
    }
}