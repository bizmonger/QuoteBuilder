using System;
using System.Linq;
using Entities;
using Entities.Utilities;
using Mediation;
using Mediation.Validation;
using Servers;
using static Bizmonger.Patterns.MessageBus;
using Repositories.Core;

namespace Repositories
{
    public partial class AbstractServicesRepository
    {
        protected void InitializeDatabase()
        {
            Subscribe(Messages.REQUEST_SERVICES_DATABASE_RESPONSE, obj => _database = obj as IDatabase);
            Publish(Messages.REQUEST_SERVICES_DATABASE);

            _database.Initialize();
        }

        void OnRequestServices(object obj) => _database.Read();

        void OnSaveService(object obj) => Publish(Messages.REQUEST_SAVE_SERVICE_RESPONSE, Save(obj));

        void OnRequestService(object obj) => _database.Read(obj as string);

        bool Save(object entity)
        {
            var service = entity as Service;
            var validated = new ServiceValidator().Validate(service);
            if (!validated) return false;

            Service serviceToProcess = null;

            var existingService = _services.FirstOrDefault(s => s.Id == service.Id);

            if (existingService == null)
            {
                var profile = new ProfileServer().GetProfile();

                service.UserId = profile.Id;
                service.Id = service.Id ?? Guid.NewGuid().ToString();

                _services.Add(service);
                serviceToProcess = service;
            }
            else
            {
                service.Update(existingService);
                serviceToProcess = existingService;
            }

            SaveData(serviceToProcess);
            SaveMaterials(serviceToProcess);

            return true;
        }

        void SaveMaterials(Service serviceToProcess)
        {
            foreach (var material in serviceToProcess.Materials)
            {
                Publish(Messages.REQUEST_SAVE_MATERIAL, material);
                Publish(Messages.REQUEST_SAVE_SERVICE_MATERIAL, new ServiceMaterial()
                {
                    ServiceId = serviceToProcess.Id,
                    MaterialId = material.Id,
                    Quantity = material.Quantity
                });
            }
        }

        void OnServiceMaterialAdded(object obj)
        {
            var serviceMaterial = obj as ServiceMaterial;
            var material = serviceMaterial.MaterialId.ToMaterial();
            if (material == null) return;

            var service = GetService(serviceMaterial);
            if (service == null) return;

            var alreadyExists = service.Materials.Any(m => m.Id == material.Id);
            if (!alreadyExists) service.Materials.Add(material);
        }



        Service GetService(ServiceMaterial serviceMaterial)
        {
            Service service = null;

            Subscribe(Messages.REQUEST_SERVICE_RESPONSE, payload => service = payload as Service);
            Publish(Messages.REQUEST_SERVICE, serviceMaterial.ServiceId);
            return service;
        }
    }
}