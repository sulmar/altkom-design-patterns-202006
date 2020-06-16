using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace StrategyPattern
{
   

    public interface IComplexDiscountStrategy
    {
        bool CanDiscount(Order order);
        decimal Discount(Order order);        
    }

    public interface ICompositeDiscountStrategy : ICanDiscountStrategy, IDiscountStrategy
    {

    }

    public interface ICanDiscountStrategy
    {
        bool CanDiscount(Order order);
    }

    public interface IDiscountStrategy
    {
        decimal Discount(Order order);
    }

    public class DiscountCalculatorTest
    {
        public static void Test()
        {
            Customer customer = new Customer("Anna", "Kowalska");
            Order order = CreateOrder(customer);

            ICanDiscountStrategy canDiscountStrategy = new HappyHoursCanDiscountStrategy(TimeSpan.Parse("9:30"), TimeSpan.Parse("16"));
            IDiscountStrategy discountStrategy = new PercentageDiscountStrategy(0.1m);

            // PMC> Install-Package Newtonsoft.Json
            string jsonDiscountStrategy = JsonConvert.SerializeObject(discountStrategy);
            IDiscountStrategy discountStrategy2 = JsonConvert.DeserializeObject<PercentageDiscountStrategy>(jsonDiscountStrategy);

            OrderDiscountCalculator discountCalculator = new OrderDiscountCalculator(canDiscountStrategy, discountStrategy);

            decimal discount = discountCalculator.CalculateDiscount(order);
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



    public class OrderDiscountCalculator
    {
        private readonly ICanDiscountStrategy canDiscountStrategy;
        private readonly IDiscountStrategy discountStrategy;

        public OrderDiscountCalculator(ICanDiscountStrategy canDiscountStrategy, IDiscountStrategy discountStrategy)
        {
            this.canDiscountStrategy = canDiscountStrategy;
            this.discountStrategy = discountStrategy;
        }

        public decimal CalculateDiscount(Order order)
        {
            if (canDiscountStrategy.CanDiscount(order)) // Predykat
            {
                return discountStrategy.Discount(order);  // Discount
            }
            else
                return 0;
        }

    }

    public class GenderCanDiscountStrategy : ICanDiscountStrategy
    {
        private readonly Gender gender;

        public GenderCanDiscountStrategy(Gender gender)
        {
            this.gender = gender;
        }

        public bool CanDiscount(Order order) => order.Customer.Gender == gender;
    }

    public class HappyHoursCanDiscountStrategy : ICanDiscountStrategy
    {
        private readonly TimeSpan from;
        private readonly TimeSpan to;

        public HappyHoursCanDiscountStrategy(TimeSpan from, TimeSpan to)
        {
            this.from = from;
            this.to = to;
        }

        public bool CanDiscount(Order order) 
            => order.OrderDate.TimeOfDay >= from && order.OrderDate.TimeOfDay <= to;
    }

    public class PercentageDiscountStrategy : IDiscountStrategy
    {
        private readonly decimal percentage;

        public decimal Percentage => percentage;

        public PercentageDiscountStrategy(decimal percentage)
        {
            this.percentage = percentage;
        }

        public decimal Discount(Order order)
        {
            return order.Amount * percentage;
        }
    }

    public class FixedDiscountStrategy : IDiscountStrategy
    {
        private readonly decimal amount;

        public FixedDiscountStrategy(decimal amount)
        {
            this.amount = amount;
        }

        public decimal Discount(Order order) => amount;
    }

    public class FixedGenderDiscountStrategy : IComplexDiscountStrategy
    {
        private readonly Gender gender;
        private readonly decimal amount;

        public FixedGenderDiscountStrategy(Gender gender, decimal amount)
        {
            this.gender = gender;
            this.amount = amount;
        }

        public bool CanDiscount(Order order)
        {
            return order.Customer.Gender == gender;
        }

        public decimal Discount(Order order)
        {
            return amount;
        }
    }

    public class PercentageGenderDiscountStrategy : IComplexDiscountStrategy
    {
        private readonly Gender gender;
        private readonly decimal percentage;

        public PercentageGenderDiscountStrategy(Gender gender, decimal percentage)
        {
            this.gender = gender;
            this.percentage = percentage;
        }

        public bool CanDiscount(Order order)
        {
            return order.Customer.Gender == gender;
        }

        public decimal Discount(Order order)
        {
            return order.Amount * percentage;
        }
    }

    public class HappyHoursDiscountStrategy : IDiscountStrategy
    {
        private readonly TimeSpan from;
        private readonly TimeSpan to;
        private readonly decimal percentage;

        public HappyHoursDiscountStrategy(TimeSpan from, TimeSpan to, decimal percentage)
        {
            this.from = from;
            this.to = to;
            this.percentage = percentage;
        }

        public bool CanDiscount(Order order)
        {
            return order.OrderDate.TimeOfDay >= from       // Predykat
                && order.OrderDate.TimeOfDay <= to;
        }

        public decimal Discount(Order order)
        {
            return order.Amount * percentage;
        }
    }

    public class ComplexOrderDiscountCalculator
    {
        private readonly IComplexDiscountStrategy discountStrategy;

        public ComplexOrderDiscountCalculator(IComplexDiscountStrategy discountStrategy)
        {
            this.discountStrategy = discountStrategy;
        }

        public decimal CalculateDiscount(Order order)
        {
            if (discountStrategy.CanDiscount(order)) // Predykat
            {
                return discountStrategy.Discount(order);  // Discount
            }
            else
                return 0;
        }

    }
}
