using System;
using System.Collections.Generic;
using System.Reactive.Subjects;
using System.Text;
using static ObserverPattern.Program;
using System.Reactive.Linq;

namespace ObserverPattern
{
    // Reactive Extensions (Reaktywne Programowanie)

    public class ObserverTest
    {
        public static void Test()
        {
            ReplaySubject<Observation> subject = new ReplaySubject<Observation>();

            IObservationService observationService = new FakeObservationService();

            var lastSubject = subject       
                .Where(o=>o.Country == "Poland")
                .Take(2);

            foreach (var observation in observationService.Get())
            {
                subject.OnNext(observation);
            }

            lastSubject.Subscribe(o => Console.WriteLine(o));


        }

        public static void FundamentalTest()
        {
            ObservationSource source = new ObservationSource();

            ConsoleObserver observer = new ConsoleObserver("Marcin");

            source.Subscribe(observer);

            ConsoleObserver observer2 = new ConsoleObserver("Kasia");

            source.Subscribe(observer2);
        }

        public static void IDisposableTest()
        {

            try
            {
                using (Device device = new Device())
                {
                    device.Init();

                    device.DoWork();

                    throw new Exception();

                    device.Dispose();

                    device.DoWork();

                    device.Dispose();
                }                
            }
            catch (Exception e)
            {

            }


        }
    }

    public class Device : IDisposable
    {
        public void Init()
        {
            System.IO.File.CreateText("plik.tmp");
        }

        public void DoWork()
        {

        }

        private void Release()
        {
            System.IO.File.Delete("plik.tmp");
        }

        public void Dispose()
        {
            Release();
        }
    }

    public class ObservationSource : IObservable<Observation>
    {
        IObservationService observationService = new FakeObservationService();

        public IDisposable Subscribe(IObserver<Observation> observer)
        {
            foreach (var observation in observationService.Get())
            {
                if (observation.Country == "Poland")
                {
                    observer.OnNext(observation);
                }
            }

            observer.OnCompleted();

            return null;            
        }
    }

    public class ConsoleObserver : IObserver<Observation>
    {
        public ConsoleObserver(string name)
        {
            Name = name;
        }

        public string Name { get; set; }

        public void OnCompleted()
        {
            Console.WriteLine($"[{Name}] koniec danych");
        }

        public void OnError(Exception error)
        {
            Console.WriteLine($"[{Name}] ERROR {error}");
        }

        public void OnNext(Observation value)
        {
            Console.WriteLine($"[{Name}] {value}");
        }
    }
}
