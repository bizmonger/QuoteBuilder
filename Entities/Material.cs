using SQLite;

namespace Entities
{
    public class Material : EntityBase
    {
        string _id = null;
        [PrimaryKey]
        public string Id
        {
            get { return _id; }
            set
            {
                if (_id != value?.Trim())
                {
                    _id = value?.Trim();
                    OnPropertyChanged();
                }
            }
        }

        string _userId = null;
        public string UserId
        {
            get { return _userId; }
            set
            {
                if (_userId != value?.Trim())
                {
                    _userId = value?.Trim();
                    OnPropertyChanged();
                }
            }
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

        string unitType = null;
        public string UnitType
        {
            get { return unitType; }
            set
            {
                if (unitType != value?.Trim())
                {
                    unitType = value?.Trim();
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

        public bool CloudSynced { get; set; }
    }
}