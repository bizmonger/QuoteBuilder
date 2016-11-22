using Entities;
using Repositories.Core;
using SQLite;
using System.Collections.Generic;
using Xamarin.Forms;
using System;
using System.Linq;

namespace Repositories.Details
{
    public class CustomersDatabase : AbstractCustomersDatabase
    {
        SQLiteConnection _databaseConnection = null;

        protected override IEnumerable<Customer> Get(string profileId) =>
            _databaseConnection.Table<Customer>().Where(c => c.UserId == profileId);

        public override void Initialize()
        {
            _databaseConnection = DependencyService.Get<IDatabaseConnection>().Connect();

            var tableExists = DependencyService.Get<IDatabaseConnection>().TableExists(_databaseConnection, "Customer");

            if (!tableExists)
            {
                _databaseConnection.CreateTable<Customer>();
            }
        }

        protected override void Add(Customer customer) => _databaseConnection.Insert(customer);

        protected override Customer ReadFromCustomerId(string customerId) =>
            _databaseConnection.Table<Customer>().FirstOrDefault(c => c.UserId == customerId);

        protected override void Update(Customer customer) => _databaseConnection.Update(customer);
    }
}