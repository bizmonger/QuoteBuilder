using System;
using System.Collections.Generic;
using System.Linq;
using Entities;
using Repositories.Core;
using System.Diagnostics;

namespace TestAPI
{
    [DebuggerNonUserCode]
    public class MockCustomersDatabase : AbstractCustomersDatabase
    {
        List<Customer> _customers = new List<Customer>();

        public override void Initialize() { }

        protected override void Add(Customer customer) =>
            _customers.Add(customer);

        protected override IEnumerable<Customer> Get(string profileId) => _customers;

        protected override Customer ReadFromCustomerId(string customerId) =>
            _customers.SingleOrDefault(c => c.Id == customerId);

        protected override void Update(Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}