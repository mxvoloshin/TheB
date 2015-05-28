using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Banalyzer.Application.Common;
using MvvmCommon;

namespace Banalyzer.Application.Deposite.ViewModel
{
    public class DepositeViewModel  : ViewModelBase, IRequestCloseViewModel
    {
        private readonly IServiceFactory _serviceFactory;
        public event EventHandler RequestClose;

        public DepositeViewModel(IServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;

            SaveCommand = new RelayCommand(Save);
        }

        public ICommand SaveCommand { get; set; }
        public void Save()
        {

            RequestClose(this, new EventArgs());
        }
    }
}
