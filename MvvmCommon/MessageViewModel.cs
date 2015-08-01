using System;

namespace MvvmCommon
{
    public class MessageViewModel : ViewModelBase
    {
        public MessageViewModel()
        {
            MessageWindowCloseCommand = new RelayCommand(MessageWindowClose);
        }

        private bool _isShowMessage;
        public bool IsShowMessage
        {
            get
            {
                return _isShowMessage;
            }
            set
            {
                _isShowMessage = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand MessageWindowCloseCommand { get; set; }
        private void MessageWindowClose()
        {
            IsShowMessage = false;
        }

        private String _errorMessage = String.Empty;
        public String ErrorMessage
        {
            get
            {
                return _errorMessage;
            }
            set
            {
                _errorMessage = value;
                OnPropertyChanged();
            }
        }
    }
}