using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Entities;
using Entities.Utilities;
using Mediation;
using Repositories.Core;
using static Bizmonger.Patterns.MessageBus;

namespace TestAPI
{
    [DebuggerNonUserCode]
    public class MockServicesDatabase : AbstractServicesDatabase
    {
        public List<Service> Services { get; set; } = new List<Service>();

        void MakePromises() =>
            Subscribe(Messages.REQUEST_SAVE_SERVICE, OnSave);

        void BreakPromises() =>
            Unsubscribe(Messages.REQUEST_SAVE_SERVICE, OnSave);

        public override void Read()
        {
            IEnumerable<Service> services = GetServicesSkeleton();

            AttachMaterials(services);
            Publish(Messages.REQUEST_SERVICES_RESPONSE, services);
        }

        public override void Read(string id)
        {
            var service = Services.FirstOrDefault(m => m.Id == id);
            Publish(Messages.REQUEST_SERVICE_RESPONSE, service);
        }

        public override void Initialize() { }

        protected override IEnumerable<Service> Get(string profileId) =>
            Services.Where(s => s.UserId == profileId).ToList();

        protected override Service ReadFromServiceId(string serviceId) =>
            Services.FirstOrDefault(s => s.Id == serviceId);

        protected override void Update(Service service)
        {
            var existingService = Services.FirstOrDefault(s => s.Id == service.Id);
            service.Update(existingService);
        }

        protected override void Add(Service service) => Services.Add(service);
    }
}