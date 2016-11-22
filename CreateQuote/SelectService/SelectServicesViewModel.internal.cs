using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Entities;
using Entities.Utilities;
using Mediation;
using static Bizmonger.Patterns.MessageBus;

namespace CreateQuote.SelectServices
{
    public partial class ViewModel
    {
        const string STATEMENT_TYPE_INVOICE = "Invoice";
        const string LOCAL_SETTINGS_INVOICE_NUMBER_KEY = "InvoiceNumber";
        const string LOCAL_SETTINGS_QUOTE_NUMBER_KEY = "QuoteNumber";

        Quote _quote = null;

        protected override void MakePromises()
        {
            Subscribe(Messages.REQUEST_SERVICES_RESPONSE, OnRequestServicesResponse);
            Subscribe(Messages.REQUEST_SELECTED_SERVICE, OnSelectedServiceResponse);
            Subscribe(Messages.SERVICE_ADDED, OnServiceAdded);
            Subscribe(Messages.REQUEST_QUOTE_RESPONSE, OnRequestQuoteResponse);
        }

        protected override void BreakPromises()
        {
            Unsubscribe(Messages.REQUEST_SERVICES_RESPONSE, OnRequestServicesResponse);
            Unsubscribe(Messages.REQUEST_SELECTED_SERVICE, OnSelectedServiceResponse);
            Unsubscribe(Messages.SERVICE_ADDED, OnServiceAdded);
            Unsubscribe(Messages.REQUEST_QUOTE_RESPONSE, OnRequestQuoteResponse);
        }

        void OnRequestQuoteResponse(object obj) => _quote = obj as Quote;

        void SendRequests() => Publish(Messages.REQUEST_SERVICES);

        void OnAdd(object obj)
        {
            SelectedServices.Add(SelectedService);
            SelectedServices = new ObservableCollection<Service>(SelectedServices.OrderByDescending(s => s.TotalCost()));

            Services.Remove(SelectedService);
            Services = new ObservableCollection<Service>(Services.OrderBy(s => s.Name));

            PromotedService = SelectedService;

            UpdateState();
        }

        void OnRemove(object obj)
        {
            SelectedServices.Remove(PromotedService);
            SelectedServices = new ObservableCollection<Service>(SelectedServices.OrderByDescending(s => s.TotalCost()));

            Services.Add(PromotedService);
            Services = new ObservableCollection<Service>(Services.OrderBy(s => s.Name));

            PromotedService = null;

            UpdateState();
        }

        void OnServiceAdded(object obj)
        {
            var service = obj as Service;
            SelectedServices.Add(service);
            SelectedServices = new ObservableCollection<Service>(SelectedServices.OrderByDescending(s => s.TotalCost()));

            UpdateState();
        }

        void OnSelectedServiceResponse(object obj)
        {
            if (SelectedService != null)
            {
                Publish(Messages.REQUEST_SELECTED_SERVICE_RESPONSE, SelectedService);
            }
        }

        void OnRequestServicesResponse(object obj) =>
            Services = new ObservableCollection<Service>((obj as IEnumerable<Service>).OrderBy(s => s.Name));

        void OnViewMaterials(object obj) =>
            Publish(Messages.REQUEST_VIEW_SERVICE_MATERIALS, SelectedService);

        void OnNext(object obj)
        {
            BreakPromises();
            Publish(Messages.REQUEST_VIEW_CUSTOMER_INFORMATION, SelectedServices);
        }

        void UpdateState()
        {
            ViewMaterials.RaiseCanExecuteChanged();
            Add.RaiseCanExecuteChanged();
            Next.RaiseCanExecuteChanged();
            Remove.RaiseCanExecuteChanged();
        }
    }
}