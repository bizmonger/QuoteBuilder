using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Entities;
using Entities.Utilities;
using Mediation;
using static Bizmonger.Patterns.MessageBus;
using Servers;

namespace Repositories.Core
{
    public abstract class AbstractServicesDatabase : IDatabase
    {
        public abstract void Read(string id);

        public abstract void Initialize();

        public void OnSave(object entity)
        {
            var service = entity as Service;
            var existingService = ReadFromServiceId(service.Id);

            if (existingService != null)
            {
                RemoveExcessServiceMaterials(service, existingService);

                service.Update(existingService);
                Update(existingService);
            }
            else
            {
                service.Id = service.Id ?? Guid.NewGuid().ToString();
                service.UserId = new ProfileServer().GetProfile().Id;

                Add(service);

                Publish(Messages.SERVICE_ADDED, service);
            }
        }

        void RemoveExcessServiceMaterials(Service modifiedService, Service existingService)
        {
            var currentMaterials = modifiedService.Materials;
            var archivedServiceMaterials = GetServiceMaterials(existingService.Id);

            if (currentMaterials == null || archivedServiceMaterials == null) return;

            var coreMaterialIds = currentMaterials.Select(m => m.Id);
            var serviceMaterialMaterialIds = archivedServiceMaterials.Select(m => m.MaterialId);

            var materialsIdsToRemove = serviceMaterialMaterialIds.Except(coreMaterialIds);

            foreach (var materialId in materialsIdsToRemove)
            {
                var serviceMaterial = archivedServiceMaterials.FirstOrDefault(sm => sm.MaterialId == materialId);
                Publish(Messages.REQUEST_REMOVE_SERVICE_MATERIAL, serviceMaterial.Id);
            }
        }

        IEnumerable<ServiceMaterial> GetServiceMaterials(string serviceId)
        {
            IEnumerable<ServiceMaterial> serviceMaterials = null;
            SubscribeFirstPublication(Messages.REQUEST_SERVICE_MATERIALS_FROM_SERVICE_ID_RESPONSE, obj => serviceMaterials = obj as IEnumerable<ServiceMaterial>);
            Publish(Messages.REQUEST_SERVICE_MATERIALS_FROM_SERVICE_ID, serviceId);

            return serviceMaterials = serviceMaterials ?? new List<ServiceMaterial>();
        }

        public abstract void Read();

        protected abstract IEnumerable<Service> Get(string profileId);

        protected abstract Service ReadFromServiceId(string serviceId);

        protected abstract void Update(Service service);

        protected abstract void Add(Service service);

        protected IEnumerable<Service> GetServicesSkeleton()
        {
            var profile = new ProfileServer().GetProfile();
            return Get(profile.Id).ToList();
        }

        protected void AttachMaterials(IEnumerable<Service> services)
        {
            foreach (var service in services)
            {
                var materials = GetMatrials(service.Id);

                if (materials != null)
                {
                    service.Materials = new ObservableCollection<Material>(materials);
                }
            }
        }

        IEnumerable<Material> GetMatrials(string serviceId)
        {
            IEnumerable<Material> materials = null;
            SubscribeFirstPublication(Messages.REQUEST_SERVICE_MATERIALS_MATERIALS_FROM_SERVICE_ID_RESPONSE, obj => materials = obj as IEnumerable<Material>);
            Publish(Messages.REQUEST_SERVICE_MATERIALS_MATERIALS_FROM_SERVICE_ID, serviceId);
            return materials;
        }
    }
}