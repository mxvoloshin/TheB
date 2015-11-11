using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Banalyzer.Application.Common;
using MvvmCommon;

namespace Banalyzer.Application.Deposite.ViewModel
{
    public class DepositesGeneralViewModel : ViewModelBase, IInitializedViewModel
    {
        private readonly IServiceFactory _serviceFactory;
        private readonly ViewModelLocator _vmLocator = new ViewModelLocator();

        public DepositesGeneralViewModel(IServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }

        public async Task Initialize()
        {
            DepositesViewModel = await _vmLocator.DepositesViewModel();
        }

        private ViewModelBase _depositesViewModel;
        public ViewModelBase DepositesViewModel
        {
            get
            {
                return _depositesViewModel;
            }
            set
            {
                if (_depositesViewModel != null)
                {
                    UnSubscribeEvents(_depositesViewModel);
                }

                SubscribeEvents(value);

                _depositesViewModel = value;
                OnPropertyChanged();
            }
        }

        private ViewModelBase _transactionsViewModel;
        public ViewModelBase DepositeTransactionViewModel
        {
            get
            {
                return _transactionsViewModel;
            }
            set
            {
                if (_transactionsViewModel != null)
                {
                    UnSubscribeEvents(_transactionsViewModel);
                }

                SubscribeEvents(value);

                _transactionsViewModel = value;
                OnPropertyChanged();
            }
        }

        public const String AddDepositeEventName = @"AddDepositeEvent";
        public const String EditDepositeEventName = @"EditDepositeEvent";
        public const String RemoveDepositeEventName = @"RemoveDepositeEvent";
        public const String DepositeSelectedEventName = @"DepositeSelectedEvent";

        public const String RequestCloseDepositeViewEventName = @"RequestCloseDepositeViewEvent";

        private void SubscribeEvents(ViewModelBase viewmodel)
        {
            if (viewmodel is DepositesViewModel)
            {
                WeakEventManager<DepositesViewModel, EventArgs>.AddHandler(viewmodel as DepositesViewModel, AddDepositeEventName, AddDeposite);
                WeakEventManager<DepositesViewModel, DepositeEventArgs>.AddHandler(viewmodel as DepositesViewModel, EditDepositeEventName, EditDeposite);
                WeakEventManager<DepositesViewModel, DepositeEventArgs>.AddHandler(viewmodel as DepositesViewModel, RemoveDepositeEventName, RemoveDeposite);
                WeakEventManager<DepositesViewModel, DepositeEventArgs>.AddHandler(viewmodel as DepositesViewModel, DepositeSelectedEventName, DepositeSelected);
            }
            else if (viewmodel is DepositeViewModel)
            {
                WeakEventManager<DepositeViewModel, EventArgs>.AddHandler(viewmodel as DepositeViewModel, RequestCloseDepositeViewEventName, RequestCloseDepositeView);
            }
            else if (viewmodel is DepositeTransactionsViewModel)
            {
            }
        }

        private void UnSubscribeEvents(ViewModelBase viewmodel)
        {
            if (viewmodel is DepositesViewModel)
            {
                WeakEventManager<DepositesViewModel, EventArgs>.RemoveHandler(viewmodel as DepositesViewModel, AddDepositeEventName, AddDeposite);
                WeakEventManager<DepositesViewModel, DepositeEventArgs>.RemoveHandler(viewmodel as DepositesViewModel, EditDepositeEventName, EditDeposite);
                WeakEventManager<DepositesViewModel, DepositeEventArgs>.RemoveHandler(viewmodel as DepositesViewModel, RemoveDepositeEventName, RemoveDeposite);
                WeakEventManager<DepositesViewModel, DepositeEventArgs>.RemoveHandler(viewmodel as DepositesViewModel, DepositeSelectedEventName, DepositeSelected);
            }
            else if (viewmodel is DepositeViewModel)
            {
                WeakEventManager<DepositeViewModel, EventArgs>.RemoveHandler(viewmodel as DepositeViewModel, RequestCloseDepositeViewEventName, RequestCloseDepositeView);

            }
        }

        public async void AddDeposite(object sender, EventArgs args)
        {
            _serviceFactory.MessagesService().BlockMainWindow();

            try
            {
                DepositeTransactionViewModel = null;
                DepositesViewModel = await _vmLocator.DepositeViewModel();
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

        public async void EditDeposite(object sender, DepositeEventArgs args)
        {
            _serviceFactory.MessagesService().BlockMainWindow();

            try
            {
                DepositeTransactionViewModel = null;
                DepositesViewModel = await _vmLocator.DepositeEditViewModel(args.Deposite);
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

        public async void RemoveDeposite(object sender, DepositeEventArgs args)
        {
            _serviceFactory.MessagesService().BlockMainWindow();

            try
            {
                DepositeTransactionViewModel = null;
                await _serviceFactory.DepositeService().RemoveDeposite(args.Deposite);
                DepositesViewModel = await _vmLocator.DepositesViewModel();
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

        public async void DepositeSelected(object sender, DepositeEventArgs args)
        {
            _serviceFactory.MessagesService().BlockMainWindow();

            try
            {
                DepositeTransactionViewModel = await _vmLocator.DepositeTransactionsViewModel(args.Deposite);
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

        public async void RequestCloseDepositeView(object sender, EventArgs args)
        {
            DepositesViewModel = await _vmLocator.DepositesViewModel();
        }
    }
}