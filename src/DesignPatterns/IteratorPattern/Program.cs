using System;
using System.Collections;
using System.Collections.Generic;

namespace IteratorPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Iterator Pattern!");

            IteratorTest();


        }

        private static void IteratorTest()
        {
            ConcreteAggregate<string> aggr = new ConcreteAggregate<string>();
            aggr.Add("One");
            aggr.Add("Two");
            aggr.Add("Three");
            aggr.Add("Four");
            aggr.Add("Five");

            IIterator<string> iterator = aggr.CreateIterator();

            do
            {
                Console.WriteLine(iterator.Current);
            }
            while (iterator.MoveNext());
        }
    }

    public interface IIterator<T>
    {
        void MoveFirst();
        bool MoveNext();
        T Current { get; }
    }

    public class ConcreteIterator<T> : IIterator<T>
    {
        private ConcreteAggregate<T> aggregate;
        private int _position;

        public ConcreteIterator(ConcreteAggregate<T> aggregate)
        {
            this.aggregate = aggregate;
        }

        public T Current
        {
            get
            {
                if (_position < aggregate.Count)
                    return aggregate[_position];
                else
                    return default(T);
            }
        }

        public void MoveFirst()
        {
            _position = 0;
        }

        public bool MoveNext()
        {
            _position++;

            return _position < aggregate.Count;

        }
    }

    public interface IAggregate<T>
    {
        IIterator<T> CreateIterator();
    }

    public class ConcreteAggregate<T> : IAggregate<T>
    {
        private IList<T> items = new List<T>();

        public IIterator<T> CreateIterator()
        {
            return new ConcreteIterator<T>(this);
        }

        public T this[int index]
        {
            get { return items[index]; }
        }

        public int Count => items.Count;

        public void Add(T item)
        {
            items.Add(item);
        }


    }
}