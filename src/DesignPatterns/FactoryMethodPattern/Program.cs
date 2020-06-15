using System;

namespace FactoryMethodPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Factory Method Pattern!");

            VisitCalculateAmountTest();

        }

        private static void VisitCalculateAmountTest()
        {
            Console.WriteLine("Podaj rodzaj wizyty: (N)FZ (P)rywatna (F)irma");

            string input = Console.ReadLine();

            IVisitFactory visitFactory = new WorkdayVisitFactory();

            Visit visit = visitFactory.Create(input);

            decimal totalAmount = visit.CalculateAmount();
            Console.WriteLine($"Total amount {totalAmount:C2}");
        }
    }

    #region Models

    // Concrete Product
    public class NFZVisit : Visit
    {
        public override decimal CalculateAmount()
        {
            return 0;
        }
    }

    // Concrete Product
    public class PrivateVisit : Visit
    {

    }

    // Concrete Product
    public class PackageVisit : Visit
    {
        public override decimal CalculateAmount()
        {
            return base.CalculateAmount() * 0.9m;
        }
    }


    // Abstract Product
    public abstract class Visit
    {
        public DateTime VisitDate { get; set; }
        public TimeSpan Duration { get; set; }
        public decimal Amount { get; set; }

        public Visit()
        {
            Amount = 100;
            Duration = TimeSpan.FromMinutes(15);
        }

        public virtual decimal CalculateAmount()
        {
            return (decimal)Duration.TotalHours * Amount;
        }
    }

    #endregion

    // Abstract Creator
    public interface IVisitFactory
    {
        Visit Create(string input);
    }

    public class CovidVisitFactory : IVisitFactory
    {
        public Visit Create(string input)
        {
            return new PrivateVisit();
        }
    }

    // Concrete Creator
    public class WorkdayVisitFactory : IVisitFactory
    {
        public Visit Create(string input)
        {
            switch (input)
            {
                case "N": return new NFZVisit(); 
                case "P": return new PrivateVisit(); 
                case "F": return new PackageVisit();

                default:
                    throw new NotSupportedException(input);
            }
        }
    }
}
