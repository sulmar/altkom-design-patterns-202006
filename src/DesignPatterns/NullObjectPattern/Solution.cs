using System;
using System.Collections.Generic;
using System.Text;

namespace NullObjectPattern.Solution
{

    public class NullObjectPatternTest
    {
        public static void Test()
        {
            IProductRepository productRepository = new FakeProductRepository();

            ProductBase product = productRepository.Get(1);

            product.RateIt(3);
        }
    }


    // Abstract Object
    public abstract class ProductBase
    {
        public abstract void RateIt(int rate);
    }

    // Real Object
    public class Product : ProductBase
    {
        private int rate;

        public override void RateIt(int rate)
        {
            // aktualizacja oceny
            this.rate = rate;
        }
    }

    //public class NullProduct : ProductBase
    //{
    //    public override void RateIt(int rate)
    //    {
    //        // nic nie rób
    //    }
    //}

    // Rozwiązanie z użyciem NullObject i wzorca Singleton 
    public class NullProduct : ProductBase
    {
        public static readonly ProductBase Instance = new NullProduct();

        private NullProduct()
        {
        }

        public override void RateIt(int rate)
        {
            // nic nie rób
        }
    }

    public interface IProductRepository
    {
        ProductBase Get(int id);
    }

    public class FakeProductRepository : IProductRepository
    {
        public ProductBase Get(int id)
        {
            // return new NullProduct();

            return NullProduct.Instance;
        }
    }
}
