using System.Windows.Input;
using Bizmonger.Patterns; using static Bizmonger.Patterns.MessageBus;
using Mediation;

namespace CalculateTransaction
{
    public partial class ViewModel
    {
        public ICommand AddService { get; private set; }
        public ICommand EditService { get; private set; }
        public ICommand RemoveService { get; private set; }

        void ActivateCommands()
        {
            AddService = new DelegateCommand(OnAddService);
            EditService = new DelegateCommand(obj => Publish(Messages.REQUEST_VIEW_EDIT_SERVICE));
            RemoveService = new DelegateCommand(OnRemoveService);
        }
    }
}