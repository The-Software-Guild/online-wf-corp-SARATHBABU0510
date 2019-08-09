using LINQ.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LINQ
{
    class Program
    {
        static void Main()
        {
            PrintAllProducts();
            PrintAllCustomers();

            PrintAllOutofStockProducts();
            PrintInStockCostmorethan3Products();
            PrintAllWACustomers();
            PrintAnonProductsNames();
            PrintAnonAllProductsIncreasedPrice();
            PrintAnonProductsNamesandCategory();
            PrintAnonProductsReorder();
            PrintAnonProductsStockValue();
            PrintEvenNumbersA();
            PrintCustomersOrderTotalLess500();
            PrintOdd3NumbersC();
            PrintAllNumbersBbutFirst3();
            PrintCompanyNameRecentOrderAllCustomersWA();
            PrintFirstNumbersCLessthan6();
            PrintNumbersCAfterFirstDivisbleby3();
            PrintAllProductsSortByNameAscending();
            PrintAllProductsSortByUnitsInStockDescending();
            PrintAllProductsSortByCategoryandUnitPrice();
            PrintNumbersBinreverse();
            PrintCategoryandProducts();
            PrintCustomersOrderYearMonth();
            PrintUniqueProductCategories();
            CheckProduct789();
            PrintCategorieswithoneoutofstockproducts();
            PrintCategorieswithnooutofstockproducts();
            PrintOddNumbersACount();
            PrintAnonTypeCustomerIDandOrdersCount();
            PrintDistinctCategoriesandProductsCount();
            PrintDistinctCategoriesandUnitsInStockCount();
            PrintDistinctCategoriesandLowestPricedProduct();
            PrintTop3DistinctCategoriesAvgUnitPriceProduct();

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        #region "Sample Code"
        /// <summary>
        /// Sample, load and print all the product objects
        /// </summary>
        static void PrintAllProducts()
        {
            List<Product> products = DataLoader.LoadProducts();
            PrintProductInformation(products);
        }

        /// <summary>
        /// This will print a nicely formatted list of products
        /// </summary>
        /// <param name="products">The collection of products to print</param>
        static void PrintProductInformation(IEnumerable<Product> products)
        {
            string line = "{0,-5} {1,-35} {2,-15} {3,6:c} {4,6}";
            Console.WriteLine(line, "ID", "Product Name", "Category", "Unit", "Stock");
            Console.WriteLine("==============================================================================");

            foreach (var product in products)
            {
                Console.WriteLine(line, product.ProductID, product.ProductName, product.Category,
                    product.UnitPrice, product.UnitsInStock);
            }

        }

        /// <summary>
        /// Sample, load and print all the customer objects and their orders
        /// </summary>
        static void PrintAllCustomers()
        {
            var customers = DataLoader.LoadCustomers();
            PrintCustomerInformation(customers);
        }

        /// <summary>
        /// This will print a nicely formated list of customers
        /// </summary>
        /// <param name="customers">The collection of customer objects to print</param>
        static void PrintCustomerInformation(IEnumerable<Customer> customers)
        {
            foreach (var customer in customers)
            {
                Console.WriteLine("==============================================================================");
                Console.WriteLine(customer.CompanyName);
                Console.WriteLine(customer.Address);
                Console.WriteLine("{0}, {1} {2} {3}", customer.City, customer.Region, customer.PostalCode, customer.Country);
                Console.WriteLine("p:{0} f:{1}", customer.Phone, customer.Fax);
                Console.WriteLine();
                Console.WriteLine("\tOrders");
                foreach (var order in customer.Orders)
                {
                    Console.WriteLine("\t{0} {1:MM-dd-yyyy} {2,10:c}", order.OrderID, order.OrderDate, order.Total);
                }
                Console.WriteLine("==============================================================================");
                Console.WriteLine();
            }
        }
        #endregion

        /// <summary>
        /// Print all products that are out of stock.
        /// </summary>
        static void PrintAllOutofStockProducts()
        {
            List<Product> products = DataLoader.LoadProducts();
            List<Product> outofstockproducts = products.Where(p => p.UnitsInStock == 0).ToList();
            PrintProductInformation(outofstockproducts);
        }

        /// <summary>
        /// Print all products that are in stock and cost more than 3.00 per unit.
        /// </summary>
        static void PrintInStockCostmorethan3Products()
        {
            List<Product> products = DataLoader.LoadProducts();
            List<Product> outofstockproducts = products.Where(p => p.UnitsInStock > 0 && p.UnitPrice > 3.00M).ToList();
            PrintProductInformation(outofstockproducts);
        }

        /// <summary>
        /// Print all customer and their order information for the Washington (WA) region.
        /// </summary>
        static void PrintAllWACustomers()
        {
            var customers = DataLoader.LoadCustomers();
            var WAcustomers = customers.Where(c => c.Region != null && c.Region.ToUpper() == "WA").ToList();
            PrintCustomerInformation(WAcustomers);
        }

        /// <summary>
        /// Create and print an anonymous type with just the ProductName
        /// </summary>
        static void PrintAnonProductsNames()
        {
            List<Product> products = DataLoader.LoadProducts();

            string line = "{0,-35}";
            Console.WriteLine(line, "Product Name");
            Console.WriteLine("===================================");

            var productnametype = products.Select(p => new { ProductName = p.ProductName });
            foreach (var product in productnametype)
            {
                Console.WriteLine(line, product.ProductName);
            }
        }

        /// <summary>
        /// Create and print an anonymous type of all product information but increase the unit price by 25%
        /// </summary>
        static void PrintAnonAllProductsIncreasedPrice()
        {
            List<Product> products = DataLoader.LoadProducts();
            List<Product> anonproducts = products.Select(p => new
            {
                ProductID = p.ProductID,
                ProductName = p.ProductName,
                Category = p.Category,
                UnitPrice = p.UnitPrice * 1.25M,
                UnitsInStock = p.UnitsInStock
            }).ToList().Select(a => new Product
            {
                ProductID = a.ProductID,
                ProductName = a.ProductName,
                Category = a.Category,
                UnitPrice = a.UnitPrice,
                UnitsInStock = a.UnitsInStock
            }).ToList();
            PrintProductInformation(anonproducts);
        }

        /// <summary>
        /// Create and print an anonymous type of only ProductName and Category with all the letters in upper case
        /// </summary>
        static void PrintAnonProductsNamesandCategory()
        {
            List<Product> products = DataLoader.LoadProducts();

            string line = "{0,-35} {1,-15}";
            Console.WriteLine(line, "Product Name", "Category");
            Console.WriteLine("==================================================");

            var productnametype = products.Select(p => new
            {
                ProductName = p.ProductName.ToUpper(),
                Category = p.Category.ToUpper()
            });
            foreach (var product in productnametype)
            {
                Console.WriteLine(line, product.ProductName, product.Category);
            }
        }

        /// <summary>
        /// Create and print an anonymous type of all Product information with an extra bool property ReOrder which should 
        /// be set to true if the Units in Stock is less than 3
        /// 
        /// Hint: use a ternary expression
        /// </summary>
        static void PrintAnonProductsReorder()
        {
            List<Product> products = DataLoader.LoadProducts();
            var anonproducts = products.Select(p => new
            {
                ProductID = p.ProductID,
                ProductName = p.ProductName,
                Category = p.Category,
                UnitPrice = p.UnitPrice,
                UnitsInStock = p.UnitsInStock,
                Reorder = (p.UnitsInStock < 3) ? true : false
            }).ToList();

            string line = "{0,-5} {1,-35} {2,-15} {3,6:c} {4,6} {5,5}";
            Console.WriteLine(line, "ID", "Product Name", "Category", "Unit", "Stock", "Reorder");
            Console.WriteLine("=====================================================================================");

            foreach (var product in anonproducts)
            {
                Console.WriteLine(line, product.ProductID, product.ProductName, product.Category,
                    product.UnitPrice, product.UnitsInStock, product.Reorder);
            }
        }

        /// <summary>
        /// Create and print an anonymous type of all Product information with an extra decimal called 
        /// StockValue which should be the product of unit price and units in stock
        /// </summary>
        static void PrintAnonProductsStockValue()
        {
            List<Product> products = DataLoader.LoadProducts();
            var anonproducts = products.Select(p => new
            {
                ProductID = p.ProductID,
                ProductName = p.ProductName,
                Category = p.Category,
                UnitPrice = p.UnitPrice,
                UnitsInStock = p.UnitsInStock,
                StockValue = Math.Round(p.UnitPrice * p.UnitsInStock, 2)
            }).ToList();

            string line = "{0,-5} {1,-35} {2,-15} {3,6:c} {4,6} {5,10}";
            Console.WriteLine(line, "ID", "Product Name", "Category", "Unit", "Stock", "StockValue");
            Console.WriteLine("===================================================================================");

            foreach (var product in anonproducts)
            {
                Console.WriteLine(line, product.ProductID, product.ProductName, product.Category,
                    product.UnitPrice, product.UnitsInStock, product.StockValue);
            }
        }

        /// <summary>
        /// Print only the even numbers in NumbersA
        /// </summary>
        static void PrintEvenNumbersA()
        {
            var evennumbers = DataLoader.NumbersA.Where(n => n % 2 == 0).ToList();

            string line = "{0,-12}";
            Console.WriteLine(line, "Even Numbers");
            Console.WriteLine("============");

            foreach (var number in evennumbers)
            {
                Console.WriteLine(line, number);
            }
        }

        /// <summary>
        /// Print only customers that have an order whos total is less than $500
        /// </summary>
        static void PrintCustomersOrderTotalLess500()
        {
            var customers = DataLoader.LoadCustomers();
            var OrderTotalLess500customers = customers.Where(c => c.Orders.Sum(order => order.Total) < 500.00M).ToList();
            PrintCustomerInformation(OrderTotalLess500customers);
        }

        /// <summary> 
        /// Print only the first 3 odd numbers from NumbersC
        /// </summary>
        static void PrintOdd3NumbersC()
        {
            var odd3nnumbers = DataLoader.NumbersC.Where(n => n % 2 != 0).ToList();

            string line = "{0,-11}";
            Console.WriteLine(line, "Odd Numbers");
            Console.WriteLine("===========");

            if (odd3nnumbers.Count >= 3)
            {
                for (int i = 0; i <= 2; i++)
                {
                    Console.WriteLine(line, odd3nnumbers[i]);
                }
            }
        }

        /// <summary>
        /// Print the numbers from NumbersB except the first 3
        /// </summary>
        static void PrintAllNumbersBbutFirst3()
        {
            string line = "{0,-24}";
            Console.WriteLine(line, "All(but first 3) Numbers");
            Console.WriteLine("========================");

            for (int i = 3; i <= DataLoader.NumbersB.Length - 1; i++)
            {
                Console.WriteLine(line, DataLoader.NumbersB[i]);
            }
        }

        /// <summary>
        /// Print the Company Name and most recent order for each customer in Washington
        /// </summary>
        static void PrintCompanyNameRecentOrderAllCustomersWA()
        {
            var customers = DataLoader.LoadCustomers();
            var WAcustomers = customers.Where(c => c.Region != null && c.Region.ToUpper() == "WA").ToList();
            var anoncustomers = WAcustomers.Select(wac => new
            {
                CompanyName = wac.CompanyName,
                Orders = wac.Orders.OrderByDescending(order => order.OrderDate)
            }).ToList();

            foreach (var customer in anoncustomers)
            {
                Console.WriteLine("==============================================================================");
                Console.WriteLine(customer.CompanyName);
                Console.WriteLine();
                Console.WriteLine("\tRecent Order");
                Console.WriteLine("\t{0} {1:MM-dd-yyyy} {2,10:c}", customer.Orders.First().OrderID, customer.Orders.First().OrderDate, customer.Orders.First().Total);
                Console.WriteLine("==============================================================================");
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Print all the numbers in NumbersC until a number is >= 6
        /// </summary>
        static void PrintFirstNumbersCLessthan6()
        {
            string line = "{0,-25}";
            Console.WriteLine(line, "First Numbers Less than 6");
            Console.WriteLine("=========================");

            var Numberslessthan6 = DataLoader.NumbersC.TakeWhile(n => n < 6);
            foreach (var number in Numberslessthan6)
            {
                Console.WriteLine(line, number);
            }
        }

        /// <summary>
        /// Print all the numbers in NumbersC that come after the first number divisible by 3
        /// </summary>
        static void PrintNumbersCAfterFirstDivisbleby3()
        {
            string line = "{0,-34}";
            Console.WriteLine(line, "Numbers after first divisible by 3");
            Console.WriteLine("==================================");

            var NumbersAfternumberDivisbleby3 = DataLoader.NumbersC.SkipWhile(n => n % 3 != 0).Skip(1);
            foreach (var number in NumbersAfternumberDivisbleby3)
            {
                Console.WriteLine(line, number);
            }
        }

        /// <summary>
        /// Print the products alphabetically by name
        /// </summary>
        static void PrintAllProductsSortByNameAscending()
        {
            List<Product> products = DataLoader.LoadProducts();
            var productssortbynames = products.OrderBy(p => p.ProductName).ToList();
            PrintProductInformation(productssortbynames);
        }

        /// <summary>
        /// Print the products in descending order by units in stock
        /// </summary>
        static void PrintAllProductsSortByUnitsInStockDescending()
        {
            List<Product> products = DataLoader.LoadProducts();
            var productssortbyunitsinstock = products.OrderByDescending(p => p.UnitsInStock).ToList();
            PrintProductInformation(productssortbyunitsinstock);
        }

        /// <summary>
        /// Print the list of products ordered first by category, then by unit price, from highest to lowest.
        /// </summary>
        static void PrintAllProductsSortByCategoryandUnitPrice()
        {
            List<Product> products = DataLoader.LoadProducts();
            var productssortbyCategoryandUnitPrice = products.OrderBy(p => p.Category).ThenByDescending(p => p.UnitPrice).ToList();
            PrintProductInformation(productssortbyCategoryandUnitPrice);
        }

        /// <summary>
        /// Print NumbersB in reverse order
        /// </summary>
        static void PrintNumbersBinreverse()
        {
            var reversenumbers = DataLoader.NumbersB.Reverse();

            string line = "{0,-19}";
            Console.WriteLine(line, "NumbersB in Reverse");
            Console.WriteLine("===================");

            foreach (var number in reversenumbers)
            {
                Console.WriteLine(line, number);
            }
        }

        /// <summary>
        /// Group products by category, then print each category name and its products
        /// ex:
        /// 
        /// Beverages
        /// Tea
        /// Coffee
        /// 
        /// Sandwiches
        /// Turkey
        /// Ham
        /// </summary>
        static void PrintCategoryandProducts()
        {
            List<Product> products = DataLoader.LoadProducts();
            var categoryproducts = from product in products
                                   orderby product.Category, product.ProductName
                                   group product by product.Category;

            string line = "{0,-50}";
            Console.WriteLine(line, "Category and Product Names");
            Console.WriteLine("==============================================");

            foreach (var category in categoryproducts)
            {
                Console.WriteLine(line, category.Key);
                foreach (var product in category)
                {
                    Console.WriteLine("\t{0}", product.ProductName);
                }
            }
        }

        /// <summary>
        /// Print all Customers with their orders by Year then Month
        /// ex:
        /// 
        /// Joe's Diner
        /// 2015
        ///     1 -  $500.00
        ///     3 -  $750.00
        /// 2016
        ///     2 - $1000.00
        /// </summary>
        static void PrintCustomersOrderYearMonth()
        {
            var customers = DataLoader.LoadCustomers();
            var customerssortbyNameandorder = from customer in customers
                                              where customer.Orders.Any() is true
                                              orderby customer.CompanyName
                                              group customer by customer.CompanyName into customerCompanyName
                                              from customer1 in
                                                    (from customerC in customerCompanyName
                                                     from order in customerC.Orders
                                                     where customerC.CompanyName == customerCompanyName.Key
                                                     orderby order.OrderDate.Year
                                                     group customerC by order.OrderDate.Year into customerYear
                                                     from customer2 in
                                                            (from customerY in customerYear
                                                             from order in customerY.Orders
                                                             where customerY.CompanyName == customerCompanyName.Key && 
                                                                   order.OrderDate.Year == customerYear.Key 
                                                             orderby order.OrderDate.Month
                                                             group customerY by order.OrderDate.Month
                                                             into customerMonth
                                                             select new
                                                             {
                                                                 month = customerMonth.Key,
                                                                 monthlyordertotals =
                                                                              (from customerM in customers
                                                                               from order in customerM.Orders
                                                                               where customerM.CompanyName == customerCompanyName.Key &&
                                                                                     order.OrderDate.Year  == customerYear.Key        &&
                                                                                     order.OrderDate.Month == customerMonth.Key 
                                                                               select order.Total).ToList().Sum() 
                                                             })
                                                     group customer2 by customerYear.Key)
                                              group customer1 by customerCompanyName.Key;

            foreach (var customerCompanyName in customerssortbyNameandorder)
            {
                Console.WriteLine("==============================================================================");
                Console.WriteLine(customerCompanyName.Key);
                Console.WriteLine("\tOrders Totals Grouped by Year and then Month");
                foreach (var customerYear in customerCompanyName)
                {
                    Console.WriteLine(customerYear.Key);
                    foreach (var customermonth in customerYear)
                    {
                        Console.WriteLine("\t{0} - {1,10:c}", customermonth.month, customermonth.monthlyordertotals);
                    }
                }
            }
        }

        /// <summary>
        /// Print the unique list of product categories
        /// </summary>
        static void PrintUniqueProductCategories()
        {
            List<Product> products = DataLoader.LoadProducts();
            var uniquecategories = from product in products
                                   group product by product.Category;

            string line = "{0,-15}";
            Console.WriteLine(line, "Category");
            Console.WriteLine("==============");

            foreach (var product in uniquecategories)
            {
                Console.WriteLine(line, product.Key);
            }
        }

        /// <summary>
        /// Write code to check to see if Product 789 exists
        /// </summary>
        static void CheckProduct789()
        {
            List<Product> products = DataLoader.LoadProducts();
            var productexists = (from product in products
                                 where product.ProductID == 789
                                 select product).Any();
            if (productexists)
            {
                Console.WriteLine("Product {0} exists", 789);
            }
            else
            {
                Console.WriteLine("Product {0} is not available", 789);
            }
        }

        /// <summary>
        /// Print a list of categories that have at least one product out of stock
        /// </summary>
        static void PrintCategorieswithoneoutofstockproducts()
        {
            List<Product> products = DataLoader.LoadProducts();
            var uniquecategories = from product in products
                                   where product.UnitsInStock == 0
                                   group product by product.Category;

            string line = "{0,-15}";
            Console.WriteLine(line, "Category");
            Console.WriteLine("==============");

            foreach (var category in uniquecategories)
            {
                Console.WriteLine(line, category.Key);
            }
        }

        /// <summary>
        /// Print a list of categories that have no products out of stock
        /// </summary>
        static void PrintCategorieswithnooutofstockproducts()
        {
            List<Product> products = DataLoader.LoadProducts();
            var instockproducts = (from product in products
                                   where product.UnitsInStock > 0
                                   group product by product.Category);

            string line = "{0,-15}";
            Console.WriteLine(line, "Category");
            Console.WriteLine("==============");

            foreach (var category in instockproducts)
            {
                var outofproductexists = (from product in products
                                          where product.UnitsInStock == 0 && product.Category == category.Key
                                          select product).Any();
                if (!outofproductexists)
                {
                    Console.WriteLine(line, category.Key);
                }
            }
        }

        /// <summary>
        /// Count the number of odd numbers in NumbersA
        /// </summary>
        static void PrintOddNumbersACount()
        {
            var oddnnumbersA = DataLoader.NumbersA.Where(n => n % 2 != 0).ToList();

            string line = "{0,-11}";
            Console.WriteLine(line, "Odd NumbersA Count");
            Console.WriteLine("==================");
            Console.WriteLine(line, oddnnumbersA.Count);
        }

        /// <summary>
        /// Create and print an anonymous type containing CustomerId and the count of their orders
        /// </summary>
        static void PrintAnonTypeCustomerIDandOrdersCount()
        {
            var customers = DataLoader.LoadCustomers();
            var customersorderscount = from customer in customers
                                       select new
                                       {
                                           CustomerID = customer.CustomerID,
                                           OrdersCount = (customer.Orders.Any()) ? customer.Orders.Count() : 0
                                       };

            foreach (var customer in customersorderscount)
            {
                Console.WriteLine("==============================================================================");
                Console.WriteLine(customer.CustomerID);
                Console.WriteLine("\tOrders Count");
                Console.WriteLine(customer.OrdersCount);
                Console.WriteLine("==============================================================================");
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Print a distinct list of product categories and the count of the products they contain
        /// </summary>
        static void PrintDistinctCategoriesandProductsCount()
        {
            List<Product> products = DataLoader.LoadProducts();
            var productscategories = (from product in products
                                      group product by product.Category);

            string line = "{0,-15} {1,-3}";
            Console.WriteLine(line, "Category", "Products Count");
            Console.WriteLine("==============================");

            foreach (var category in productscategories)
            {
                Console.WriteLine(line, category.Key, category.Count());
            }
        }

        /// <summary>
        /// Print a distinct list of product categories and the total units in stock
        /// </summary>
        static void PrintDistinctCategoriesandUnitsInStockCount()
        {
            List<Product> products = DataLoader.LoadProducts();
            var productscategories = (from product in products
                                      group product by product.Category);

            string line = "{0,-15} {1,-3}";
            Console.WriteLine(line, "Category", "UnitsInStock Count");
            Console.WriteLine("==================================");

            foreach (var category in productscategories)
            {
                var UnitsInStockList = (from product in products
                                        where product.Category == category.Key
                                        select product.UnitsInStock);
                Console.WriteLine(line, category.Key, UnitsInStockList.Sum());
            }
        }

        /// <summary>
        /// Print a distinct list of product categories and the lowest priced product in that category
        /// </summary>
        static void PrintDistinctCategoriesandLowestPricedProduct()
        {
            List<Product> products = DataLoader.LoadProducts();
            var productscategories = (from product in products
                                      orderby product.UnitPrice
                                      group product by product.Category into tempproducts
                                      select new
                                      {
                                          Category = tempproducts.Key,
                                          ProductName = tempproducts.First().ProductName
                                      });

            string line = "{0,-15} {1,-35}";
            Console.WriteLine(line, "Category", "ProductName");
            Console.WriteLine("=======================================");
            foreach (var product in productscategories)
            {

                Console.WriteLine(line, product.Category, product.ProductName);

            }
            Console.WriteLine("=======================================");
        }

        /// <summary>
        /// Print the top 3 categories by the average unit price of their products
        /// </summary>
        static void PrintTop3DistinctCategoriesAvgUnitPriceProduct()
        {
            List<Product> products = DataLoader.LoadProducts();
            var productscategories = (from product in products
                                      group product by product.Category into tempproduct
                                      select new
                                      {
                                          Category = tempproduct.Key,
                                          UnitPriceAverage = tempproduct.Average(product => product.UnitPrice)
                                      }).ToList();
            var productscategoriessort = (from product in productscategories
                                          orderby product.UnitPriceAverage descending
                                          select product).Take(3).ToList();

            string line = "{0,-15}";
            Console.WriteLine(line, "Category");
            Console.WriteLine("==============");

            foreach (var product in productscategoriessort)
            {
                Console.WriteLine(line, product.Category);
            }
        }
    }
}
