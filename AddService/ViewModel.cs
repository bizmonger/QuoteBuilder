using System.Collections.ObjectModel;
using Entities;
using UILogic;

namespace AddService
{
    public sealed partial class ViewModel : ViewModelBase
    {
        public ViewModel()
        {
            MakePromises();
            ActivateCommands();
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
                    SaveCommand.RaiseCanExecuteChanged();
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
                    SaveCommand.RaiseCanExecuteChanged();
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
                    SaveCommand.RaiseCanExecuteChanged();
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
                    SaveCommand.RaiseCanExecuteChanged();
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
                    SaveCommand.RaiseCanExecuteChanged();
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
                    SaveCommand.RaiseCanExecuteChanged();
                }
            }
        }
        public bool Saved { get; set; }
    }
}