using System.Windows;
using AutoMapper;
using Banalyzer.Application.Common;
using Banalyzer.Application.Deposite.Model;
using Banalyzer.Application.Deposite.ViewModel;
using Banalyzer.Application.Helpers;
using Banalyzer.Domain.MoneyTransaction;
using MvvmCommon;

namespace Banalyzer.Application
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            CreateMapping();

            var mw = new MainWindow();
            mw.Show();

            this.DispatcherUnhandledException += (o, args) =>
            {
                var vmLocator = new ViewModelLocator();
                vmLocator.MainViewModel.ErrorViewModel = new MessageViewModel
                {
                    ErrorMessage = args.Exception.ToErrorMessage(),
                    IsShowMessage = true
                };

                args.Handled = true;
            };
        }

        private void CreateMapping()
        {
            Mapper.CreateMap<DepositeViewModel, Domain.Common.Deposite>();
//                .ForMember(x => x.Currency_Id, y => y.MapFrom(x=>x.Currency.Id));
//                .ForMember(x => x.Currency, y=>y.Ignore());

            Mapper.CreateMap<Domain.Common.Deposite, DepositeViewModel>()
                .ForMember(x => x.Currency, y => y.MapFrom(x => x.Currency));

            Mapper.CreateMap<DepositeMoneyTransaction, DepositeTransactionTableModel>()
                .ForMember(x => x.TransactionType, y => y.MapFrom(x=>x.TransactionType.ToString()));
        }
    }
}
