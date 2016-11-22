using System.Linq;
using Mediation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestAPI;
using static Bizmonger.Patterns.MessageBus;
using Repositories.Core;
using Entities;
using System.Diagnostics;

namespace ManageServices.Tests
{
    [DebuggerNonUserCode]
    [TestClass]
    public class _ManageServices
    {
        [TestInitialize]
        public void TestSetup() => ClearSubscriptions();

        [TestMethod]
        public void load_services()
        {
            // Setup
            new Bootstrap().Run();

            Publish(Messages.REQUEST_SAVE_SERVICE, Mocks.SERVICE_1);
            var viewModel = new ViewModel();

            // Test
            viewModel.Load.Execute(null);

            // Verify
            var expected = viewModel.Services.Count > 0;
            Assert.IsTrue(expected);
        }

        [TestMethod]
        public void remove_service()
        {
            // Setup
            new Bootstrap().Run();

            Publish(Messages.REQUEST_SAVE_SERVICE, Mocks.SERVICE_1);

            var viewModel = new ViewModel();
            viewModel.Load.Execute(null);

            // Test
            var service = viewModel.Services.First();
            viewModel.Remove.Execute(service);

            // Verify
            var expected = !viewModel.Services.Contains(service);
            Assert.IsTrue(expected);
        }

        [TestMethod]
        public void add_service()
        {
            // Setup
            var materialsDatabase = new Mock().PrepareMaterialsPromiseDependencies();
            materialsDatabase.Materials.Add(Mocks.SERVICE_1.Materials.Single());
            new Autonomy().Activate();

            var viewModel = new ViewModel();
            viewModel.Load.Execute(null);

            // Test
            Publish(Messages.REQUEST_SAVE_SERVICE, Mocks.SERVICE_1);

            // Verify
            var expected = viewModel.Services.Contains(Mocks.SERVICE_1);
            Assert.IsTrue(expected);
        }

        [TestMethod]
        public void view_materials()
        {
            // Setup
            var materialsDatabase = new Mock().PrepareMaterialsPromiseDependencies();
            materialsDatabase.Materials.Add(Mocks.SERVICE_1.Materials.Single());
            new Autonomy().Activate();

            Publish(Messages.REQUEST_SAVE_SERVICE, Mocks.SERVICE_1);

            var viewModel = new ViewModel();
            viewModel.Load.Execute(null);

            viewModel.SelectedService = viewModel.Services.First();

            // Test
            viewModel.Edit.Execute(null);

            // Verify
            var expected = viewModel.Services.Count > 0;
            Assert.IsTrue(expected);
        }

        [TestMethod]
        public void add_material_to_service()
        {
            // Setup
            new Mock().PrepareDatabases();
            new Autonomy().Activate();
            Mocks.SERVICE_1.Materials.Clear();

            var viewModel = new ViewModel();
            viewModel.Load.Execute(null);
            Publish(Messages.REQUEST_SAVE_SERVICE, Mocks.SERVICE_1);

            // Test
            Publish(Messages.REQUEST_SAVE_MATERIAL, Mocks.MATERIAL_2);

            var serviceMaterial = new ServiceMaterial() { MaterialId = Mocks.MATERIAL_2.Id, ServiceId = Mocks.SERVICE_1.Id };
            Publish(Messages.REQUEST_SAVE_SERVICE_MATERIAL, serviceMaterial);

            // Verify
            var service = viewModel.Services.Single();
            var expected = service.Materials.Contains(Mocks.MATERIAL_2);
            Assert.IsTrue(expected);
        }
    }
}
