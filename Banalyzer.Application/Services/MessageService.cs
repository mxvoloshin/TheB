using System;
using System.Data.Entity.Validation;
using System.Linq;
using System.Windows;
using Banalyzer.Application.Common;
using Banalyzer.Application.Helpers;
using MvvmCommon;

namespace Banalyzer.Application.Services
{
    public class MessageService : IMessagesService
    {
        private readonly ViewModelLocator _vmLocator = new ViewModelLocator();

        public void ShowExceptionInsideView(Exception ex, IDisplayMessageInContent model)
        {
            model.ErrorViewModel = new MessageViewModel
            {
                ErrorMessage = ex.ToErrorMessage(),
                IsShowMessage = true
            };
        }

        public void ShowQuestionInsideView(String question, IDisplayMessageInContent model, EventHandler<CloseEventArgs> continuation)
        {
            var vm = new QuestionViewModel
            {
                Question = question,
                IsShowMessage = true
            };
            vm.CloseEvent += continuation;
            
            model.QuestionViewModel = vm;
        }

        public void BlockMainWindow()
        {
            _vmLocator.MainViewModel.IsLocked = true;
        }

        public void UnBlockMainWindow()
        {
            _vmLocator.MainViewModel.IsLocked = false;
        }
    }
}