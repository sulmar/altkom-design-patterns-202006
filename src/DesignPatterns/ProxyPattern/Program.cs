using System;

namespace ProxyPattern
{
    public class CustomersViewModel
    {

        public virtual void Send()
        {

        }
    }

    //public class ProxyCustomersViewModel : CustomersViewModel
    //{
    //    private ICommand SendCommand;

    //    public override void Send()
    //    {
    //        base.Send();
    //    }
    //}

    public class Device
    {
        private bool enabled;

        public virtual void SwitchOn()
        {
            enabled = true;
        }

        public virtual void SwitchOff()
        {
            enabled = false;
        }
    }

    public class Customer
    {
        public int Id { get; set; }
        public virtual string FirstName { get; set; }
    }

    public class ProxyCustomer : Customer
    {
        private bool hasChanged;

        public override string FirstName
        {
            get {
                return base.FirstName;
            }
            set
            {
                base.FirstName = value;

                hasChanged = true;
            }
        }
    }


    public class ProxyDevice : Device
    {
        public override void SwitchOn()
        {
            Console.WriteLine("Before Switch On");
            base.SwitchOn();
            Console.WriteLine("After Switch On");
        }
    }

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
