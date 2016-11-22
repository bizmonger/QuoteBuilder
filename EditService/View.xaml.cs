using static Bizmonger.Patterns.MessageBus;
using Entities;
using Mediation;
using Xamarin.Forms;

namespace EditService
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Redundancies in Symbol Declarations", "RECS0001:Class is declared partial but has only one part", Justification = "<Pending>")]
    public partial class View : ContentPage
    {
        public View()
        {
            InitializeComponent();

            Subscribe(Messages.REQUEST_SELECTED_SERVICE_RESPONSE, obj =>
                Title = $" Edit ({(obj as Service)?.Name})");
        }
    }
}