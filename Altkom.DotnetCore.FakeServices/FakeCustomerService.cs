using Altkom.DotnetCore.IServices;
using Altkom.DotnetCore.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Altkom.DotnetCore.FakeServices
{
    public class CustomerServiceOptions
    {
        public int Count { get; set; }
    }

    public class FakeCustomerService : ICustomerService
    {
        private ICollection<Customer> customers;

        private CustomerFaker customerFaker;

        // snippet: ctor
        public FakeCustomerService(CustomerFaker customerFaker, 
            IOptions<CustomerServiceOptions> options)
        {
            this.customerFaker = customerFaker;

            customers = customerFaker.Generate(options.Value.Count);

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

        public void Update(Customer item)
        {
            throw new NotImplementedException();
        }
    }
}
