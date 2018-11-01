// Copyright © Microsoft Corporation.  All Rights Reserved.
// This code released under the terms of the 
// Microsoft Public License (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//Copyright (C) Microsoft Corporation.  All rights reserved.

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using SampleSupport;
using Task.Data;

// Version Mad01

namespace SampleQueries
{
	[Title("LINQ Module")]
	[Prefix("Linq")]
	public class LinqSamples : SampleHarness
	{

		private DataSource dataSource = new DataSource();

        [Category("Restriction Operators")]
        [Title("Where - Task 001")]
        [Description("This sample return all customers who have orders sum more then specific values")]
        public void Linq001()
        {
            var customers = dataSource.Customers.Where(c => c.Orders.Sum(o => o.Total) > 25000)
                .Where(c => c.Orders.Sum(o => o.Total) > 50000)
                .Where(c => c.Orders.Sum(o => o.Total) > 75000)
                .Select(c => new {
                customerId = c.CustomerID,
                total = c.Orders.Sum(o => o.Total)
            });

            foreach (var p in customers) {
                ObjectDumper.Write(p);
            }
        }


	    [Category("Restriction Operators")]
	    [Title("Where - Task 002")]
	    [Description("This sample return all suppliers list to each customer who leaving in the same country with suppliers")]
	    public void Linq002()
	    {
	        var customers = dataSource.Customers
	            .Select(s => new {
	                customer = s,
	                suppliers = dataSource.Suppliers.Where(sup => sup.Country == s.Country && sup.City == s.City)
	            });

	        ObjectDumper.Write($"Without grouping");

            foreach (var item in customers) {
	            ObjectDumper.Write($"CustomerId: {item.customer.CustomerID} " +
	                               $"List of suppliers: {string.Join(", ", item.suppliers.Select(s => s.SupplierName))}");
	        }

	           var customersWithGrouping = dataSource.Customers.GroupJoin(dataSource.Suppliers,
	            c => new { c.City, c.Country },
	            s => new { s.City, s.Country },
	            (c, s) => new { Customer = c, Suppliers = s });

	        ObjectDumper.Write($"With grouping");

	        foreach (var item in customersWithGrouping) {
	            ObjectDumper.Write($"CustomerId: {item.Customer.CustomerID} " +
	                               $"List of suppliers: {string.Join(", ", item.Suppliers.Select(s => s.SupplierName))}");
	        }
        }

        [Category("Restriction Operators")]
        [Title("Where - Task 003")]
        [Description("This sample return all customers who have orders more then specific values")]
        public void Linq003()
        {
            var orderValue = 1000;
            var customers = dataSource.Customers
                .Where(c => c.Orders
                .Any(o => o.Total > orderValue))
                .Select(c => new {
                    customerName = c.CompanyName
                });

            foreach (var p in customers) {
                ObjectDumper.Write(p);
            }
        }

        [Category("Restriction Operators")]
        [Title("Where - Task 004")]
        [Description("This sample return all customers with begining orders date.")]
        public void Linq004()
        {
            var customers = dataSource.Customers
                  .Select(c => new {
                      customerName = c.CompanyName,
                      customerStartDate = c.Orders.Select(o => o.OrderDate).Min()
                  });

            foreach (var p in customers) {
                ObjectDumper.Write(p);
            }
        }


        [Category("Restriction Operators")]
        [Title("Where - Task 005")]
        [Description("This sample return all customers with begining orders date and Order By")]
        public void Linq005()
        {
            var customers = dataSource.Customers.Where(c => c.Orders.Length > 0)
                  .Select(c => new {
                      customerName = c.CompanyName,
                      customerTotal = c.Orders.Select(o => o.Total).First(),
                      customerStartDate = c.Orders.Select(o => o.OrderDate).Min()
                  }).OrderByDescending(o => o.customerStartDate).ThenByDescending(o => o.customerTotal).ThenBy(o => o.customerName);


            foreach (var customer in customers) {
                ObjectDumper.Write($"customerName: {customer.customerName} " +
                                 $"customerTotal: {customer.customerTotal} customerDate: {customer.customerStartDate}");
            }
        }

