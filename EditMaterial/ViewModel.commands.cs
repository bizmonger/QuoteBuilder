using Bizmonger.Patterns; using static Bizmonger.Patterns.MessageBus;
using System.Windows.Input;

namespace EditMaterial
{
    public partial class ViewModel
    {
        public ICommand Cancel { get; set; }
        public ICommand Save { get; set; }
        void ActivateCommands()
        {
            Cancel = new DelegateCommand(OnCancel);
            Save = new DelegateCommand(OnUpdate);
        }
    }
}