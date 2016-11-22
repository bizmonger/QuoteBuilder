using System.Diagnostics;
using UILogic;

namespace AddMaterial
{
    public sealed partial class ViewModel : ViewModelBase
    {
        public ViewModel()
        {
            MakePromises();
            ActivateCommands();
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
                    Save.RaiseCanExecuteChanged();
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
                    Save.RaiseCanExecuteChanged();
                }
            }
        }

        string _baseCost = null;
        [DebuggerNonUserCode]
        public string BaseCost
        {
            get { return _baseCost; }
            set
            {
                if (_baseCost != value)
                {
                    _baseCost = value;
                    OnPropertyChanged();
                    Save.RaiseCanExecuteChanged();
                }
            }
        }

        string _markupPrice = null;
        [DebuggerNonUserCode]
        public string MarkupPrice
        {
            get { return _markupPrice; }
            set
            {
                if (_markupPrice != value)
                {
                    _markupPrice = value;
                    OnPropertyChanged();
                    Save.RaiseCanExecuteChanged();
                }
            }
        }

        string _quantity = "1";
        [DebuggerNonUserCode]
        public string Quantity
        {
            get { return _quantity; }
            set
            {
                if (_quantity != value)
                {
                    _quantity = value;
                    OnPropertyChanged();
                    Save.RaiseCanExecuteChanged();
                }
            }
        }
        string _unitType = "units";
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
                    Save.RaiseCanExecuteChanged();
                }
            }
        }
        [DebuggerNonUserCode]
        public bool IsSaved { get; set; }
    }
}