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
                  .Select(c => new
                  {
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
            var customers = dataSource.Customers.Where(c => c.PostalCode.Any(s => char.IsDigit(s)) || string.IsNullOrEmpty(c.Region) || !c.Phone.Contains("("))
                  .Select(c => new
                  {
                      customerName = c.CompanyName
                  });

            foreach (var customer in customers) {
                ObjectDumper.Write($"customerName: {customer.customerName}");
            }
        }


        [Category("Restriction Operators")]
        [Title("Where - Task 007")]
        [Description("This sample return all customers with begining orders date and Order By")]
        public void Linq007()
        {
            var customers = dataSource.Customers.Where(c => c.PostalCode.Any(s => char.IsDigit(s)) || string.IsNullOrEmpty(c.Region) || !c.Phone.Contains("("))
                  .Select(c => new
                  {
                      customerName = c.CompanyName
                  });

            foreach (var customer in customers)
            {
                ObjectDumper.Write($"customerName: {customer.customerName}");
            }
        }

    }
}
