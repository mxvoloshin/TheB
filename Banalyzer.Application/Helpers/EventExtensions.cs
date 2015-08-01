using System;
using System.Threading;

namespace Banalyzer.Application.Helpers
{
    public static class EventExtensions
    {
        public static void Rase<TEventArgs>(this TEventArgs args, object sender, ref EventHandler<TEventArgs> eventDelegate) where TEventArgs : EventArgs
        {
            var temp = Volatile.Read(ref eventDelegate);
            if (temp != null)
            {
                temp(sender, args);
            }
        }
    }
}