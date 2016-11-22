using System.Collections.Generic;
using System.Linq;
using Entities;
using Mediation;
using SQLite;
using Xamarin.Forms;
using static Bizmonger.Patterns.MessageBus;
using Repositories.Core;
using Servers;

namespace Repositories.Details
{
    public class ServiceMaterialsDatabase : AbstractServiceMaterialsDatabase, IDatabase
    {
        SQLiteConnection _databaseConnection = null;

        public override void Initialize()
        {
            _databaseConnection = DependencyService.Get<IDatabaseConnection>().Connect();

            var tableExists = DependencyService.Get<IDatabaseConnection>().TableExists(_databaseConnection, "ServiceMaterial");

            if (!tableExists)
            {
                _databaseConnection.CreateTable<ServiceMaterial>();
            }
        }

        protected override void Add(ServiceMaterial serviceMaterial) =>
            _databaseConnection.Insert(serviceMaterial);

        protected override void Update(ServiceMaterial serviceMaterial) =>
            _databaseConnection.Update(serviceMaterial);

        protected override ServiceMaterial Read(ServiceMaterial serviceMaterial) =>
            _databaseConnection.Table<ServiceMaterial>().FirstOrDefault(
                sm => sm.MaterialId == serviceMaterial.MaterialId &&
                sm.ServiceId == serviceMaterial.ServiceId);

        public override void Read()
        {
            var profileId = new ProfileServer().GetProfile()?.Id;
            if (profileId == null) return;

            var serviceMaterials = _databaseConnection.Table<ServiceMaterial>().Where(m => m.UserId == profileId);

            Publish(Messages.REQUEST_SERVICE_MATERIALS_MATERIALS_FROM_SERVICE_ID_RESPONSE, serviceMaterials);
        }

        public override void Read(string id)
        {
            var material = _databaseConnection.Table<Material>().FirstOrDefault(m => m.Id == id);
            Publish(Messages.REQUEST_SERVICE_MATERIAL_RESPONSE, material);
        }

        protected override ServiceMaterial ReadByMaterialId(string materialId) =>
            _databaseConnection.Table<ServiceMaterial>().FirstOrDefault(m => m.MaterialId == materialId);

        protected override IEnumerable<ServiceMaterial> ReadByServiceId(string serviceId) =>
            _databaseConnection.Table<ServiceMaterial>().Where(m => m.ServiceId == serviceId);

        public override void Delete(string id) =>
            _databaseConnection.Table<ServiceMaterial>().Delete(sm => sm.Id == id);
    }
}