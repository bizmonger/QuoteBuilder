using System.Collections.Generic;
using Entities;
using Mediation;
using SQLite;
using static Bizmonger.Patterns.MessageBus;
using Repositories.Core;
using System;

namespace Repositories
{
    public abstract partial class AbstractServiceMaterialsRepository : AbstractPromise
    {
        protected List<ServiceMaterial> _serviceMaterials = new List<ServiceMaterial>();
        protected SQLiteConnection _databaseConnection = null;
        protected AbstractServiceMaterialsDatabase _database = null;

        protected override void MakePromises()
        {
            Subscribe(Messages.REQUEST_SAVE_SERVICE_MATERIAL, OnSaveServiceMaterialResponse);
            Subscribe(Messages.REQUEST_SERVICE_MATERIALS_MATERIALS_FROM_SERVICE_ID, OnRequestMaterialsFromServiceId);
            Subscribe(Messages.REQUEST_SERVICE_MATERIALS_FROM_SERVICE_ID, OnRequestServiceMaterialsFromServiceId);
            Subscribe(Messages.REQUEST_SERVICE_MATERIAL, OnRequestFromMaterialId);
            Subscribe(Messages.REQUEST_REMOVE_SERVICE_MATERIAL, OnDeleteServiceMaterial);
        }

        protected override void BreakPromises()
        {
            Unsubscribe(Messages.REQUEST_SAVE_SERVICE_MATERIAL, OnSaveServiceMaterialResponse);
            Unsubscribe(Messages.REQUEST_SERVICE_MATERIALS_MATERIALS_FROM_SERVICE_ID, OnRequestMaterialsFromServiceId);
            Unsubscribe(Messages.REQUEST_SERVICE_MATERIALS_FROM_SERVICE_ID, OnRequestServiceMaterialsFromServiceId);
            Unsubscribe(Messages.REQUEST_SERVICE_MATERIAL, OnRequestFromMaterialId);
            Unsubscribe(Messages.REQUEST_REMOVE_SERVICE_MATERIAL, OnDeleteServiceMaterial);
        }

        protected void SendRequests() => Read();

        protected abstract void SaveData(ServiceMaterial serviceMaterial);

        protected abstract void Read();

        protected void UpdatServiceMaterials(ServiceMaterial serviceMaterial)
        {
            Publish(Messages.REQUEST_SAVE_SERVICE_MATERIAL,
                new ServiceMaterial()
                {
                    Id = serviceMaterial.Id,
                    ServiceId = serviceMaterial.ServiceId,
                    MaterialId = serviceMaterial.MaterialId
                });
        }
    }
}