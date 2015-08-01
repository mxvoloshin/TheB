using System;
using MvvmCommon;

namespace Banalyzer.Application.Services
{
    public interface IMessagesService
    {
        void ShowExceptionInsideView(Exception ex, IDisplayMessageInContent model);
        void ShowQuestionInsideView(String question, IDisplayMessageInContent model, EventHandler<CloseEventArgs> continuation);

        void BlockMainWindow();
        void UnBlockMainWindow();
    }
}