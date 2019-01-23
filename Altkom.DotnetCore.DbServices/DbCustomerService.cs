using Altkom.DotnetCore.IServices;
using Altkom.DotnetCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Altkom.DotnetCore.DbServices
{
    public class DbCustomerService : ICustomerService
    {
        private readonly MyContext context;

        public DbCustomerService(MyContext context)
        {
            this.context = context;
        }

        public void Add(Customer item)
        {
            Trace.WriteLine($"{context.Entry(item).State}");

            context.Customers.Add(item);
            Trace.WriteLine($"{context.Entry(item).State}");

            context.SaveChanges();
            Trace.WriteLine($"{context.Entry(item).State}");
        }

        public IEnumerable<Customer> Get()
        {
            //return from c in context.Customers
            //       orderby c.FirstName, c.LastName
            //       select c;

            //return context.Customers                
            //    .OrderBy(c => c.FirstName)
            //    .ThenBy(c => c.LastName)
            //    .ToList();                

            return context.Customers.ToList();
        }

        public Customer Get(int id)
        {
            return context.Customers.Find(id);
        }

        public void Remove(int id)
        {
            Customer customer = new Customer { Id = id };
            context.Customers.Attach(customer);
            context.Entry(customer).State = EntityState.Deleted;
            context.SaveChanges();
        }

        public void Update(Customer item)
        {
            //context.Customers.Attach(item);
            //context.Entry(item).State = EntityState.Modified;
            context.Update(item);
            context.SaveChanges();
        }
    }
}
