namespace NXHub.Extensions.Observer
{
    public interface IObserver
    {
        void Update(IObservable observerable, object arg);
    }
}
