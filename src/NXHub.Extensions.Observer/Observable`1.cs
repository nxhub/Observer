using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NXHub.Extensions.Observer
{
    public class Observable<T> : IObservable<T>
    {
        private readonly ReaderWriterLockSlim _lock;
        private readonly List<IObserver<T>> _observers;

        public Observable()
        {
            _lock = new ReaderWriterLockSlim();
            _observers = new List<IObserver<T>>();
        }

        public IDisposable Subscribe(IObserver<T> observer)
        {
            try
            {
                _lock.EnterWriteLock();

                _observers.Add(observer);
            }
            finally
            {
                _lock.ExitWriteLock();
            }

            return new Dispose(observer, this);
        }

        public void NotifyObservers()
        {
            NotifyObservers(default);
        }

        public void NotifyObservers(T arg)
        {
            try
            {
                _lock.EnterReadLock();

                Parallel.ForEach(_observers, observer =>
                {
                    observer.OnNext(arg);
                });
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        private void UnSubscribe(IObserver<T> observer)
        {
            try
            {
                _lock.EnterWriteLock();

                _observers.Remove(observer);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        private class Dispose : IDisposable
        {
            private readonly IObserver<T> _observer;
            private readonly Observable<T> _observable;

            public Dispose(
                IObserver<T> observer,
                Observable<T> observable)
            {
                _observer = observer;
                _observable = observable;
            }

            void IDisposable.Dispose()
            {
                _observable.UnSubscribe(_observer);
            }
        }
    }
}
