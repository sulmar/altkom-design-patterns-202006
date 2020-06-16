using System;

namespace StrategyPattern
{
    public interface IDiscountStrategy
    {
        bool CanDiscount(Order order);
        decimal Discount(Order order);        
    }


    public class FixedGenderDiscountStrategy : IDiscountStrategy
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

    public class PercentageGenderDiscountStrategy : IDiscountStrategy
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

    public class OrderDiscountCalculator
    {
        private readonly IDiscountStrategy discountStrategy;

        public OrderDiscountCalculator(IDiscountStrategy discountStrategy)
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
