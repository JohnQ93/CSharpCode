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
            //分别获取产品和供应商的列表
            List<Product> products = Product.GetSampleProducts();
            List<Supplier> suppliers = Supplier.GetSampleSuppliers();

            //对产品进行按名称排序
            products.Sort((x, y) => x.Name.CompareTo(y.Name));  //C#3 Lambda表达式
            products.Sort(delegate (Product x, Product y) { return x.Name.CompareTo(y.Name); });  //C#2 匿名函数
            foreach (var product in products.OrderBy(p => p.Name)) //LINQ
            {
                Console.WriteLine(product);
            }

            //按价格进行查询
            //多行分步骤
            Predicate<Product> predicate = p => p.Price > 10m;
            List<Product> matches = products.FindAll(predicate);

            Action<Product> print = p => Console.WriteLine(p);
            matches.ForEach(print);
            //单行直接查询
            products.FindAll(delegate (Product p) { return p.Price > 10m; }).ForEach(Console.WriteLine);
            //LINQ
            foreach (Product product in products.Where(p => p.Price > 10m))
            {
                Console.WriteLine(product);
            }

            //对未知参数的处理“？”,可以将值类型赋予Nullable<T>的可空类型
            Product unReleasedProduct = new Product("Demaxiya");
            products.Add(unReleasedProduct);
            products.Where(p => p.Price > 10m).ToList().ForEach(Console.WriteLine);
            products.FindAll(p => p.Price == null).ForEach(Console.WriteLine);

            //LINQ查询表达式
            var filters = from p in products
                          join s in suppliers
                           on p.ID equals s.ID
                          where p.Price > 10m
                          orderby p.Name, s.Name
                          select new { ProductName = p.Name, SupplierName = s.Name };
            foreach(var item in filters)
            {
                Console.WriteLine(item.ProductName + ":" + item.SupplierName);
            }

        }
    }

    class Product
    {
        public string Name { get; private set; }
        public decimal? Price { get; private set; }
        public int ID { get; private set; }

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
                new Product{Name = "West Side Story", Price = 9.99m, ID = 2},
                new Product{Name = "Assassins", Price = 14.99m, ID = 1},
                new Product{Name = "Frogs", Price = 13.99m,ID = 1},
                new Product{Name = "Sweeney", Price = 10.99m, ID = 3},
                new Product{Name = "CandySuager", ID = 3}
            };
        }

        public override string ToString()
        {
            return string.Format("{0}: {1}", Name, Price);
        }
    }

    class Supplier
    {
        public string Name { get; private set; }
        public int ID { get; private set; }

        public Supplier() { }

        public static List<Supplier> GetSampleSuppliers()
        {
            return new List<Supplier>
            {
                new Supplier{Name = "Seven-Eleven",ID = 1},
                new Supplier{Name = "FamilyMart",ID = 2},
                new Supplier{Name = "C-Store",ID = 3}
            };
        }
    }
}
