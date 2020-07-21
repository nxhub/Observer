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
            var observable = new Observable<Message>();

            new Observer(observable);

            observable.NotifyObservers();
        }
    }

    internal class Observer : IObserver<Message>
    {
        public Observer(Observable<Message> observable)
        {
            observable.AddObserver(this);
        }

        public void Update(IObservable<Message> observable, Message arg)
        {
            Console.WriteLine("Observer: Update.");
        }
    }

    internal class Message { }
}
