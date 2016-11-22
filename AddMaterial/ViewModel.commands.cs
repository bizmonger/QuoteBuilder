using Bizmonger.Patterns;
using static Bizmonger.Patterns.MessageBus;
using Mediation;
using System.Diagnostics;

namespace AddMaterial
{
    public partial class ViewModel
    {
        [DebuggerNonUserCode]
        public DelegateCommand Cancel { get; private set; }

        [DebuggerNonUserCode]
        public DelegateCommand Save { get; private set; }

        void ActivateCommands()
        {
            Cancel = new DelegateCommand(obj => Publish(Messages.REQUEST_PREVIOUS_VIEW));
            Save = new DelegateCommand(OnSave, CanSave);
        }
    }
}