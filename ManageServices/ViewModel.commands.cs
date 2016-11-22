using Bizmonger.Patterns;
using Entities;
using Mediation;
using static Bizmonger.Patterns.MessageBus;

namespace ManageServices
{
    public partial class ViewModel
    {
        public DelegateCommand Load { get; private set; }
        public DelegateCommand New { get; private set; }
        public DelegateCommand Finish { get; private set; }
        public DelegateCommand Remove { get; private set; }
        public DelegateCommand Edit { get; private set; }

        void ActivateCommands()
        {
            Load = new DelegateCommand(obj => Publish(Messages.REQUEST_SERVICES));
            New = new DelegateCommand(obj => Publish(Messages.REQUEST_VIEW_NEW_SERVICE));
            Finish = new DelegateCommand(obj => Publish(Messages.REQUEST_PREVIOUS_VIEW));
            Remove = new DelegateCommand(obj => Services.Remove(obj as Service), obj => SelectedService != null);
            Edit = new DelegateCommand(obj =>
                {
                    Publish(Messages.REQUEST_VIEW_EDIT_SERVICE, SelectedService);
                }, obj => SelectedService != null);
        }
    }
}