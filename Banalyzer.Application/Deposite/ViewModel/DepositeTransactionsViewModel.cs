using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Banalyzer.Application.Common;
using Banalyzer.Application.Deposite.Model;
using Banalyzer.Domain.MoneyTransaction;
using Microsoft.Practices.Unity.Configuration;
using MvvmCommon;

namespace Banalyzer.Application.Deposite.ViewModel
{
    public class DepositeTransactionsViewModel : ViewModelWithValidation, IInitializedViewModel
    {
        private readonly IServiceFactory _serviceFactory;
        private readonly ViewModelLocator _vmLocator = new ViewModelLocator();
        private readonly Domain.Common.Deposite _deposite;
        private IList<DepositeMoneyTransaction> _depositeTransactionModels = new List<DepositeMoneyTransaction>();

        public DepositeTransactionsViewModel(IServiceFactory serviceFactory, Domain.Common.Deposite deposite)
        {
            _serviceFactory = serviceFactory;

            AddDepositeInTransactionCommand = new RelayCommand(AddDepositeInTransaction);
            AddDepositeOutTransactionCommand = new RelayCommand(AddDepositeOutTransaction);
            EditDepositeTransactionCommand = new RelayCommand(EditDepositeTransaction);
            RemoveDepositeTransactionCommand = new RelayCommand(RemoveDepositeTransaction);

            _deposite = deposite;
        }

        public async Task Initialize()
        {
            var entities = await _serviceFactory.DepositeTransactionService().GetDepositeTransactions(_deposite);
            entities = entities.OrderByDescending(x => x.TransactionDate).ToList();
            
            _depositeTransactionModels = new List<DepositeMoneyTransaction>(entities);

            var tableModels = entities.Select(Mapper.Map<DepositeTransactionTableModel>).ToList();
            DepositeTransactions = new ReadOnlyCollection<DepositeTransactionTableModel>(tableModels);
        }

        private DepositeTransactionTableModel _selectedTransaction;
        public DepositeTransactionTableModel SelectedTableTransaction
        {
            get
            {
                return _selectedTransaction;
            }
            set
            {
                _selectedTransaction = value;
                OnPropertyChanged();

                if (_selectedTransaction != null)
                {
                    SelectedTransaction = _depositeTransactionModels.FirstOrDefault(x => x.Id == _selectedTransaction.Id);
                }

                IsEditMode = _selectedTransaction != null;
            }
        }

        private DepositeMoneyTransaction _transactionModel = new DepositeMoneyTransaction();
        public DepositeMoneyTransaction SelectedTransaction
        {
            get
            {
                return _transactionModel;
            }
            set
            {
                _transactionModel = value;
                OnPropertyChanged();
            }
        }

        private bool _isEditMode = false;

        public bool IsEditMode
        {
            get { return _isEditMode; }
            set
            {
                _isEditMode = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand RemoveDepositeTransactionCommand { get; set; }
        private async void RemoveDepositeTransaction()
        {
            _serviceFactory.MessagesService().BlockMainWindow();
            try
            {
                await _serviceFactory.DepositeTransactionService().RemoveDepositeTransaction(SelectedTransaction);
                await Initialize();
            }
            catch (Exception ex)
            {
                _serviceFactory.MessagesService().ShowExceptionInsideView(ex, _vmLocator.MainViewModel);
            }
            finally
            {
                _serviceFactory.MessagesService().UnBlockMainWindow();
            }
        }
        
        public RelayCommand EditDepositeTransactionCommand { get; set; }
        private async void EditDepositeTransaction()
        {
            _serviceFactory.MessagesService().BlockMainWindow();
            try
            {
                await _serviceFactory.DepositeTransactionService().UpdateDepositeTransaction(SelectedTransaction);
                await Initialize();
            }
            catch (Exception ex)
            {
                _serviceFactory.MessagesService().ShowExceptionInsideView(ex, _vmLocator.MainViewModel);
            }
            finally
            {
                _serviceFactory.MessagesService().UnBlockMainWindow();
            }
        }

        public RelayCommand AddDepositeInTransactionCommand { get; set; }
        private async void AddDepositeInTransaction()
        {
            await AddDepositeTransaction(MoneyTransactionType.Income);
        }
       
        public RelayCommand AddDepositeOutTransactionCommand { get; set; }
        private async void AddDepositeOutTransaction()
        {
            await AddDepositeTransaction(MoneyTransactionType.Outcome);
        }

        private async Task AddDepositeTransaction(MoneyTransactionType transactionType)
        {
            _serviceFactory.MessagesService().BlockMainWindow();
            try
            {
                SelectedTransaction.Deposite_Id = _deposite.Id;
                SelectedTransaction.TransactionType = transactionType;

                await _serviceFactory.DepositeTransactionService().AddDepositeTransaction(SelectedTransaction);
                await Initialize();
            }
            catch (Exception ex)
            {
                _serviceFactory.MessagesService().ShowExceptionInsideView(ex, _vmLocator.MainViewModel);
            }
            finally
            {
                _serviceFactory.MessagesService().UnBlockMainWindow();
            }
        }

        private IReadOnlyCollection<DepositeTransactionTableModel> _depositeTransactions;
        public IReadOnlyCollection<DepositeTransactionTableModel> DepositeTransactions
        {
            get
            {
                return _depositeTransactions;
            }
            private set
            {
                _depositeTransactions = value;
                OnPropertyChanged();
            }

        }
    }
}
