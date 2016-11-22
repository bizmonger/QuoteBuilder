using System.Collections.Generic;
using System.Linq;
using Entities;
using Mediation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repositories.Core;
using Repositories.Details;
using TestAPI;
using static Bizmonger.Patterns.MessageBus;
using static TestAPI.Gimme;
using Servers;

namespace Repositories.Tests
{
    [TestClass]
    public class _Repositories
    {
        [TestInitialize]
        public void TestSetup() => ClearSubscriptions();

        [TestMethod]
        public void save_service_with_material()
        {
            // Setup
            var mock = new Mock();
            mock.PrepareProfileDB();
            mock.PrepareMaterialsDB();
            mock.PrepareQuotesDB();
            mock.PrepareCustomersDB();

            var servicesDatabase = mock.PrepareServicesDB();
            var serviceMaterialsDatabase = mock.PrepareServiceMaterialsDB();
            new Autonomy().Activate();

            // Test
            Publish(Messages.REQUEST_SAVE_SERVICE, Mocks.SERVICE_1);

            // Verify
            var serviceSaved = servicesDatabase.Services.Single() == Mocks.SERVICE_1;
            var serviceMaterialsSaved = serviceMaterialsDatabase.ServiceMaterials.Single().ServiceId == Mocks.SERVICE_1.Id &&
                                        serviceMaterialsDatabase.ServiceMaterials.Single().MaterialId == Mocks.SERVICE_1.Materials.Single().Id;
            var expected = serviceSaved &&
                           serviceMaterialsSaved;

            Assert.IsTrue(expected);
        }

        [TestMethod]
        public void read_service_materials()
        {
            // Setup
            ClearSubscriptions();
            var mock = new Mock();
            var serviceMaterialsDatabase = mock.PrepareServiceMaterialsDependencies();
            new Autonomy().Activate();

            // Test
            var service = Mocks.SERVICE_1;
            Publish(Messages.REQUEST_SAVE_SERVICE, service);

            IEnumerable<Material> actualMaterials = null;
            Subscribe(Messages.REQUEST_SERVICE_MATERIALS_MATERIALS_FROM_SERVICE_ID_RESPONSE, obj => actualMaterials = obj as IEnumerable<Material>);
            Publish(Messages.REQUEST_SERVICE_MATERIALS_MATERIALS_FROM_SERVICE_ID, service.Id);

            // Verify
            var expected = actualMaterials.Single() == service.Materials.Single();

            Assert.IsTrue(expected);
        }

        [TestMethod]
        public void get_material_from_service_material()
        {
            // Setup
            ClearSubscriptions();

            var mock = new Mock();
            var serviceMaterialsDatabase = mock.PrepareServiceMaterialsDependencies();
            new Autonomy().Activate();

            var profileId = new ProfileServer().GetProfile().Id;

            var material = new Material() { Name = SOME_TEXT, UserId = profileId };
            Publish(Messages.REQUEST_SAVE_MATERIAL, material);

            var service = new Service() { Name = SOME_TEXT, UserId = profileId };
            Publish(Messages.REQUEST_SAVE_SERVICE, service);

            var serviceMaterial = new ServiceMaterial()
            {
                MaterialId = material.Id,
                ServiceId = service.Id,
                Quantity = 1,
                UserId = profileId
            };

            Publish(Messages.REQUEST_SAVE_SERVICE_MATERIAL, serviceMaterial);

            // Test
            Material materialResult = null;
            Subscribe(Messages.REQUEST_SERVICE_MATERIAL_RESPONSE, obj => materialResult = obj as Material);
            Publish(Messages.REQUEST_SERVICE_MATERIAL, material.Id);

            // Verify
            var expected = material == materialResult;
            Assert.IsTrue(expected);
        }

        [TestMethod]
        public void saving_service_material_inserts_item_in_materials_repository()
        {
            // Setup
            ClearSubscriptions();
            var mock = new Mock();
            var materialsDatabase = mock.PrepareMaterialsPromiseDependencies();
            new Autonomy().Activate();

            // Test
            Publish(Messages.REQUEST_SAVE_SERVICE, Mocks.SERVICE_1);

            // Verify
            var materialSaved = materialsDatabase.Materials.Single() == Mocks.SERVICE_1.Materials.Single();
            Assert.IsTrue(materialSaved);
        }
    }
}