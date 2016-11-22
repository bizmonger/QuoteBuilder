using System.Collections.Generic;
using System.Linq;
using Entities;
using Mediation;
using Repositories.Core;
using static Bizmonger.Patterns.MessageBus;
using System.Diagnostics;

namespace TestAPI
{
    [DebuggerNonUserCode]
    public class MockServiceMaterialsDatabase : AbstractServiceMaterialsDatabase, IDatabase
    {
        public List<ServiceMaterial> ServiceMaterials { get; set; } = new List<ServiceMaterial>();

        public override void Read() =>
            Publish(Messages.REQUEST_SERVICE_MATERIALS_MATERIALS_FROM_SERVICE_ID_RESPONSE, ServiceMaterials);

        protected override ServiceMaterial ReadByMaterialId(string materialId) =>
            ServiceMaterials.FirstOrDefault(sm => sm.MaterialId == materialId);

        protected override IEnumerable<ServiceMaterial> ReadByServiceId(string serviceId) =>
             ServiceMaterials.Where(sm => sm.ServiceId == serviceId);

        public override void Initialize() { }

        public override void Read(string id)
        {
            var serviceMaterial = ServiceMaterials.FirstOrDefault(sm => sm.Id == id);
            Publish(Messages.REQUEST_SERVICE_MATERIAL_RESPONSE, serviceMaterial);
        }

        protected override void Add(ServiceMaterial serviceMaterial) =>
            ServiceMaterials.Add(serviceMaterial);

        protected override void Update(ServiceMaterial serviceMaterial)
        {
            var existingMaterial = ServiceMaterials.FirstOrDefault(sm => sm.ServiceId == serviceMaterial.ServiceId &&
                                                                         sm.MaterialId == serviceMaterial.MaterialId);

            existingMaterial.ServiceId = serviceMaterial.ServiceId;
            existingMaterial.MaterialId = serviceMaterial.MaterialId;
            existingMaterial.Quantity = serviceMaterial.Quantity;
            existingMaterial.UserId = serviceMaterial.UserId;
        }

        protected override ServiceMaterial Read(ServiceMaterial serviceMaterial) =>
            ServiceMaterials.FirstOrDefault(sm => sm.ServiceId == serviceMaterial.ServiceId &&
                                                  sm.MaterialId == serviceMaterial.MaterialId);

        public override void Delete(string id)
        {
            var existing = ServiceMaterials.FirstOrDefault(sm => sm.Id == id);

            if (existing != null)
            {
                ServiceMaterials.Remove(existing);
            }
        }
    }
}