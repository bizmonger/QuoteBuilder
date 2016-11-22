using System.Collections.ObjectModel;
using Entities;
using UILogic;
using System.Diagnostics;

namespace ManageMaterials
{
    public sealed partial class ViewModel : ViewModelBase
    {
        public ViewModel()
        {
            MakePromises();
            ActivateCommands();
            SendRequests();
        }

        ObservableCollection<Material> _materials = null;
        [DebuggerNonUserCode]
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

        Material _selectedMaterialFromCache = null;
        [DebuggerNonUserCode]
        public Material SelectedMaterialFromCache
        {
            get { return _selectedMaterialFromCache; }
            set
            {
                if (_selectedMaterialFromCache != value)
                {
                    _selectedMaterialFromCache = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}