using Bizmonger.Patterns;

namespace EditService
{
    public partial class ViewModel
    {
        public DelegateCommand Update { get; private set; }
        public DelegateCommand Cancel { get; private set; }
        public DelegateCommand ViewMaterials { get; private set; }
        void ActivateCommands()
        {
            Update = new DelegateCommand(OnUpdate, OnCanSave);
            Cancel = new DelegateCommand(OnCancel);
            ViewMaterials = new DelegateCommand(OnViewMaterials);
        }
    }
}