using System;
using System.Windows.Input;
using Banalyzer.Application.Common;
using MvvmCommon;

namespace Banalyzer.Application.Deposite.ViewModel
{
    public class DepositesViewModel : ViewModelBase
    {
        private readonly IServiceFactory _serviceFactory;

        public event EventHandler AddDepositeEvent;

        public DepositesViewModel(IServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;

            AddDepositeCommand = new RelayCommand(AddDeposite);

        }

        public ICommand AddDepositeCommand { get; set; }

        public void AddDeposite()
        {

            AddDepositeEvent(this, new EventArgs());
        }
    }
}