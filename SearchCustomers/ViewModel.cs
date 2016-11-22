using System;
using System.Collections.ObjectModel;
using Entities;
using Mediation;
using UILogic;
using static Bizmonger.Patterns.MessageBus;

namespace SearchCustomers
{
    public sealed partial class ViewModel : ViewModelBase
    {
        public ViewModel()
        {
            var someLastName = "last_name";
            Publish(Messages.SAVE_CUSTOMER, new Customer() { FirstName = "albert", LastName = someLastName, Id = Guid.NewGuid().ToString() });
            Publish(Messages.SAVE_CUSTOMER, new Customer() { FirstName = "alvin", LastName = someLastName, Id = Guid.NewGuid().ToString() });
            Publish(Messages.SAVE_CUSTOMER, new Customer() { FirstName = "alex", LastName = someLastName, Id = Guid.NewGuid().ToString() });
            Publish(Messages.SAVE_CUSTOMER, new Customer() { FirstName = "ashish", LastName = someLastName, Id = Guid.NewGuid().ToString() });

            MakePromises();
            ActivateCommands();
            SendMessages();
        }

        public ObservableCollection<Customer> Customers { get; set; }

        ObservableCollection<Customer> _results = null;
        public ObservableCollection<Customer> Results
        {
            get { return _results; }
            set
            {
                if (_results != value)
                {
                    _results = value;
                    OnPropertyChanged();
                }
            }
        }

        Customer _selectedCustomer = null;
        public Customer SelectedCustomer
        {
            get { return _selectedCustomer; }
            set
            {
                if (_selectedCustomer != value)
                {
                    _selectedCustomer = value;
                    OnPropertyChanged();
                    View.RaiseCanExecuteChanged();
                }
            }
        }

        string _searchText = null;
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                if (_searchText != value)
                {
                    _searchText = value;
                    OnPropertyChanged();
                    Search.Execute(_searchText);
                }
            }
        }
    }
}