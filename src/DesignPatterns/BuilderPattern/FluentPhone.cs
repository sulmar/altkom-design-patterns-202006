using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BuilderPattern
{
    // https://github.com/VerbalExpressions/CSharpVerbalExpressions

    // https://github.com/FluentDateTime/FluentDateTime

    public class FluentPhone : IFrom, ITo, ISubject, ICall
    {
        private string from;
        private ICollection<string> tos;
        private string subject;

        protected FluentPhone()
        {
            tos = new Collection<string>();
        }

        public static IFrom On
        {
            get
            {
                return new FluentPhone();
            }
        }

        public ITo From(string number)
        {
            this.from = number;

            return this;
        }

        public ISubject To(string number)
        {
            this.tos.Add(number);

            return this;
        }

        public ICall WithSubject(string subject)
        {
            this.subject = subject;

            return this;
        }


        // Build
        public void Call()
        {
            foreach (var to in tos)
            {
                if (!string.IsNullOrEmpty(subject))
                    Call(from, to, subject);
                else
                    Call(from, to);
            }
        }

        private void Call(string from, string to, string subject)
        {
            Console.WriteLine($"Calling from {from} to {to} with subject {subject}");
        }

        private void Call(string from, string to)
        {
            Console.WriteLine($"Calling from {from} to {to}");
        }
    }

}
