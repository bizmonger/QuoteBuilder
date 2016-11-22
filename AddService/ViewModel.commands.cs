using System.Windows.Input;
using Bizmonger.Patterns;
using Mediation;
using static Bizmonger.Patterns.MessageBus;

namespace AddService
{
    public partial class ViewModel
    {
        public ICommand Cancel { get; set; }
        public DelegateCommand SaveCommand { get; set; }
        public ICommand ViewMaterials { get; set; }
        void ActivateCommands()
        {
            SaveCommand = new DelegateCommand(OnSave, OnCanSave);
            Cancel = new DelegateCommand(obj => Publish(Messages.REQUEST_PREVIOUS_VIEW));
            ViewMaterials = new DelegateCommand(OnViewMaterials);
        }
    }
}