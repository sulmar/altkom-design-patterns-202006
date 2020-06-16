using System;
using System.Collections.Generic;

namespace ISP
{
    public interface IBankomat
    {
        void Wplata(decimal amount);
        void Wyplata(decimal amount);
        decimal PobierzSaldo();
    }

    public interface IWplatomat
    {
        void Wplata(decimal amount);
    }

    public interface IWyplatomat
    {
        void Wyplata(decimal amount);
    }

    public interface ISaldomat
    {
        decimal PobierzSaldo();
    }

    public class BetterMyBankomat : IWyplatomat, ISaldomat
    {
        private decimal saldo;

        public decimal PobierzSaldo()
        {
            return saldo;
        }

        public void Wyplata(decimal amount)
        {
            saldo -= amount;
        }
    }

    public class MyBankomat : IBankomat
    {
        private decimal saldo;

        public decimal PobierzSaldo()
        {
            return saldo;
        }

        public void Wplata(decimal amount)
        {
            throw new NotSupportedException();
        }

        public void Wyplata(decimal amount)
        {
            saldo -= amount;
        }
    }

    public interface IServices
    {
        IEnumerable<Product> GetProducts();
        void AddProduct(Product product);

        IEnumerable<Order> GetOrders();
        void AddOrder(Order order);

        IEnumerable<Customer> GetCustomers();
        void AddCustomer(Customer customer);

    }

    // interfejs generyczny
    public interface IEntityService<TEntity, TKey>
    {
        IEnumerable<TEntity> Get();
        TEntity Get(TKey id);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Remove(TKey id);
    }

    public interface IProductService : IEntityService<Product, int>
    {
        IEnumerable<Product> GetByColor(string color);
    }

    public interface IOrderService : IEntityService<Order, long>
    {
    }

    public interface ICustomerService : IEntityService<Customer, int>
    {
    }

    public class Product
    {

    }

    public class Order
    {

    }

    public class Customer
    {

    }

    public class DbCustomerService : ICustomerService
    {
        public void Add(Customer entity)
        {
            throw new NotImplementedException();
        }

        public void AddCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> Get()
        {
            throw new NotImplementedException();
        }

        public Customer Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> GetCustomers()
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Customer entity)
        {
            throw new NotImplementedException();
        }
    }

    public class FileProductService : IProductService
    {
        public void Add(Product entity)
        {
            throw new NotImplementedException();
        }

        public void AddProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> Get()
        {
            throw new NotImplementedException();
        }

        public Product Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetByColor(string color)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetProducts()
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Product entity)
        {
            throw new NotImplementedException();
        }
    }

    public class DbServices : IServices
    {
        public void AddCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }

        public void AddOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public void AddProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> GetCustomers()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Order> GetOrders()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetProducts()
        {
            throw new NotImplementedException();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            IWyplatomat bankomat = new BetterMyBankomat();
            bankomat.Wyplata(100);

            if (bankomat is IWplatomat)
            {
                
            }
            
        }
    }
}
