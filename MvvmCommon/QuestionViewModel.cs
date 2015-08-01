using System;

namespace MvvmCommon
{
    public enum QuestionResult
    {
        Yes,
        No,
        Cancel
    };

    public class CloseEventArgs : EventArgs
    {

        public CloseEventArgs(QuestionResult result)
        {
            Result = result;
        }

        public QuestionResult Result
        {
            get; private set;
        }
    }

    public class QuestionViewModel : ViewModelBase
    {
        public EventHandler<CloseEventArgs> CloseEvent;

        protected virtual void OnCloseEvent()
        {
            var handler = CloseEvent;
            if (handler != null)
            {
                handler(this, new CloseEventArgs(Result));
            }
        }

        public QuestionViewModel()
        {
            QuestionWindowYesCommand = new RelayCommand(QuestionWindowYes);
            QuestionWindowNoCommand = new RelayCommand(QuestionWindowNo);
            QuestionWindowCancelCommand = new RelayCommand(QuestionWindowCancel);
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

        public QuestionResult Result
        {
            get; private set;
        }

        public RelayCommand QuestionWindowYesCommand { get; set; }
        private void QuestionWindowYes()
        {
            Result = QuestionResult.Yes;
            IsShowMessage = false;
            OnCloseEvent();
        }

        public RelayCommand QuestionWindowNoCommand { get; set; }
        private void QuestionWindowNo()
        {
            Result = QuestionResult.No;
            IsShowMessage = false;
            OnCloseEvent();
        }

        public RelayCommand QuestionWindowCancelCommand { get; set; }
        private void QuestionWindowCancel()
        {
            Result = QuestionResult.Cancel;
            IsShowMessage = false;
            OnCloseEvent();
        }

        private String _question = String.Empty;
        public String Question
        {
            get
            {
                return _question;
            }
            set
            {
                _question = value;
                OnPropertyChanged();
            }
        }
    }
}