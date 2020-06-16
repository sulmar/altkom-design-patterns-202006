using System;

namespace TemplateMethodPattern
{
    public abstract class OrderCalculatorBase
    {
        public abstract bool CanDiscount(Order order);  // Predykat

        public abstract decimal Discount(Order order); // Discount

        public decimal CalculateDiscount(Order order)
        {
            if (CanDiscount(order)) // Predykat
            {
                return Discount(order);  // Discount
            }
            else
                return 0;
        }
    }

    public class DelegateOrderCalculator
    {
        private readonly Predicate<Order> predicate;
        private readonly Func<Order, decimal> discount;

        public DelegateOrderCalculator(Predicate<Order> predicate, Func<Order, decimal> discount)
        {
            this.predicate = predicate;
            this.discount = discount;
        }

        public decimal CalculateDiscount(Order order)
        {
            if (predicate(order)) // Predykat
            {
                return discount(order);  // Discount
            }
            else
                return 0;
        }
    }


    public class BetterGenderOrderCalculator : OrderCalculatorBase
    {
        private readonly Gender gender;
        private readonly decimal percentage;

        public BetterGenderOrderCalculator(Gender gender, decimal percentage)
        {
            this.gender = gender;
            this.percentage = percentage;
        }

        public override bool CanDiscount(Order order) => order.Customer.Gender == gender;

        public override decimal Discount(Order order) => order.Amount * percentage;
    }

    public class BetterHappyHoursOrderCalculator : OrderCalculatorBase
    {
        private readonly TimeSpan from;
        private readonly TimeSpan to;
        private readonly decimal percentage;

        public BetterHappyHoursOrderCalculator(TimeSpan from, TimeSpan to, decimal percentage)
        {
            this.from = from;
            this.to = to;
            this.percentage = percentage;
        }

        public override bool CanDiscount(Order order)
        {
            return order.OrderDate.TimeOfDay >= from       
                && order.OrderDate.TimeOfDay <= to;
        }

        public override decimal Discount(Order order)
        {
            return order.Amount * percentage;
        }
    }


    // Gender - 20% upustu dla kobiet
    public class GenderOrderCalculator
    {
        public decimal CalculateDiscount(Order order)
        {
            if (order.Customer.Gender == Gender.Female) // Predykat
            {
                return order.Amount * 0.2m;  // Discount
            }
            else
                return 0;
        }
    }

    // Happy Hours - 10% upustu w godzinach od 9:30 do 16:00
    public class HappyHoursOrderCalculator
    {
        private readonly TimeSpan from;
        private readonly TimeSpan to;
        private readonly decimal percentage;

        public HappyHoursOrderCalculator(TimeSpan from, TimeSpan to, decimal percentage)
        {
            this.from = from;
            this.to = to;
            this.percentage = percentage;
        }

        public decimal CalculateDiscount(Order order)
        {            
            if (order.OrderDate.TimeOfDay >= from       // Predykat
                && order.OrderDate.TimeOfDay <= to) 
            {
                return order.Amount * percentage;       // Upust
            }
            else
                return 0;
        }
    }



}
