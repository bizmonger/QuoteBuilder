using Bizmonger.Patterns; using static Bizmonger.Patterns.MessageBus;
using Mediation;

namespace ManageProfile
{
    public partial class ViewModel
    {
        public DelegateCommand Save { get; private set; }
        public DelegateCommand Cancel { get; private set; }
        void ActivateCommands()
        {
            Save = new DelegateCommand(OnSave, OnCanSave);
            Cancel = new DelegateCommand(obj => Publish(Messages.REQUEST_PREVIOUS_VIEW));
        }
    }
}