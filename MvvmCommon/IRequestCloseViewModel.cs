using System;

namespace MvvmCommon
{
    public interface IRequestCloseViewModel
    {
        event EventHandler RequestClose;
    }
}