using System.Diagnostics.CodeAnalysis;
using Bizmonger.Patterns; using static Bizmonger.Patterns.MessageBus;
using Mediation;
using Xamarin.Forms;

namespace ManageProfile
{
    [SuppressMessage("Redundancies in Symbol Declarations", "RECS0001:Class is declared partial but has only one part", Justification = "readability")]
    public partial class View : ContentPage
    {
        public View()
        {
            InitializeComponent();

            this.Appearing += (se, ev) => Publish(Messages.REQUEST_PROFILE);
        }
    }
}
