namespace NXHub.Extensions.Observer
{
    public interface IObservable<T>
    {
        void AddObserver(IObserver<T> observer);
        void DeleteObserver(IObserver<T> observer);
        void NotifyObservers();
        void NotifyObservers(T arg);
    }
}