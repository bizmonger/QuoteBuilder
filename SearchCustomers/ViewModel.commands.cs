using Bizmonger.Patterns;
using static Bizmonger.Patterns.MessageBus;
using Mediation;
using System.Windows.Input;

namespace SearchCustomers
{
    public partial class ViewModel
    {
        public DelegateCommand Load { get; private set; }
        public DelegateCommand Search { get; private set; }
        public DelegateCommand View { get; private set; }
        void ActivateCommands()
        {
            Load = new DelegateCommand(obj => Publish(Messages.REQUEST_CUSTOMERS));
            Search = new DelegateCommand(obj => Publish(Messages.REQUEST_CUSTOMER, obj));
            View = new DelegateCommand(obj => Publish(Messages.REQUEST_VIEW_QUOTE, SelectedCustomer), obj => SelectedCustomer != null);
        }
    }
}