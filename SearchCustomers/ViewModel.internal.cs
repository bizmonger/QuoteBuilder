using static Bizmonger.Patterns.MessageBus;
using Entities;
using Mediation;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SearchCustomers
{
    public partial class ViewModel
    {


        protected override void MakePromises()
        {
            Subscribe(Messages.REQUEST_CUSTOMERS_RESPONSE, OnCustomersLoaded);
            Subscribe(Messages.REQUEST_CUSTOMER_RESPONSE, OnSearchResults);
        }

        protected override void BreakPromises()
        {
            Unsubscribe(Messages.REQUEST_CUSTOMERS_RESPONSE, OnCustomersLoaded);
            Unsubscribe(Messages.REQUEST_CUSTOMER_RESPONSE, OnSearchResults);
        }

        void SendMessages() => Load.Execute(null);

        void OnCustomersLoaded(object obj) =>
            Customers = new ObservableCollection<Customer>(obj as IEnumerable<Customer>);

        void OnSearchResults(object obj) =>
            Results = new ObservableCollection<Customer>(obj as IEnumerable<Customer>);
    }
}