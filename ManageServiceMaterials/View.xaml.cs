using System.Diagnostics.CodeAnalysis;
using Entities;
using Mediation;
using Xamarin.Forms;
using static Bizmonger.Patterns.MessageBus;

namespace ManageServiceMaterials
{
    [SuppressMessage("Redundancies in Symbol Declarations", "RECS0001:Class is declared partial but has only one part", Justification = "readability")]
    public partial class View : ContentPage
    {
        ViewModel _viewModel = null;
        public View()
        {
            InitializeComponent();

            _viewModel = BindingContext as ViewModel;

            Appearing += (se, ev) =>
                {
                    SubscribeFirstPublication(Messages.REQUEST_SELECTED_SERVICE_RESPONSE, obj =>
                        {
                            var service = obj as Service;
                            if (service != null) Title = $" {service.Name} ( materials )";
                        });

                    _viewModel.Refresh();
                };
        }
    }
}