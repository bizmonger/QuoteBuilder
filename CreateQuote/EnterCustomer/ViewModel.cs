using Entities;
using System.Collections.Generic;
using UILogic;

namespace CreateQuote.EnterCustomer
{
    public sealed partial class ViewModel : ViewModelBase
    {
        public ViewModel()
        {
            MakePromises();
            ActivateCommands();
            SendRequests();
        }

        string _title = null;
        public string Title
        {
            get { return _title; }
            set
            {
                if (_title != value?.Trim())
                {
                    _title = value?.Trim();
                    OnPropertyChanged();
                    Generate.RaiseCanExecuteChanged();
                }
            }
        }
        string _firstName = null;
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                if (_firstName != value?.Trim())
                {
                    _firstName = value?.Trim();
                    OnPropertyChanged();
                    Generate.RaiseCanExecuteChanged();
                }
            }
        }
        string _lastName = null;
        public string LastName
        {
            get { return _lastName; }
            set
            {
                if (_lastName != value?.Trim())
                {
                    _lastName = value?.Trim();
                    OnPropertyChanged();
                    Generate.RaiseCanExecuteChanged();
                }
            }
        }
        string _phone = null;
        public string Phone
        {
            get { return _phone; }
            set
            {
                if (_phone != value?.Trim())
                {
                    _phone = value?.Trim();
                    OnPropertyChanged();
                    Generate.RaiseCanExecuteChanged();
                }
            }
        }

        string _email = null;
        public string Email
        {
            get { return _email; }
            set
            {
                if (_email != value?.Trim())
                {
                    _email = value?.Trim();
                    OnPropertyChanged();
                    Generate.RaiseCanExecuteChanged();
                }
            }
        }

        decimal _total = 0.00m;
        public decimal Total
        {
            get { return _total; }
            set
            {
                if (_total != value)
                {
                    _total = value;
                    OnPropertyChanged();
                    Generate.RaiseCanExecuteChanged();
                }
            }
        }

        decimal _subtotal = 0.00m;
        public decimal Subtotal
        {
            get { return _subtotal; }
            set
            {
                if (_subtotal != value)
                {
                    _subtotal = value;
                    OnPropertyChanged();
                    Generate.RaiseCanExecuteChanged();
                }
            }
        }

        decimal _materialsCost = 0.00m;
        public decimal MaterialsCost
        {
            get { return _materialsCost; }
            set
            {
                if (_materialsCost != value)
                {
                    _materialsCost = value;
                    OnPropertyChanged();
                    Generate.RaiseCanExecuteChanged();
                }
            }
        }

        decimal _laborCost = 0.00m;
        public decimal LaborCost
        {
            get { return _laborCost; }
            set
            {
                if (_laborCost != value)
                {
                    _laborCost = value;
                    OnPropertyChanged();
                    Generate.RaiseCanExecuteChanged();
                }
            }
        }

        decimal _tax = 0.00m;
        public decimal Tax
        {
            get { return _tax; }
            set
            {
                if (_tax != value)
                {
                    _tax = value;
                    OnPropertyChanged();
                    Generate.RaiseCanExecuteChanged();
                }
            }
        }

        public IEnumerable<Service> SelectedServices { get; set; }
    }
}