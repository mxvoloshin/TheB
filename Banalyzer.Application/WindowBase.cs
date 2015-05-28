using System.Windows;
using MvvmCommon;

namespace Banalyzer.Application
{
    public class WindowBase : Window
    {
        public WindowBase()
        {
            this.DataContextChanged += WindowBase_DataContextChanged;
        }

        void WindowBase_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var model = e.NewValue as IRequestCloseViewModel;
            if (model != null)
            {
                model.RequestClose += (s, eargs) => this.Close();
            }
        }
    }
}