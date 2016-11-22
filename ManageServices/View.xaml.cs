using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using Entities;
using Xamarin.Forms;

namespace ManageServices
{
    [SuppressMessage("Redundancies in Symbol Declarations", "RECS0001:Class is declared partial but has only one part", Justification = "readability")]
    public partial class View : ContentPage
    {
        ViewModel _viewModel = null;

        public View()
        {
            InitializeComponent();

            this.Appearing += (se, ev) =>
                {
                    _viewModel = BindingContext as ViewModel;
                    _viewModel.Services = _viewModel.Services ?? new ObservableCollection<Service>();
                    _viewModel.Services = new ObservableCollection<Service>(_viewModel.Services);
                };
        }
    }
}