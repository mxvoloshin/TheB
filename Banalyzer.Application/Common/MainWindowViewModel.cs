using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Banalyzer.Application.Deposite.View;
using Banalyzer.Application.Deposite.ViewModel;
using MvvmCommon;

namespace Banalyzer.Application.Common
{
    public class MainWindowViewModel : ViewModelBase, IRequestCloseViewModel, IDisplayMessageInContent
    {
        private readonly IServiceFactory _serviceFactory;
        private readonly ViewModelLocator _vmLocator = new ViewModelLocator();
        public event EventHandler RequestClose;

        public MainWindowViewModel(IServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;

            ShowDepositesCommand = new RelayCommand(ShowDeposites);
            CloseApplicationCommand = new RelayCommand(CloseApplication);
        }

        private MessageViewModel _errorViewModel;
        public MessageViewModel ErrorViewModel
        {
            get { return _errorViewModel; }
            set
            {
                _errorViewModel = value;
                OnPropertyChanged();
            }
        }

        private QuestionViewModel _questionViewModel;
        public QuestionViewModel QuestionViewModel
        {
            get { return _questionViewModel; }
            set
            {
                _questionViewModel = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand CloseApplicationCommand { get; set; }
        private void CloseApplication()
        {
            RequestClose(this, null);
        }
        
        private bool _locked;
        public bool IsLocked
        {
            get { return _locked; }
            set
            {
                _locked = value;
                OnPropertyChanged();
            }
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
                
                SubscribeEvents(value);

                _currentViewModel = value;
                OnPropertyChanged();
            }
        }

        private void SubscribeEvents(ViewModelBase viewmodel)
        {
            if (viewmodel is DepositesGeneralViewModel)
            {
                SubscribeDepositesEvents(viewmodel as DepositesGeneralViewModel);
            }
        }

        private void UnSubscribeEvents(ViewModelBase viewmodel)
        {
            if (viewmodel is DepositesGeneralViewModel)
            {
                UnSubscribeDepositesEvents(viewmodel as DepositesGeneralViewModel);
            }
        }

        public ICommand ShowDepositesCommand { get; set; }
        private async void ShowDeposites()
        {
            //CurrentViewModel = new WaitingViewModel();
            IsLocked = true;
            CurrentViewModel = await _vmLocator.DepositeGeneralViewModel();
            IsLocked = false;
        }

        private void SubscribeDepositesEvents(DepositesGeneralViewModel viewModel)
        {
            
        }
        private void UnSubscribeDepositesEvents(DepositesGeneralViewModel viewModel)
        {
            
        }
    }
}