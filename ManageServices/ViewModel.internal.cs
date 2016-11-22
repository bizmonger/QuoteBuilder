using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Entities;
using Mediation;
using static Bizmonger.Patterns.MessageBus;

namespace ManageServices
{
    public partial class ViewModel
    {
        protected override void MakePromises()
        {
            Subscribe(Messages.REQUEST_SERVICES_RESPONSE, OnServicesLoaded);
            Subscribe(Messages.SERVICE_ADDED, OnServiceAdded);
            Subscribe(Messages.MATERIAL_ADDED, OnMaterialAdded);
        }

        protected override void BreakPromises()
        {
            Unsubscribe(Messages.REQUEST_SERVICES_RESPONSE, OnServicesLoaded);
            Unsubscribe(Messages.SERVICE_ADDED, OnServiceAdded);
            Unsubscribe(Messages.MATERIAL_ADDED, OnMaterialAdded);
        }

        void SendRequests() => Publish(Messages.REQUEST_SERVICES);

        void OnServicesLoaded(object obj) => Services = new ObservableCollection<Service>((obj as IEnumerable<Service>).OrderBy(s => s.Name));

        void OnServiceAdded(object obj)
        {
            var service = obj as Service;
            Services.Add(service);
            Services = new ObservableCollection<Service>(Services.OrderBy(s => s.Name));
        }

        void OnMaterialAdded(object obj)
        {
            var isOnlyService = Services.Count == 1;
            if (isOnlyService) { SelectedService = Services.Single(); }

            Services = new ObservableCollection<Service>(Services.OrderBy(s => s.Name));
        }
    }
}