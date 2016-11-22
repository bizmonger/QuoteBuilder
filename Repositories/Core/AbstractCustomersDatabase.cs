using System;
using System.Collections.Generic;
using Entities;
using Mediation;
using Servers;
using static Bizmonger.Patterns.MessageBus;

namespace Repositories.Core
{
    public abstract class AbstractCustomersDatabase : IDatabase
    {
        public void Read(string id)
        {
            var customer = ReadFromCustomerId(id);
            Publish(Messages.REQUEST_CUSTOMER_RESPONSE, customer);
        }

        public abstract void Initialize();

        public void OnSave(object entity)
        {
            var customer = entity as Customer;
            var existingCustomer = ReadFromCustomerId(customer.Id);

            if (existingCustomer != null)
            {
                Update(customer);
            }
            else
            {
                customer.Id = Guid.NewGuid().ToString();
                customer.UserId = new ProfileServer().GetProfile().Id;

                Add(customer);
                Publish(Messages.CUSTOMER_ADDED, customer);
            }
        }

        public void Read()
        {
            var profile = new ProfileServer().GetProfile();
            var materials = Get(profile.Id);

            Publish(Messages.REQUEST_MATERIALS_RESPONSE, materials);
        }

        protected abstract Customer ReadFromCustomerId(string customerId);

        protected abstract void Update(Customer customer);

        protected abstract void Add(Customer customer);
        protected abstract IEnumerable<Customer> Get(string profileId);
    }
}