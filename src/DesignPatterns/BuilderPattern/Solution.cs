using System;
using System.Collections.Generic;
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

}
