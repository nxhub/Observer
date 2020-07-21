using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NXHub.Extensions.Observer
{
    public class Observable : IObservable
    {
        private readonly List<IObserver> _observers;
        private readonly ReaderWriterLockSlim _lock;

        public Observable()
        {
            _observers = new List<IObserver>();
            _lock = new ReaderWriterLockSlim();
        }

        public void AddObserver(IObserver observer)
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

        public void DeleteObserver(IObserver observer)
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

        public void NotifyObservers(object arg)
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
            NotifyObservers(null);
        }
    }
}
