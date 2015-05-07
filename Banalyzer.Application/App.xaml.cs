using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Banalyzer.Application.Common;
using Banalyzer.DAL.Repository;
using Domain.DAL.Repository;
using Microsoft.Practices.Unity;

namespace Banalyzer.Application
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var container = new UnityContainer();
            container.RegisterType(typeof (IRepository<Domain.Common.Deposite, Guid>),
                typeof (Repository<Domain.Common.Deposite, Guid>));

            var mw = new MainWindow();
            mw.DataContext = new MainWindowViewModel(new ServiceFactory(container));
            mw.Show();
        }
    }
}
