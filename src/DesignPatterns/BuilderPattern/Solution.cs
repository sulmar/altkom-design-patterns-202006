using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace BuilderPattern
{
    // Abstract Builder
    public interface ISalesReportBuilder
    {
        SalesReportBase Build();
        void AddHeader();
        void AddGenderTotals();
        void AddProductTotals();
    }

    // Concrete Builder
    public class SalesReportBuilder : ISalesReportBuilder
    {
        private IEnumerable<Order> orders;

        private bool hasHeader;
        private bool hasGenderTotals;
        private bool hasProductTotals;

        public SalesReportBuilder(IEnumerable<Order> orders)
        {
            this.orders = orders;            
        }

        public void AddHeader()
        {
            hasHeader = true;
        }

        public void AddGenderTotals()
        {
            hasGenderTotals = true;
        }

        public void AddProductTotals()
        {
            hasProductTotals = true;
        }


        // Create product
        public SalesReportBase Build()
        {
            SalesReport salesReport = new SalesReport();

            if (hasHeader)
            {
                salesReport.Title = "Raport sprzedaży";
                salesReport.CreateDate = DateTime.Now;
                salesReport.TotalSalesAmount = orders.Sum(s => s.Amount);
            }

            if (hasGenderTotals)
            {
                salesReport.GenderDetails = orders
              .GroupBy(o => o.Customer.Gender)
              .Select(g => new GenderReportDetail(
                          g.Key,
                          g.Sum(x => x.Details.Sum(d => d.Quantity)),
                          g.Sum(x => x.Details.Sum(d => d.LineTotal))));
            }

            if (hasProductTotals)
            {
                salesReport.ProductDetails = orders
              .SelectMany(o => o.Details)
              .GroupBy(o => o.Product)
              .Select(g => new ProductReportDetail(g.Key, g.Sum(p => p.Quantity), g.Sum(p => p.LineTotal)));
            }

            return salesReport;
        }
    }


    // Abstract Product
    public abstract class SalesReportBase
    {
        public string Title { get; set; }
        public DateTime CreateDate { get; set; }
        public decimal TotalSalesAmount { get; set; }

        public override string ToString()
        {
            string output = string.Empty;

            output += "------------------------------\n";

            output += $"{Title} {CreateDate}\n";

            return output;
        }
    }

    
    //FluentPhone
    //    .On
    //    .From("555000111")
    //    .To("555666333")
    //    .To("555666331")
    //    .To("555666332")
    //    .WithSubject("DP")
    //.Call();

    public class FluentPhoneTest
    {
        public static void CallTest()
        {
            FluentPhone
                .On
                .From("555000111")
                .To("555666333")
                .To("555666331")
                .To("555666332")
                //.WithSubject("DP")
                .Call();
        }
    }

    public interface IFrom
    {
        ITo From(string number);
    }

    public interface ITo
    {
        ISubject To(string number);
    }

    public interface ISubject : ITo, ICall
    {
        ICall WithSubject(string subject);
    }

    public interface ICall
    {
        void Call();
    }

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
