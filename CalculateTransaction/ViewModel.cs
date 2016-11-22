using System.Collections.ObjectModel;
using Entities;
using Transaction;
using UILogic;

namespace CalculateTransaction
{
    public sealed partial class ViewModel : ViewModelBase
    {
        public ViewModel()
        {
            MakePromises();
            ActivateCommands();
            Initialize();
            RefreshEntries();
            UpdateSummary();
        }
        
        ObservableCollection<Entry> _entries = new ObservableCollection<Entry>();
        public ObservableCollection<Entry> Entries
        {
            get
            {
                return _entries;
            }

            set
            {
                if (_entries != value)
                {
                    _entries = value;
                    OnPropertyChanged();
                }
            }
        }

        ObservableCollection<Service> _services = new ObservableCollection<Service>();
        public ObservableCollection<Service> Services
        {
            get
            {
                return _services;
            }

            set
            {
                if (_services != value)
                {
                    _services = value;
                    OnPropertyChanged();
                }
            }
        }

        Entry _selectedEntry = null;
        public Entry SelectedEntry
        {
            get
            {
                return _selectedEntry;
            }

            set
            {
                if (_selectedEntry != value)
                {
                    _selectedEntry = value;
                    OnPropertyChanged();
                }
            }
        }

        string _title = string.Empty;
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                if (_title != value?.Trim())
                {
                    _title = value?.Trim();
                    OnPropertyChanged();
                }
            }
        }

        string _description = string.Empty;
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                if (_description != value?.Trim())
                {
                    _description = value?.Trim();
                    OnPropertyChanged();
                }
            }
        }

        decimal _materials = 0.00m;
        public decimal MaterialsCost
        {
            get
            {
                return _materials;
            }
            set
            {
                if (_materials != value)
                {
                    _materials = value;
                    OnPropertyChanged();
                }
            }
        }

        decimal _labor = 0.00m;
        public decimal LaborCost
        {
            get
            {
                return _labor;
            }
            set
            {
                if (_labor != value)
                {
                    _labor = value;
                    OnPropertyChanged();
                }
            }
        }

        decimal _subtotal = 0.00m;
        public decimal Subtotal
        {
            get
            {
                return _subtotal;
            }
            set
            {
                if (_subtotal != value)
                {
                    _subtotal = value;
                    OnPropertyChanged();
                }
            }
        }

        decimal _tax = 0.00m;
        public decimal Tax
        {
            get
            {
                return _tax;
            }
            set
            {
                if (_tax != value)
                {
                    _tax = value;
                    OnPropertyChanged();
                }
            }
        }

        decimal _total = 0.00m;
        public decimal Total
        {
            get
            {
                return _total;
            }
            set
            {
                if (_total != value)
                {
                    _total = value;
                    OnPropertyChanged();
                }
            }
        }

        //public void Remove()
        //{
        //    if (_entryDictionary.Count > 0)
        //    {
        //        var service = _entryDictionary.Where(e => e.Value == _selectedEntry).Single().Key;
        //        Remove(service);
        //    }

        //    UpdateSummary();
        //}

        public void Initialize()
        {
            Services.Clear();
            Entries.Clear();

            _registry.Clear();
            _entryDictionary.Clear();

            UpdateSummary();
        }
    }
}