using Entities;
using System.Collections.ObjectModel;
using UILogic;

namespace ManageServiceMaterials
{
    public sealed partial class ViewModel : ViewModelBase
    {
        public ViewModel()
        {
            MakePromises();
            ActivateCommands();
            SendRequests();
        }

        Service _service = null;
        public Service Service
        {
            get { return _service; }
            set
            {
                if (_service != value)
                {
                    _service = value;
                    OnPropertyChanged();
                    UpdateState();
                }
            }
        }

        Material _selectedMaterialFromCache = null;
        public Material SelectedMaterialFromCache
        {
            get { return _selectedMaterialFromCache; }
            set
            {
                if (_selectedMaterialFromCache != value)
                {
                    _selectedMaterialFromCache = value;
                    OnPropertyChanged();

                    if (_selectedMaterialFromCache != null) SelectedAssignedMaterial = null;

                    UpdateState();
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
                    UpdateState();
                }
            }
        }

        ObservableCollection<Material> _assignedMaterials = new ObservableCollection<Material>();
        public ObservableCollection<Material> AssignedMaterials
        {
            get { return _assignedMaterials; }
            set
            {
                if (_assignedMaterials != value)
                {
                    _assignedMaterials = value;
                    OnPropertyChanged();
                    UpdateState();
                }
            }
        }

        Material _selectedAssignedMaterial = null;
        public Material SelectedAssignedMaterial
        {
            get { return _selectedAssignedMaterial; }
            set
            {
                if (_selectedAssignedMaterial != value)
                {
                    _selectedAssignedMaterial = value;
                    OnPropertyChanged();

                    if (_selectedAssignedMaterial != null) SelectedMaterialFromCache = null;

                    UpdateState();
                }
            }
        }

        public bool IsDirty = false;
    }
}