using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using AutoMapper;
using Banalyzer.Application.Common;
using Banalyzer.Application.Helpers;
using Banalyzer.Domain.Common;
using MvvmCommon;
using MvvmCommon.Validation;

namespace Banalyzer.Application.Deposite.ViewModel
{
    public class DepositeViewModel  : ViewModelWithValidation, IInitializedViewModel
    {
        private readonly IServiceFactory _serviceFactory;
        public event EventHandler RequestCloseDepositeViewEvent;
        private readonly Domain.Common.Deposite _model;
        private readonly ViewModelLocator _vmLocator = new ViewModelLocator();

        public DepositeViewModel(IServiceFactory serviceFactory, Domain.Common.Deposite model)
        {
            _serviceFactory = serviceFactory;
            _model = model;

            if (_model.Id != Guid.Empty)
            {
                IsEditMode = true;
            }

            SaveCommand = new RelayCommand(Save);
            ExitCommand = new RelayCommand(Exit);
        }
       
        public async Task Initialize()
        {
            _currencies = await _serviceFactory.CurrencyService().GetCurrencies;
        }

        public DateTime OpenedDate
        {
            get { return _model.OpenedDate; }
            set
            {
                _model.OpenedDate = value;
                OnPropertyChanged();
            }
        }

        public DateTime CloseDate
        {
            get { return _model.CloseDate; }
            set
            {
                _model.CloseDate = value;
                OnPropertyChanged();
            }
        }

        [Required]
        public String BankName
        {
            get { return _model.BankName; }
            set
            {
                _model.BankName = value;
                OnPropertyChanged();
                ModelValidator.ValidateProperty(this);
            }
        }

        [MvvmCommon.Range(1, Double.MaxValue)]
        public Double OpenedAmount
        {
            get { return _model.OpenedAmount; }
            set
            {
                _model.OpenedAmount = value.ToCurrency();
                OnPropertyChanged();
                ModelValidator.ValidateProperty(this);
            }
        }

        [MvvmCommon.Range(1, 100)]
        public Double Percent
        {
            get { return _model.Percent; }
            set
            {
                _model.Percent = value.ToCurrency();
                OnPropertyChanged();
                ModelValidator.ValidateProperty(this);
            }
        }

        [ValidId]
        public Int32 Currency
        {
            get { return _model.Currency_Id; }
            set
            {
                _model.Currency_Id = value;

                OnPropertyChanged();
                ModelValidator.ValidateProperty(this);
            }
        }

        [Required]
        public String Owner
        {
            get { return _model.Owner; }
            set
            {
                _model.Owner = value;
                OnPropertyChanged();
                ModelValidator.ValidateProperty(this);
            }
        }

        private IReadOnlyCollection<Currency> _currencies; 
        public IReadOnlyCollection<Currency> Currencies
        {
            get
            {
                return _currencies;
            }
        }

        private bool _isLocked;
        public bool IsLocked
        {
            get
            {
                return _isLocked;
            }
            set
            {
                _isLocked = value;
                OnPropertyChanged();
            }
        }

        private bool _isEditMode;
        public bool IsEditMode
        {
            get
            {
                return _isEditMode;
            }
            private set
            {
                _isEditMode = value;
                OnPropertyChanged();
            }
        }

        public ICommand SaveCommand { get; set; }
        public async void Save()
        {
            IsLocked = true;
            try
            {
                var isValid = await ModelValidator.ValidateModel(this);
                if (!isValid)
                {
                    return;
                }

                if (!IsEditMode)
                {
                    await SaveDeposite();
                }
                else
                {
                    await EditDeposite();
                }

                RequestCloseDepositeViewEvent(this, new EventArgs());
            }
            catch (Exception ex)
            {
                _serviceFactory.MessagesService().ShowExceptionInsideView(ex, _vmLocator.MainViewModel);
            }
            finally
            {
                IsLocked = false;
            }
        }

        private Task SaveDeposite()
        {
            _model.Id = Guid.NewGuid();
            return _serviceFactory.DepositeService().AddNewDeposite(_model);
        }

        private Task EditDeposite()
        {
            return _serviceFactory.DepositeService().UpdateDeposite(_model);
        }

        public ICommand ExitCommand { get; set; }
        public void Exit()
        {
            RequestCloseDepositeViewEvent(this, new EventArgs());
        }
    }
}
