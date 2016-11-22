using System.Collections.Generic;
using System.Linq;
using Entities;
using Mediation;
using SQLite;
using Xamarin.Forms;
using static Bizmonger.Patterns.MessageBus;
using Repositories.Core;

namespace Repositories.Details
{
    public class ServicesDatabase : AbstractServicesDatabase
    {
        SQLiteConnection _databaseConnection = null;

        public override void Initialize()
        {
            _databaseConnection = DependencyService.Get<IDatabaseConnection>().Connect();

            var tableExists = DependencyService.Get<IDatabaseConnection>().TableExists(_databaseConnection, "Service");

            if (!tableExists)
            {
                _databaseConnection.CreateTable<Service>();
            }
        }



        protected override void Add(Service service) =>
            _databaseConnection.Insert(service);

        protected override void Update(Service service) =>
            _databaseConnection.Update(service);

        protected override Service ReadFromServiceId(string serviceId) =>
            _databaseConnection.Table<Service>().FirstOrDefault(s => s.Id == serviceId);

        public override void Read()
        {
            var services = GetServicesSkeleton();
            AttachMaterials(services);

            Publish(Messages.REQUEST_SERVICES_RESPONSE, services);
        }

        public override void Read(string id)
        {
            var existingService = _databaseConnection.Table<Service>().FirstOrDefault(s => s.Id == id);
            Publish(Messages.REQUEST_SERVICE_RESPONSE, existingService);
        }

        protected override IEnumerable<Service> Get(string profileId) =>
            _databaseConnection.Table<Service>().Where(s => s.UserId == profileId);
    }
}