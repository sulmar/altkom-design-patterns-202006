using System;

namespace ProxyPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Proxy Pattern!");
            SaveProductTest();

        }

        private static void SaveProductTest()
        {
            ProductsDbContext context = new ProductsDbContext();

            Product product = new Product(1, "Design Patterns w C#", 150m);

            context.Add(product);

            product.UnitPrice = 99m;

            context.MarkAsChanged();

            context.SaveChanges();
        }
    }

    #region Models
    public class Product
    {
        public Product(int id, string name, decimal unitPrice)
        {
            Id = id;
            Name = name;
            UnitPrice = unitPrice;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal UnitPrice { get; set; }
    }

    public class ProductsDbContext
    {
        private Product product;
        private bool changed;

        public void Add(Product product)
        {
            this.product = product;
        }

        public Product Get()
        {
            return product;
        }

        public void SaveChanges()
        {
            if (changed)
            {
                Console.WriteLine($"UPDATE dbo.Products SET UnitPrice = {product.UnitPrice} WHERE ProductId = {product.Id}" );
            }
        }

        public void MarkAsChanged()
        {
            changed = true;
        }
    }

    #endregion
}
