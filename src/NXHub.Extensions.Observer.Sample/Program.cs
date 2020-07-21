using System;

namespace NXHub.Extensions.Observer.Sample
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Test();
        }

        public static void Test()
        {
            Observable<int> observable = new Observable<int>();

            new Observer(observable);

            observable.NotifyObservers();
        }
    }

    internal class Observer : IObserver<int>
    {
        public Observer(Observable<int> observable)
        {
            observable.AddObserver(this);
        }

        public void Update(IObservable<int> observable, int arg)
        {
            Console.WriteLine("Observer: Update.");
        }
    }
}
