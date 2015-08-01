using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace MvvmCommon
{
    public class ViewModelWithValidation : ViewModelBase, INotifyDataErrorInfo
    {
        private readonly ConcurrentDictionary<string, List<string>> _errors = new ConcurrentDictionary<string, List<string>>();
        public IEnumerable GetErrors(string propertyName)
        {
            return _errors.ContainsKey(propertyName) ? _errors[propertyName] : null;
        }

        public bool HasErrors
        {
            get
            {
                return _errors.Any();
            }
        }

        public void AddError(string error, string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                return;
            }

            _errors.AddOrUpdate(propertyName, new List<string> { error }, (property, value) =>
            {
                value.Add(error);
                return value;
            });

            OnErrorsChanged(propertyName);

            IsValid = !HasErrors;
        }

        public void ClearErrors()
        {
            _errors.Clear();    
        }

        public void RemoveError(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                return;
            }

            List<string> values;
            _errors.TryRemove(propertyName, out values);

            IsValid = !HasErrors;
        }

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        private void OnErrorsChanged(string propertyName)
        {
            if (ErrorsChanged != null)
            {
                ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
            }
        }

        private bool _isValid = true;
        public Boolean IsValid
        {
            get { return _isValid; }
            set
            {
                _isValid = value;
                OnPropertyChanged();
            }
        }
    }
}