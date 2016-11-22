using Bizmonger.Patterns;
using Mediation;
using static Bizmonger.Patterns.MessageBus;

namespace ManageServiceMaterials
{
    public partial class ViewModel
    {
        public DelegateCommand Add { get; set; }
        public DelegateCommand Edit { get; set; }
        public DelegateCommand New { get; set; }
        public DelegateCommand Remove { get; set; }
        public DelegateCommand Continue { get; set; }
        public DelegateCommand Cancel { get; set; }

        void ActivateCommands()
        {
            Add = new DelegateCommand(OnAddToSelection, obj => SelectedMaterialFromCache != null);
            New = new DelegateCommand(OnNewMaterial);
            Edit = new DelegateCommand(OnEdit, obj => SelectedAssignedMaterial != null);
            Remove = new DelegateCommand(OnRemove, obj => SelectedAssignedMaterial != null);
            Continue = new DelegateCommand(OnSaveMaterials, obj => IsDirty);
            Cancel = new DelegateCommand(obj => Publish(Messages.REQUEST_PREVIOUS_VIEW));
        }
    }
}