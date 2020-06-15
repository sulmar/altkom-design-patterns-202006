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

            Visit visit = null;

            switch (input)
            {
                case "N": visit = new NFZVisit(); break;
                case "P": visit = new PrivateVisit(); break;
                case "F": visit = new PackageVisit(); break;
            }

            decimal totalAmount = visit.CalculateAmount();
            Console.WriteLine($"Total amount {totalAmount:C2}");
        }
    }

    #region Models

    public class NFZVisit : Visit
    {
        public override decimal CalculateAmount()
        {
            return 0;
        }
    }

    public class PrivateVisit : Visit
    {

    }

    public class PackageVisit : Visit
    {
        public override decimal CalculateAmount()
        {
            return base.CalculateAmount() * 0.9m;
        }
    }

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
}
