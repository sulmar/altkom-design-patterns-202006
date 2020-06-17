using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace DecoratorPattern.Solution
{
    public class OrderTest
    {
        public static void Test()
        {
            Customer customer = new Customer("Anna", "Kowalska");

            Order order = CreateOrder(customer);

            var discountedOrder = new GenderPercentageDiscountDecorator
                                    (new HappyHoursPercentageDiscountDecorator(order, TimeSpan.Parse("09:30"), TimeSpan.Parse("16:00"), 0.1m),
                                        Gender.Female, 0.2m);

            Console.WriteLine($"Discount {discountedOrder.Discount:c2}");

        }

        private static void StreamTest()
        {
            string filename = "lorem-ipsum.txt";

            Stream stream = new GZipStream(new FileStream(filename, FileMode.Open), CompressionLevel.Optimal);

            using (StreamReader reader = new StreamReader(stream))
            {
                string content = reader.ReadLine();
            }
        }

        private static Order CreateOrder(Customer customer)
        {
            Product product1 = new Product(1, "Książka C#", unitPrice: 100m);
            Product product2 = new Product(2, "Książka Praktyczne Wzorce projektowe w C#", unitPrice: 150m);
            Product product3 = new Product(3, "Zakładka do książki", unitPrice: 10m);

            Order order = new Order(DateTime.Parse("2020-06-12 14:59"), customer);
            order.AddDetail(product1);
            order.AddDetail(product2);
            order.AddDetail(product3, 5);

            return order;
        }
    }

    public interface IOrderDiscountCalculator
    {
        decimal CalculateDiscount(Order order);
    }

    public class HappyHoursPercentageDiscountDecorator : Decorator
    {
        private readonly TimeSpan from;
        private readonly TimeSpan to;
        private readonly decimal percentage;

        public HappyHoursPercentageDiscountDecorator(Order order, TimeSpan from, TimeSpan to, decimal percentage)
            : base(order)
        {
            this.from = from;
            this.to = to;
            this.percentage = percentage;

            if (order.OrderDate.TimeOfDay >= from && order.OrderDate.TimeOfDay<=to)
            {
                order.Discount += order.Amount * percentage;
            }
        }
    }

    public class GenderPercentageDiscountDecorator : Decorator
    {
        private readonly Gender gender;
        private readonly decimal percentage;

        public GenderPercentageDiscountDecorator(Order order, Gender gender, decimal percentage) : base(order)
        {
            this.gender = gender;

            if(order.Customer.Gender == gender)
            {
                order.Discount += order.Amount * percentage;
            }
        }
    }

    // Decorator
    public abstract class Decorator : Order
    {
        public Order order;

        public Decorator(Order order)
        {
            this.order = order;
        }


    }





}
