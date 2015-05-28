using System;
using Banalyzer.Application.Deposite.View;
using Banalyzer.Application.Deposite.ViewModel;
using Banalyzer.DAL.Repository;
using Domain.DAL.Repository;
using Microsoft.Practices.Unity;

namespace Banalyzer.Application.Common
{
    public class ViewModelLocator
    {
        private readonly static IUnityContainer _container;
        static ViewModelLocator()
        {
            _container = new UnityContainer();
            
            _container.RegisterType(typeof(IRepository<Domain.Common.Deposite, Guid>),
                typeof(Repository<Domain.Common.Deposite, Guid>));

            _container.RegisterInstance(typeof (IServiceFactory), new ServiceFactory(_container));

            _container.RegisterType<MainWindowViewModel, MainWindowViewModel>();
            _container.RegisterType<DepositeViewModel, DepositeViewModel>();
        }

        public MainWindowViewModel MainViewModel
        {
            get
            {
                var vm = _container.Resolve<MainWindowViewModel>();
                object hash = vm.GetHashCode();
                return vm;
            }
        }

        public DepositeViewModel DepositeViewModel
        {
            get
            {
                var vm = _container.Resolve<DepositeViewModel>();
                object hash = vm.GetHashCode();
                return vm;
            }
        }
    }
}