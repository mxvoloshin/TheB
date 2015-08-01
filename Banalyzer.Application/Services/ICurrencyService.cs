using System.Collections.Generic;
using System.Threading.Tasks;
using Banalyzer.Domain.Common;

namespace Banalyzer.Application.Services
{
    public interface ICurrencyService
    {
        Task<IReadOnlyCollection<Currency>> GetCurrencies { get; }
    }
}