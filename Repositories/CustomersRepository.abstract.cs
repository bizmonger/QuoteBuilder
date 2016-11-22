using System;
using Entities;
using Mediation;
using static Bizmonger.Patterns.MessageBus;

namespace Repositories
{
    public abstract partial class AbstractCustomersRepository
    {
        protected void MakePromises()
        {
            Subscribe(Messages.REQUEST_CUSTOMERS, OnRequestCustomersResponse);
            Subscribe(Messages.REQUEST_CUSTOMER, OnRequestCustomerResponse);
            Subscribe(Messages.SAVE_CUSTOMER, OnSaveCustomer);
        }

        protected void breakExistingPromises()
        {
            Unsubscribe(Messages.REQUEST_CUSTOMERS, OnRequestCustomersResponse);
            Unsubscribe(Messages.REQUEST_CUSTOMER, OnRequestCustomerResponse);
            Unsubscribe(Messages.SAVE_CUSTOMER, OnSaveCustomer);
        }

        protected void SaveData(Customer customer) => _database.OnSave(customer);
    }
}