using System.Collections.ObjectModel;
using Entities;
using UILogic;

namespace ManageServices
{
    public sealed partial class ViewModel : ViewModelBase
    {
        public ViewModel()
        {
            MakePromises();
            ActivateCommands();
            SendRequests();
        }

        ObservableCollection<Service> _services = null;
        public ObservableCollection<Service> Services
        {
            get { return _services; }
            set
            {
                if (_services != value)
                {
                    _services = value;
                    OnPropertyChanged();
                    Edit.RaiseCanExecuteChanged();
                    Remove.RaiseCanExecuteChanged();
                }
            }
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
                    Edit.RaiseCanExecuteChanged();
                    Remove.RaiseCanExecuteChanged();
                }
            }
        }
    }
}