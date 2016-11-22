using System;
using System.Collections.Generic;
using System.Linq;
using Entities;
using Mediation;
using Servers;
using static Bizmonger.Patterns.MessageBus;

namespace Repositories.Core
{
    public abstract class AbstractServiceMaterialsDatabase : IDatabase
    {
        public void GetFromMaterialId(string materialId)
        {
            var serviceMaterial = ReadByMaterialId(materialId);
            if (serviceMaterial == null) return;

            PublishMaterial(serviceMaterial.MaterialId);
        }

        public void GetMaterialsFromServiceId(string serviceId)
        {
            var serviceMaterials = ReadByServiceId(serviceId);
            PublishMaterials(serviceMaterials);
        }

        public void GetServiceMaterialsFromServiceId(string serviceId)
        {
            var serviceMaterials = ReadByServiceId(serviceId);
            Publish(Messages.REQUEST_SERVICE_MATERIALS_FROM_SERVICE_ID_RESPONSE, serviceMaterials);
        }

        public abstract void Read(string id);

        public abstract void Initialize();
        public void OnSave(object entity)
        {
            var serviceMaterial = entity as ServiceMaterial;
            var existingServiceMaterial = Read(serviceMaterial);

            if (existingServiceMaterial != null)
            {
                Update(existingServiceMaterial);
            }
            else
            {
                serviceMaterial.Id = serviceMaterial.Id ?? Guid.NewGuid().ToString();
                serviceMaterial.UserId = new ProfileServer().GetProfile().Id;

                Add(serviceMaterial);

                Publish(Messages.SERVICE_MATERIAL_ADDED, serviceMaterial);
            }
        }

        public abstract void Read();

        public abstract void Delete(string id);

        protected abstract IEnumerable<ServiceMaterial> ReadByServiceId(string serviceId);
        protected abstract ServiceMaterial ReadByMaterialId(string materialId);

        protected abstract void Add(ServiceMaterial serviceMaterial);

        protected abstract void Update(ServiceMaterial serviceMaterial);

        protected abstract ServiceMaterial Read(ServiceMaterial serviceMaterial);

        protected void PublishMaterial(string materialId)
        {
            if (string.IsNullOrEmpty(materialId)) return;

            var material = materialId.ToMaterial();
            Publish(Messages.REQUEST_SERVICE_MATERIAL_RESPONSE, material);
        }

        protected void PublishMaterials(IEnumerable<ServiceMaterial> serviceMaterials)
        {
            if (serviceMaterials == null) return;

            var materialIds = serviceMaterials.Select(sm => sm.MaterialId);
            foreach (var materialId in materialIds)
            {
                Publish(Messages.REQUEST_SERVICE_MATERIALS_MATERIALS_FROM_SERVICE_ID_RESPONSE, new List<Material>() { materialId.ToMaterial() });
            }
        }
    }
}