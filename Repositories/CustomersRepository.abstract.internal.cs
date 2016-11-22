using System.Collections.Generic;
using Entities;
using Entities.Utilities;
using Mediation;
using static Bizmonger.Patterns.MessageBus;
using Repositories.Core;
using System.Linq;

namespace Repositories
{
    public abstract partial class AbstractCustomersRepository
    {
        readonly List<Customer> _customers = new List<Customer>();

        protected void InitializeDatabase()
        {
            Subscribe(Messages.REQUEST_CUSTOMERS_DATABASE_RESPONSE, obj => _database = obj as IDatabase);
            Publish(Messages.REQUEST_CUSTOMERS_DATABASE);

            _database.Initialize();
        }

        void OnRequestCustomerResponse(object obj)
        {
            var str = obj as string;
            if (str == null) return;

            var text = str.ToLower();
            var result = _customers.Where(c => c.FirstName.ToLower().Contains(text) || c.LastName.ToLower().Contains(text));
            Publish(Messages.REQUEST_CUSTOMER_RESPONSE, result);
        }

        void OnRequestCustomersResponse(object obj) =>
            Publish(Messages.REQUEST_CUSTOMERS_RESPONSE, _customers);

        void OnSaveCustomer(object obj) => Save(obj);

        protected bool Save(object entity)
        {
            var customer = entity as Customer;
            var result = _customers.FirstOrDefault(c => c.Id == customer.Id);

            if (result != null)
            {
                customer.Update(result);
                SaveData(result);
            }
            else
            {
                SaveData(customer);
                _customers.Add(customer);
            }
            return true;
        }
    }
}