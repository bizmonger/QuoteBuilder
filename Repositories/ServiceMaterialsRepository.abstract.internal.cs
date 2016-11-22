using System;
using System.Linq;
using Entities;
using Mediation;
using Servers;
using static Bizmonger.Patterns.MessageBus;
using Repositories.Core;

namespace Repositories
{
    public partial class AbstractServiceMaterialsRepository
    {
        protected void InitializeDatabase()
        {
            Subscribe(Messages.REQUEST_SERVICE_MATERIALS_DATABASE_RESPONSE, obj => _database = obj as AbstractServiceMaterialsDatabase);
            Publish(Messages.REQUEST_SERVICE_MATERIALS_DATABASE);

            _database.Initialize();
        }

        void OnRequestFromMaterialId(object obj) => _database.GetFromMaterialId(obj as string);

        void OnRequestMaterialsFromServiceId(object obj) => _database.GetMaterialsFromServiceId(obj as string);

        void OnRequestServiceMaterialsFromServiceId(object obj) => _database.GetServiceMaterialsFromServiceId(obj as string);

        void OnDeleteServiceMaterial(object obj) => _database.Delete(obj as string);

        void OnSaveServiceMaterialResponse(object obj) =>
            Publish(Messages.REQUEST_SAVE_SERVICE_MATERIAL_RESPONSE, Save(obj));

        bool Save(object entity)
        {
            var serviceMaterial = entity as ServiceMaterial;

            var existingServiceMaterial = _serviceMaterials.
                FirstOrDefault(s => s.ServiceId == serviceMaterial.ServiceId &&
                                    s.MaterialId == serviceMaterial.MaterialId);

            if (existingServiceMaterial == null)
            {
                serviceMaterial.Id = serviceMaterial.Id ?? Guid.NewGuid().ToString();
                serviceMaterial.UserId = new ProfileServer().GetProfile().Id;

                _serviceMaterials.Add(serviceMaterial);
            }
            else
            {
                Update(serviceMaterial, existingServiceMaterial);
            }

            _database.OnSave(serviceMaterial);

            return true;
        }

        void Update(ServiceMaterial serviceMaterial, ServiceMaterial result)
        {
            result.MaterialId = serviceMaterial.MaterialId;
            result.ServiceId = serviceMaterial.ServiceId;
            result.UserId = serviceMaterial.UserId;
            result.Id = serviceMaterial.Id;
            result.Quantity = serviceMaterial.Quantity;
        }
    }
}