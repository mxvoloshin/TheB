using System;
using System.Windows;
using System.Windows.Input;
using Banalyzer.Application.Deposite.View;
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

            ShowDepositesCommand = new RelayCommand(ShowDeposites);
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
                if (_currentViewModel != value && _currentViewModel != null)
                {
                    UnSubscribeEvents(_currentViewModel);
                }
                else
                {
                    SubscribeEvents(value);
                }

                _currentViewModel = value;
                OnPropertyChanged("CurrentViewModel");
            }
        }

        private void SubscribeEvents(ViewModelBase viewmodel)
        {
            if (viewmodel is DepositesViewModel)
            {
                SubscribeDepositesEvents(viewmodel as DepositesViewModel);
            }
        }

        private void UnSubscribeEvents(ViewModelBase viewmodel)
        {
            if (viewmodel is DepositesViewModel)
            {
                UnSubscribeDepositesEvents(viewmodel as DepositesViewModel);
            }
        }

        public ICommand ShowDepositesCommand { get; set; }
        private void ShowDeposites()
        {
            var depositesViewModel = new DepositesViewModel(_serviceFactory);

            CurrentViewModel = depositesViewModel;
        }

        public const String AddDepositeEventName = "AddDepositeEvent";
        private void SubscribeDepositesEvents(DepositesViewModel viewModel)
        {
            WeakEventManager<DepositesViewModel, EventArgs>.AddHandler(viewModel, AddDepositeEventName, AddDeposite);
        }
        private void UnSubscribeDepositesEvents(DepositesViewModel viewModel)
        {
            WeakEventManager<DepositesViewModel, EventArgs>.RemoveHandler(viewModel, AddDepositeEventName, AddDeposite);
        }

        public void AddDeposite(object sender, EventArgs args)
        {
            var view = new DepositeView();
            view.Show();
        }
    }
}