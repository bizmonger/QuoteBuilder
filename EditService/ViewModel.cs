using System.Collections.ObjectModel;
using Entities;
using UILogic;


namespace EditService
{
    public sealed partial class ViewModel : ViewModelBase
    {
        public ViewModel()
        {
            MakePromises();
            ActivateCommands();
            SendRequests();
        }

        string _name = null;
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value?.Trim())
                {
                    _name = value?.Trim();
                    OnPropertyChanged();
                    Update.RaiseCanExecuteChanged();
                }
            }
        }
        string _description = "Standard";
        public string Description
        {
            get { return _description; }
            set
            {
                if (_description != value?.Trim())
                {
                    _description = value?.Trim();
                    OnPropertyChanged();
                }
            }
        }
        string _laborCost = null;
        public string LaborCost
        {
            get { return _laborCost; }
            set
            {
                if (_laborCost != value)
                {
                    _laborCost = value;
                    OnPropertyChanged();
                    Update.RaiseCanExecuteChanged();
                }
            }
        }

        string _taxPercentage = null;
        public string TaxPercentage
        {
            get { return _taxPercentage; }
            set
            {
                if (_taxPercentage != value)
                {
                    _taxPercentage = value;
                    OnPropertyChanged();
                    Update.RaiseCanExecuteChanged();
                }
            }
        }

        bool _isUpdated = false;
        public bool IsUpdated
        {
            get { return _isUpdated; }
            set
            {
                if (_isUpdated != value)
                {
                    _isUpdated = value;
                    OnPropertyChanged();
                }
            }
        }

        ObservableCollection<Material> _materials = new ObservableCollection<Material>();
        public ObservableCollection<Material> Materials
        {
            get { return _materials; }
            set
            {
                if (_materials != value)
                {
                    _materials = value;
                    OnPropertyChanged();
                }
            }
        }

        Service _serviceToUpdate = null;
        public Service ServiceToUpdate
        {
            get { return _serviceToUpdate; }
            set
            {
                if (_serviceToUpdate != value)
                {
                    _serviceToUpdate = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}