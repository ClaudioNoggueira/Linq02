using Linq02.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Linq02 {
    class Program {
        static void Main(string[] args) {
            
            //in.txt
            Console.Write("Enter the full file path: ");
            string path = Console.ReadLine();

            List<Product> products = new List<Product>();
            try {
                using(StreamReader sr = File.OpenText(path)) {
                    while (!sr.EndOfStream) {
                        string[] fields = sr.ReadLine().Split(',');
                        string name = fields[0];
                        double price = double.Parse(fields[1], CultureInfo.InvariantCulture);
                        products.Add(new Product(name, price));
                    }
                }

                var avg = products.Select(p => p.Price).DefaultIfEmpty(0.0).Average();
                Console.WriteLine("Average price: R$ " +avg.ToString("F2"));

                var names =
                    from p in products
                    where p.Price < avg
                    orderby p.Name descending
                    select p.Name;
                foreach (string name in names) {
                    Console.WriteLine(name);
                }
            }catch(IOException e) {
                Console.WriteLine("An error occured!");
                Console.WriteLine(e.Message);
            }
        }
    }
}
