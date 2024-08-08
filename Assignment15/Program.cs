using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Threading.Channels;
using static Assignment15.ListGenerators;

namespace Assignment15
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region LINQ - Element Operators
            //1
            Console.WriteLine(ProductList.First());
            Console.WriteLine("===============================");

            //2
            Console.WriteLine(ProductList.FirstOrDefault(p => p.UnitPrice > 1000));
            Console.WriteLine("===============================");

            //3
            int[] Arr = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            var secondNumberGreaterThanFive = Arr.Where(n => n > 5).Skip(1).FirstOrDefault();
            Console.WriteLine(secondNumberGreaterThanFive);
            Console.WriteLine("===============================");
            #endregion

            #region LINQ - Aggregate Operators
            //1
            var oddNumsCount = Arr.Count(num => num % 2 == 0);
            Console.WriteLine(oddNumsCount);
            Console.WriteLine("===============================");

            //2
            var customerOrderCounts = CustomerList.Select(c => new { CustomerName = c.CustomerName, OrderCount = c.Orders.Count() });
            foreach (var customerOrder in customerOrderCounts)
            {
                Console.WriteLine($"{customerOrder.CustomerName} :: {customerOrder.OrderCount}");
            }
            Console.WriteLine("===============================");

            //3
            var categoryProducCounts = from p in ProductList
                                       group p by p.Category
                                       into pc
                                       select new { Category = pc.Key, Count = pc.Count() };
            Console.WriteLine("===============================");

            foreach (var category in categoryProducCounts)
            {
                Console.WriteLine($"{category.Category} :: {category.Count}");
            }
            Console.WriteLine("===============================");

            //4
            int[] Arr2 = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            var summation = Arr2.Aggregate((num1, num2) => num1 + num2);
            Console.WriteLine(summation);
            Console.WriteLine("===============================");

            //5
            const string fileText = "dictionary_english.txt";
            string[] text;
            text = File.ReadAllLines(fileText);

            var letterCount = text.Sum(s => s.Count());
            Console.WriteLine(letterCount);
            Console.WriteLine("===============================");

            //6
            var shortestWord = text.MinBy(s => s.Count());
            Console.WriteLine(shortestWord);
            Console.WriteLine("===============================");

            //7
            var longestWord = text.MaxBy(s => s.Count());
            Console.WriteLine(longestWord);
            Console.WriteLine("===============================");

            //8
            var averageWordLen = text.Average(s => s.Count());
            Console.WriteLine(averageWordLen);
            Console.WriteLine("===============================");

            //9
            var unitsInStockTotal = ProductList.GroupBy(p => p.Category).Select(c => new
            {
                Category = c.Key,
                TotalUnitsInStock = c.Sum(p => p.UnitsInStock)
            });
            foreach (var unit in unitsInStockTotal) Console.WriteLine(unit);
            Console.WriteLine("===============================");

            //10
            var cheapestInCategory = ProductList.GroupBy(p => p.Category).Select(c => new
            {
                Category = c.Key,
                Price = c.Min(p => p.UnitPrice)
            });
            foreach (var unit in cheapestInCategory) Console.WriteLine(unit);
            Console.WriteLine("===============================");

            //11
            var cheapestProds = from p in ProductList
                                group p by p.Category
                                       into pc
                                let minPriceProduct = pc.MinBy(p => p.UnitPrice)
                                select minPriceProduct;
            foreach (var item in cheapestProds)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("===============================");

            //12
            var expensiveInCategory = ProductList.GroupBy(p => p.Category).Select(c => new
            {
                Category = c.Key,
                Price = c.Max(p => p.UnitPrice)
            });
            foreach (var unit in expensiveInCategory) Console.WriteLine(unit);
            Console.WriteLine("===============================");

            //13
            var expensiveProds = from p in ProductList
                                 group p by p.Category
                                       into pc
                                 let minPriceProduct = pc.MaxBy(p => p.UnitPrice)
                                 select minPriceProduct;
            foreach (var item in expensiveProds)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("===============================");

            //14
            var avgInCategory = ProductList.GroupBy(p => p.Category).Select(c => new
            {
                Category = c.Key,
                Price = c.Average(p => p.UnitPrice)
            });
            foreach (var item in avgInCategory)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("===============================");
            #endregion

            #region LINQ - Set Operators
            //1
            var uniqueCategory = ProductList.Distinct();
            PrintEnumerable(uniqueCategory);
            #endregion

            #region LINQ - Partitioning Operators
            //1
            var first3Orders = CustomerList.Where(p => p.City == "Washington").Take(3);
            PrintEnumerable(first3Orders);

            //2
            var skip2Orders = CustomerList.Where(p => p.City == "Washington").Skip(2);
            PrintEnumerable(skip2Orders);

            //3
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            var takeUntil = numbers.TakeWhile((n, i) => n < i);
            PrintEnumerable(takeUntil);

            //4
            var skipUntil = numbers.SkipWhile(n => n % 3 != 0);
            PrintEnumerable(skipUntil);

            //5
            var skipUntil2 = numbers.SkipWhile((n, i) => n > i);
            PrintEnumerable(skipUntil2);
            #endregion

            #region LINQ - Quantifiers
            //1
            var quant1 = text.Any(s => s.Contains("ei"));
            Console.WriteLine(quant1);

            //2
            var quant2 = ProductList.GroupBy(p => p.Category).Where(c => c.Any(p => p.UnitsInStock == 0));
            PrintEnumerable(quant2);

            //2
            var quant3 = ProductList.GroupBy(p => p.Category).Where(c => c.All(p => p.UnitsInStock > 0));
            PrintEnumerable(quant3);
            #endregion

            #region LINQ – Grouping Operators
            //1
            List<int> numbers2 = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
            var group1 = numbers2.GroupBy(n => n % 5);

            int i = 0;
            foreach (var group in group1)
            {
                Console.WriteLine($"Numbers with a remainder of {i} when divided by 5");
                foreach (var item in group)
                {
                    Console.WriteLine(item);
                }
                i++;
            }

            //2
            var group2 = text.GroupBy(s => s[0]);
            foreach (var group in group2)
            {
                foreach (var item in group) Console.WriteLine(item);
                Console.WriteLine(".........");
            }
            Console.WriteLine("=====================================");

            //3
            String[] Arr3 = { "from", "salt", "earn", "last", "near", "form" };
            var group3 = Arr3.GroupBy(s => s, new CustomComparer());
            foreach (var group in group3)
            {
                foreach (var item in group)
                {
                    Console.WriteLine(item);
                }
                Console.WriteLine(".........");
            }
            #endregion
        }

        static void PrintEnumerable(IEnumerable enumerable)
        {
            foreach (var item in enumerable)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("===============================");
        }
    }
}
