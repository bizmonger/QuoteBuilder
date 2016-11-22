using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Entities;
using Mediation;
using UILogic;
using Xamarin.Forms;
using static Bizmonger.Patterns.MessageBus;

namespace QuoteBuilder
{
    public class ViewFactory
    {

        ViewModelBase _currentViewModel = null;
        Page _mainPage = null;

        [SuppressMessage("Redundancies in Code", "RECS0070:Redundant explicit argument name specification", Justification = "readability")]
        public void Promise(Page mainPage)
        {
            _mainPage = mainPage;

            Subscribe(Messages.REQUEST_PREVIOUS_VIEW, async obj =>
                {
                    _currentViewModel?.Disable();
                    await mainPage.Navigation.PopAsync(animated: true);
                    _currentViewModel = mainPage.BindingContext as ViewModelBase;
                });

            Subscribe(Messages.REQUEST_VIEW_VIEW_MENU, async obj =>
                {
                    _currentViewModel?.Disable();
                    await mainPage.Navigation.PopToRootAsync(animated: true);
                    _currentViewModel = mainPage.BindingContext as ViewModelBase;
                });

            Subscribe(Messages.REQUEST_VIEW_CUSTOMER_INFORMATION, async obj =>
                {
                    var page = new CreateQuote.EnterCustomer.View();
                    var viewModel = page.BindingContext as CreateQuote.EnterCustomer.ViewModel;

                    viewModel.SelectedServices = obj as IEnumerable<Service>;
                    viewModel.Load.Execute(null);

                    await Activate(page, breakExistingPromises: true);
                });

            Subscribe(Messages.REQUEST_VIEW_NEW_QUOTE, async obj =>
                {
                    var page = new CreateQuote.SelectServices.View();
                    await Activate(page, breakExistingPromises: true);

                    var viewModel = page.BindingContext as CreateQuote.SelectServices.ViewModel;
                    if (!viewModel.Services.Any())
                    {
                        var AddServicePage = new AddService.View();
                        await Activate(AddServicePage, breakExistingPromises: false);
                    }
                });

            Subscribe(Messages.REQUEST_VIEW_QUOTE, async obj =>
                {
                    await Activate(new ViewQuote.View(), breakExistingPromises: false);
                });

            Subscribe(Messages.REQUEST_VIEW_CUSTOMERS,
                async obj => await Activate(new SearchCustomers.View(), breakExistingPromises: true));

            Subscribe(Messages.REQUEST_VIEW_SERVICES,
                async obj =>
                {
                    var page = new ManageServices.View();
                    await Activate(page, breakExistingPromises: true);

                    var viewModel = page.BindingContext as ManageServices.ViewModel;
                    if (!viewModel.Services.Any())
                    {
                        var addServicePage = new AddService.View();
                        await Activate(addServicePage, breakExistingPromises: false);
                    }
                });

            Subscribe(Messages.REQUEST_VIEW_EDIT_SERVICE,
                async obj =>
                    {
                        await Activate(new EditService.View(), breakExistingPromises: true);
                        Publish(Messages.REQUEST_SELECTED_SERVICE_RESPONSE, obj);
                    });

            Subscribe(Messages.REQUEST_VIEW_EDIT_MATERIAL, async obj =>
                {
                    await Activate(new EditMaterial.View(), breakExistingPromises: false);
                    Publish(Messages.REQUEST_SELECTED_MATERIAL_RESPONSE, obj);
                });

            Subscribe(Messages.REQUEST_VIEW_NEW_MATERIAL,
                async obj => await Activate(new AddMaterial.View(), breakExistingPromises: true));

            Subscribe(Messages.REQUEST_VIEW_MATERIALS, async obj =>
                {
                    var page = new ManageMaterials.View();
                    await Activate(page, breakExistingPromises: true);

                    var viewModel = page.BindingContext as ManageMaterials.ViewModel;
                    if (!viewModel.Materials.Any())
                    {
                        var addMaterialPage = new AddMaterial.View();
                        await Activate(addMaterialPage, breakExistingPromises: false);
                    }
                });

            Subscribe(Messages.REQUEST_VIEW_PROFILE,
                async obj => await Activate(new ManageProfile.View(), breakExistingPromises: true));

            Subscribe(Messages.REQUEST_VIEW_SERVICE_MATERIALS, async obj =>
                    {
                        var view = new ManageServiceMaterials.View();
                        var viewModel = view.BindingContext as ManageServiceMaterials.ViewModel;
                        viewModel.Service = obj as Service;
                        await Activate(view, breakExistingPromises: false);
                        Publish(Messages.REQUEST_SELECTED_SERVICE_RESPONSE, viewModel.Service);
                    });

            Subscribe(Messages.REQUEST_VIEW_NEW_SERVICE,
                async obj => await Activate(new AddService.View(), breakExistingPromises: true));

        }

        [SuppressMessage("Redundancies in Code", "RECS0070:Redundant explicit argument name specification", Justification = "readability")]
        async Task Activate(Page page, bool breakExistingPromises)
        {
            if (breakExistingPromises)
            {
                _currentViewModel?.Disable();
            }

            await _mainPage.Navigation.PushAsync(page, animated: true);

            _currentViewModel = page.BindingContext as ViewModelBase;
        }
    }
}
