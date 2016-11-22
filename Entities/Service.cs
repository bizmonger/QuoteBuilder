using SQLite;
using System.Collections.ObjectModel;

namespace Entities
{
    public class Service : EntityBase
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

        ObservableCollection<Material> _materials = new ObservableCollection<Material>();
        [Ignore]
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

        ObservableCollection<ServiceMaterial> _serviceMaterials = new ObservableCollection<ServiceMaterial>();
        [Ignore]
        public ObservableCollection<ServiceMaterial> ServiceMaterials
        {
            get { return _serviceMaterials; }
            set
            {
                if (_serviceMaterials != value)
                {
                    _serviceMaterials = value;
                    OnPropertyChanged();
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
                }
            }
        }

        decimal _taxPercentage = 0.00m;
        public decimal TaxPercentage
        {
            get { return _taxPercentage; }
            set
            {
                if (_taxPercentage != value)
                {
                    _taxPercentage = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool CloudSynced { get; set; }
    }
}