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
            observable.Subscribe(this);
        }

        public void OnCompleted()
        {
        }

        public void OnError(Exception error)
        {
        }

        public void OnNext(Message value)
        {
            Console.WriteLine("Observer: Update.");
        }
    }

    internal class Message { }
}
