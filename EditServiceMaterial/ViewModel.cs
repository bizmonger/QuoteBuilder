using Entities;
using UILogic;

namespace EditServiceMaterial
{
    public sealed partial class ViewModel : ViewModelBase
    {
        public ViewModel()
        {
            MakePromises();
            ActivateCommands();
            SendRequests();
        }

        public Material Material { get; set; }
        string _name = null;
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }

        string _description = null;
        public string Description
        {
            get { return _description; }
            set
            {
                if (_description != value)
                {
                    _description = value;
                    OnPropertyChanged();
                }
            }
        }

        decimal _baseCost = 0.00m;
        public decimal BaseCost
        {
            get { return _baseCost; }
            set
            {
                if (_baseCost != value)
                {
                    _baseCost = value;
                    OnPropertyChanged();
                }
            }
        }

        decimal _markupPrice = 0.00m;
        public decimal MarkupPrice
        {
            get { return _markupPrice; }
            set
            {
                if (_markupPrice != value)
                {
                    _markupPrice = value;
                    OnPropertyChanged();
                }
            }
        }

        decimal _quantity = 0.0m;
        public decimal Quantity
        {
            get { return _quantity; }
            set
            {
                if (_quantity != value)
                {
                    _quantity = value;
                    OnPropertyChanged();
                }
            }
        }
        string _unitType = null;
        public string UnitType
        {
            get { return _unitType; }
            set
            {
                if (_unitType != value)
                {
                    _unitType = value;
                    OnPropertyChanged();
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

    }
}