using Bizmonger.Patterns;
using System.Linq;

namespace CreateQuote.SelectServices
{
    public partial class ViewModel
    {
        public DelegateCommand ViewMaterials { get; private set; }
        public DelegateCommand Next { get; private set; }
        public DelegateCommand Add { get; private set; }
        public DelegateCommand Remove { get; private set; }
        void ActivateCommands()
        {
            ViewMaterials = new DelegateCommand(OnViewMaterials, obj => SelectedService != null && Services.Any());
            Next = new DelegateCommand(OnNext, obj => SelectedServices.Any());
            Add = new DelegateCommand(OnAdd, obj => SelectedService != null && Services.Any());
            Remove = new DelegateCommand(OnRemove, obj => PromotedService != null && SelectedServices.Any());
        }
    }
}