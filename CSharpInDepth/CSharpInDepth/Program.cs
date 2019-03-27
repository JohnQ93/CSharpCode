using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpInDepth
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Product> products = Product.GetSampleProducts();
            //products.Sort((x, y) => x.Name.CompareTo(y.Name));
            //products.Sort(delegate (Product x, Product y) { return x.Name.CompareTo(y.Name); });
            //foreach (var product in products.OrderBy(p => p.Name))
            //{
            //    Console.WriteLine(product);
            //}

            //Predicate<Product> predicate = p => p.Price > 10m;
            //List<Product> matches = products.FindAll(predicate);

            //Action<Product> print = p => Console.WriteLine(p);
            //matches.ForEach(print);

            //products.FindAll(delegate (Product p) { return p.Price > 10m; }).ForEach(Console.WriteLine);

            //foreach (Product product in products.Where(p => p.Price > 10m))
            //{
            //    Console.WriteLine(product);
            //}
            Product unReleasedProduct = new Product("Demaxiya");
            products.Add(unReleasedProduct);
            products.Where(p => p.Price > 10m).ToList().ForEach(Console.WriteLine);
            products.FindAll(p => p.Price == null).ForEach(Console.WriteLine);
        }
    }

    class Product
    {
        public string Name { get; private set; }
        public decimal? Price { get; private set; }

        public Product(string name, decimal? price = null)
        {
            Name = name;
            Price = price;
        }

        public Product() { }

        public static List<Product> GetSampleProducts()
        {
            return new List<Product>
            {
                new Product{Name = "West Side Story", Price = 9.99m},
                new Product{Name = "Assassins", Price = 14.99m},
                new Product{Name = "Frogs", Price = 13.99m},
                new Product{Name = "Sweeney", Price = 10.99m},
                new Product{Name = "CandySuager"}
            };
        }

        public override string ToString()
        {
            return string.Format("{0}: {1}", Name, Price);
        }
    }
}
