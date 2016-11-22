using System.Collections.ObjectModel;
using Entities;
using UILogic;

namespace CreateQuote.SelectServices
{
    public sealed partial class ViewModel : ViewModelBase
    {
        public ViewModel()
        {
            MakePromises();
            ActivateCommands();
            SendRequests();
        }

        Service _selectedService = null;
        public Service SelectedService
        {
            get { return _selectedService; }
            set
            {
                if (_selectedService != value)
                {
                    _selectedService = value;
                    OnPropertyChanged();
                    UpdateState();
                }
            }
        }

        Service _promotedService = null;
        public Service  PromotedService
        {
            get { return _promotedService; }
            set
            {
                if (_promotedService != value)
                {
                    _promotedService = value;
                    OnPropertyChanged();
                    UpdateState();
                }
            }
        }

        ObservableCollection<Service> _services = new ObservableCollection<Service>();
        public ObservableCollection<Service> Services
        {
            get { return _services; }
            set
            {
                if (_services != value)
                {
                    _services = value;
                    OnPropertyChanged();
                    UpdateState();
                }
            }
        }

        ObservableCollection<Service> _selectedServices = new ObservableCollection<Service>();
        public ObservableCollection<Service> SelectedServices
        {
            get { return _selectedServices; }
            set
            {
                if (_selectedServices != value)
                {
                    _selectedServices = value;
                    OnPropertyChanged();
                    UpdateState();
                }
            }
        }
    }
}