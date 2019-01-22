using Altkom.DotnetCore.IServices;
using Altkom.DotnetCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Altkom.DotnetCore.FakeServices
{
    public class FakeCustomerService : ICustomerService
    {
        private ICollection<Customer> customers;

        private CustomerFaker customerFaker;

        // snippet: ctor
        public FakeCustomerService(CustomerFaker customerFaker)
        {
            this.customerFaker = customerFaker;

            customers = customerFaker.Generate(100);

        }

        public void Add(Customer item) => customers.Add(item);

        public IEnumerable<Customer> Get() => customers;

        public Customer Get(int id)
        {
            //return customers
            //    .Where(c => c.Id == id)
            //    .SingleOrDefault();

            return customers.SingleOrDefault(c => c.Id == id);
        }

        public void Remove(int id) => customers.Remove(Get(id));
    }
}
