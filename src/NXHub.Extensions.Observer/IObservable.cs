using System;

namespace NXHub.Extensions.Observer
{
    public interface IObservable : IObservable<object>
    {
        void NotifyObservers();
        void NotifyObservers(object arg);
    }
}