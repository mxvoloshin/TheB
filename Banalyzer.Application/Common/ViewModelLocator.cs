using System;
using System.Threading.Tasks;
using Banalyzer.Application.Deposite.View;
using Banalyzer.Application.Deposite.ViewModel;
using Banalyzer.Application.Services;
using Banalyzer.DAL.Repository;
using Banalyzer.DAL.UnitOfWork;
using Domain.DAL.Repository;
using Microsoft.Practices.Unity;
using MvvmCommon;

namespace Banalyzer.Application.Common
{
    public class ViewModelLocator
    {
        private readonly static IUnityContainer _container;
        static ViewModelLocator()
        {
            _container = new UnityContainer();
            
            _container.RegisterType(typeof(IBanalyzerUnitOfWork), typeof(BanalyzerUnitOfWork));

            _container.RegisterType(typeof(IDepositeTransactionService), typeof(DepositeTransactionService));
            _container.RegisterType(typeof(IDepositeService), typeof(DepositeService));
            _container.RegisterType(typeof(ICurrencyService), typeof(CurrencyService));

            _container.RegisterType(typeof(IMessagesService), typeof(MessageService));

            _container.RegisterInstance(typeof (IServiceFactory), new ServiceFactory(_container));

            _container.RegisterType<MainWindowViewModel, MainWindowViewModel>();
            _container.RegisterType<MessageViewModel, MessageViewModel>();

            _container.RegisterType<DepositesGeneralViewModel, DepositesGeneralViewModel>();
            _container.RegisterType<DepositeViewModel, DepositeViewModel>();
            _container.RegisterType<DepositesViewModel, DepositesViewModel>();

            _container.RegisterType<DepositeTransactionsViewModel, DepositeTransactionsViewModel>();
        }

        private static MainWindowViewModel _mainViewModel;
        public MainWindowViewModel MainViewModel
        {
            get
            {
                if (_mainViewModel != null)
                {
                    return _mainViewModel;
                }

                _mainViewModel = _container.Resolve<MainWindowViewModel>();
                return _mainViewModel;
            }
        }

        public MessageViewModel MessageViewModel()
        {
            var vm = _container.Resolve<MessageViewModel>();
            return vm;
        }

        public async Task<DepositesGeneralViewModel> DepositeGeneralViewModel()
        {
            var vm = _container.Resolve<DepositesGeneralViewModel>();
            await vm.Initialize();
            return vm;
        }

        public async Task<DepositeViewModel> DepositeViewModel()
        {
            var vm = _container.Resolve<DepositeViewModel>();
            await vm.Initialize();
            return vm;
        }

        public async Task<DepositeViewModel> DepositeEditViewModel(Domain.Common.Deposite deposite)
        {
            var vm = _container.Resolve<DepositeViewModel>(new ResolverOverride[]
                                   {
                                       new ParameterOverride("model", deposite)
                                   });
            await vm.Initialize();
            return vm;
        }

        public async Task<DepositesViewModel> DepositesViewModel()
        {
            var vm = _container.Resolve<DepositesViewModel>();
            await vm.Initialize();
            return vm;
        }

        public async Task<DepositeTransactionsViewModel> DepositeTransactionsViewModel(Domain.Common.Deposite deposite)
        {
            var vm = _container.Resolve<DepositeTransactionsViewModel>(new ResolverOverride[]
                                   {
                                       new ParameterOverride("deposite", deposite)
                                   });
            await vm.Initialize();
            return vm;
        }
    }
}