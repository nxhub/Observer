namespace NXHub.Extensions.Observer
{
    public interface IObservable
    {
        void AddObserver(IObserver observer);
        void DeleteObserver(IObserver observer);
        void NotifyObservers();
        void NotifyObservers(object arg);
    }
}