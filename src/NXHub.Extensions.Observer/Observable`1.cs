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

        public void AddObserver(IObserver<T> observer)
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
        }

        public void DeleteObserver(IObserver<T> observer)
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

        public void NotifyObservers(T arg)
        {
            try
            {
                _lock.EnterReadLock();

                Parallel.ForEach(_observers, observer =>
                {
                    observer.Update(this, arg);
                });
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        public void NotifyObservers()
        {
            NotifyObservers(default);
        }
    }
}
