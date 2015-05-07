using System;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Input;
using Banalyzer.Application.Deposite.ViewModel;
using MvvmCommon;

namespace Banalyzer.Application.Common
{
    public class MainWindowViewModel : ViewModelBase
    {
        
        private readonly IServiceFactory _serviceFactory;

        public MainWindowViewModel(IServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
            ShowDepositeCommand = new RelayCommand(ShowDeposite);
        }

        private ViewModelBase _currentViewModel;
        public ViewModelBase CurrentViewModel
        {
            get
            {
                return _currentViewModel;
            }
            set
            {
                _currentViewModel = value;
                OnPropertyChanged("CurrentViewModel");
            }
        }

        public ICommand ShowDepositeCommand { get; set; }
        private void ShowDeposite()
        {
            CurrentViewModel = new DepositesViewModel(_serviceFactory);
        }
    }
}