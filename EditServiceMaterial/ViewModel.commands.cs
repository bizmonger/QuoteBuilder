using Bizmonger.Patterns;
using Mediation;
using System.Windows.Input;

namespace EditServiceMaterial
{
    public partial class ViewModel
    {
        public ICommand Update { get; private set; }
        public ICommand Cancel { get; private set; }

        void ActivateCommands()
        {
            Update = new DelegateCommand(obj => _messagebus.Publish(Messages.REQUEST_SAVE_MATERIAL, _materialToUpdate));
            Cancel = new DelegateCommand(OnCancel);
        }
    }
}