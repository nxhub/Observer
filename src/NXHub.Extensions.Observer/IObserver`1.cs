namespace NXHub.Extensions.Observer
{
    public interface IObserver<T>
    {
        void Update(IObservable<T> observerable, T arg);
    }
}
