using Entities;
using System.Diagnostics;
using UILogic;

namespace EditMaterial
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
        [DebuggerNonUserCode]
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value?.Trim())
                {
                    _name = value?.Trim();
                    OnPropertyChanged();
                }
            }
        }

        string _description = "Standard";
        [DebuggerNonUserCode]
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

        decimal _baseCost = 0.00m;
        [DebuggerNonUserCode]
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
        [DebuggerNonUserCode]
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
        [DebuggerNonUserCode]
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
        [DebuggerNonUserCode]
        public string UnitType
        {
            get { return _unitType; }
            set
            {
                if (_unitType != value?.Trim())
                {
                    _unitType = value?.Trim();
                    OnPropertyChanged();
                }
            }
        }

        bool _isUpdated = false;
        [DebuggerNonUserCode]
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

        Material _materialToUpdate = null;
        [DebuggerNonUserCode]
        public Material MaterialToUpdate
        {
            get { return _materialToUpdate; }
            set
            {
                if (_materialToUpdate != value)
                {
                    _materialToUpdate = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}