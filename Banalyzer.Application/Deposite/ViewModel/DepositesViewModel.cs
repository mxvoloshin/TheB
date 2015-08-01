using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Banalyzer.Application.Common;
using Banalyzer.Application.Helpers;
using MvvmCommon;

namespace Banalyzer.Application.Deposite.ViewModel
{
    public class DepositeEventArgs : EventArgs
    {
        public Domain.Common.Deposite Deposite { get; set; }
    }

    public class DepositesViewModel : ViewModelBase, IInitializedViewModel
    {
        private readonly IServiceFactory _serviceFactory;

        public event EventHandler<EventArgs> AddDepositeEvent;
        public event EventHandler<DepositeEventArgs> EditDepositeEvent;
        public event EventHandler<DepositeEventArgs> RemoveDepositeEvent;
        public event EventHandler<DepositeEventArgs> DepositeSelectedEvent;

        private readonly ViewModelLocator _vmLocator = new ViewModelLocator();

        public DepositesViewModel(IServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;

            AddDepositeCommand = new RelayCommand(AddDeposite);
            EditDepositeCommand = new RelayCommand(EditDeposite);
            RemoveDepositeCommand = new RelayCommand(RemoveDeposite);
        }

        public async Task Initialize()
        {
            _deposites = await _serviceFactory.DepositeService().GetDeposites();
        }

        private bool _isDepositeSelected = false;
        public bool IsDepositeSelected
        {
            get
            {
                return _isDepositeSelected;
            }
            set
            {
                _isDepositeSelected = value;
                OnPropertyChanged();
            }
        }

        private Domain.Common.Deposite _selectedDeposite;
        public Domain.Common.Deposite SelectedDeposite
        {
            get
            {
                return _selectedDeposite;
            }
            set
            {
                _selectedDeposite = value;
                IsDepositeSelected = true;

                var eventArgs = new DepositeEventArgs {Deposite = _selectedDeposite};
                eventArgs.Rase(this, ref DepositeSelectedEvent);
            }
        }
        
        private IReadOnlyCollection<Domain.Common.Deposite> _deposites;
        public IReadOnlyCollection<Domain.Common.Deposite> Deposites
        {
            get
            {
                return _deposites;
            }
        } 

        public ICommand AddDepositeCommand { get; set; }
        public void AddDeposite()
        {
            var args = new EventArgs();
            args.Rase(this, ref AddDepositeEvent);
        }

        public ICommand EditDepositeCommand { get; set; }
        public void EditDeposite()
        {
            var args = new DepositeEventArgs {Deposite = SelectedDeposite};
            args.Rase(this, ref EditDepositeEvent);
        }

        public ICommand RemoveDepositeCommand { get; set; }
        public void RemoveDeposite()
        {
            _serviceFactory.MessagesService().ShowQuestionInsideView("Are you sure you want to remove deposite?", _vmLocator.MainViewModel,
                (sender, args) =>
                {
                    if (args.Result == QuestionResult.Yes)
                    {
                        var depositeArgs = new DepositeEventArgs {Deposite = SelectedDeposite};
                        depositeArgs.Rase(this, ref RemoveDepositeEvent);
                    }
                });
        }
    }
}
