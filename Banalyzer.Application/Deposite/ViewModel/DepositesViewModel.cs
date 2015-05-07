using System;
using Banalyzer.Application.Common;
using Domain.DAL.Repository;
using MvvmCommon;

namespace Banalyzer.Application.Deposite.ViewModel
{
    public class DepositesViewModel : ViewModelBase
    {
        private readonly IServiceFactory _serviceFactory;
        
        public DepositesViewModel(IServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }    
    }
}