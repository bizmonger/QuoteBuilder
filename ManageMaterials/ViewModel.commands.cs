using System.Windows.Input;
using Bizmonger.Patterns; using static Bizmonger.Patterns.MessageBus;
using Entities;
using Mediation;

namespace ManageMaterials
{
    public partial class ViewModel
    {
        public ICommand Load { get; private set; }
        public ICommand New { get; private set; }
        public ICommand Edit { get; private set; }
        public ICommand Finish { get; private set; }
        public ICommand Remove { get; private set; }

        void ActivateCommands()
        {
            Load = new DelegateCommand(obj => Publish(Messages.REQUEST_MATERIALS));
            New = new DelegateCommand(obj => Publish(Messages.REQUEST_VIEW_NEW_MATERIAL));
            Edit = new DelegateCommand(obj => Publish(Messages.REQUEST_VIEW_EDIT_MATERIAL, SelectedMaterialFromCache));
            Finish = new DelegateCommand(obj => Publish(Messages.REQUEST_PREVIOUS_VIEW));
            Remove = new DelegateCommand(obj => Materials.Remove(obj as Material));
        }
    }
}