using System;

namespace StrategyPattern
{
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