        [Category("Restriction Operators")]
        [Title("Where - Task 006")]
        [Description("This sample return all customers with begining orders date and Order By")]
        public void Linq006()
        {
            var customers = dataSource.Customers
                .Where(cu => !string.IsNullOrEmpty(cu.PostalCode))
                .Where(c => c.PostalCode.Any(s => char.IsDigit(s)) || string.IsNullOrEmpty(c.Region) || !c.Phone.Contains("("))
                  .Select(c => new {
                      customerName = c.CompanyName
                  });

            foreach (var customer in customers) {
                ObjectDumper.Write($"customerName: {customer.customerName}");
            }
        }


        [Category("Restriction Operators")]
        [Title("Where - Task 007")]
        [Description("This sample return all products with groups by Category and UnitsInStock with ordering by UnitCost for UnitsInStock group")]
        public void Linq007()
        {
            var productGroups = dataSource.Products
                .GroupBy(p => p.Category)
                .Select(g => new {
                    Name = g.Key,
                    ProductsByStock = g.GroupBy(g2 => g2.UnitsInStock)
                        .Select(s => new {
                            Name = s.Key,
                            Products = s.OrderBy(o => o.UnitPrice)
                        })
                });

            foreach (var group in productGroups) {
                ObjectDumper.Write($"Product category: {group.Name}");
                foreach (var productByStock in group.ProductsByStock) {
                    ObjectDumper.Write($"In Stock: {productByStock.Name}");
                    foreach (var product in productByStock.Products) {
                        ObjectDumper.Write($"Product name: {product.ProductName} Price: {product.UnitPrice}");
                    }
                }
            }
        }


	    [Category("Restriction Operators")]
	    [Title("Where - Task 008")]
	    [Description("This sample return all products by three groups")]
	    public void Linq008()
	    {
	        var groups = dataSource.Products
	            .GroupBy(p => p.UnitPrice < 20 ? "Deshman"
	                : p.UnitPrice < 50 ? "norm" : "Elita");

	        foreach (var group in groups) {
	            ObjectDumper.Write($"{group.Key}:");
	            foreach (var product in group) {
	                ObjectDumper.Write($"Product name: {product.ProductName} Price: {product.UnitPrice}\n");
	            }
	        }
        }

	    [Category("Restriction Operators")]
	    [Title("Where - Task 009")]
	    [Description("This sample return all products by three groups")]
	    public void Linq009()
	    {
	        var cityGroups = dataSource.Customers
	            .GroupBy(g => g.City)
	            .Select(s => new {
	                City = s.Key,
	                AverageSum = s
	                    .Average(a => a.Orders.Sum(o => o.Total)),
	                AverageWanna = s
	                    .Average(o => o.Orders.Count())
	            });

	        foreach (var city in cityGroups) {
	            ObjectDumper.Write($"City:{city.City}, Average sum of orders: {city.AverageSum}, Average count of orders: {city.AverageWanna}");
	        }
        }

	    [Category("Restriction Operators")]
	    [Title("Where - Task 010")]
	    [Description("Average year statistic of activity clients per month")]
	    public void Linq010()
	    {
	        var statistic = dataSource.Customers
	            .Select(c => new {
	                c.CompanyName,
	                Months = c.Orders.GroupBy(o => o.OrderDate.Month)
	                    .Select(g => new { Month = g.Key, OrdersCount = g.Count() }),
	                Years = c.Orders.GroupBy(o => o.OrderDate.Year)
	                    .Select(g => new { Year = g.Key, OrdersCount = g.Count() }),
	                YearMonth = c.Orders
	                    .GroupBy(o => new { o.OrderDate.Year, o.OrderDate.Month })
	                    .Select(g => new { g.Key.Year, g.Key.Month, OrdersCount = g.Count() })
	            });

	        foreach (var record in statistic) {
	            ObjectDumper.Write($"Company name: {record.CompanyName}");
	            ObjectDumper.Write("Months statistic:");
	            foreach (var month in record.Months) {
	                ObjectDumper.Write($"Month: {month.Month} Orders count: {month.OrdersCount}");
	            }

	            ObjectDumper.Write("Years statistic:");
	            foreach (var year in record.Years) {
	                ObjectDumper.Write($"Year: {year.Year} Orders count: {year.OrdersCount}");
	            }

	            ObjectDumper.Write("Year and month statistic:");
                foreach (var yearMonth in record.YearMonth) {
	                ObjectDumper.Write($"Year: {yearMonth.Year} Month: {yearMonth.Month} Orders count: {yearMonth.OrdersCount}");
	            }
	        }
        }
    }
}
