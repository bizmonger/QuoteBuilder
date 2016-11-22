using Mediation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Linq;
using TestAPI;
using static Bizmonger.Patterns.MessageBus;

namespace ManageServiceMaterials.Tests
{
    [DebuggerNonUserCode]
    [TestClass]
    public class _ManageServiceMaterials
    {
        [TestInitialize]
        public void TestSetup() => ClearSubscriptions();

        [TestMethod]
        public void add_service_material()
        {
            // Setup
            new Bootstrap().Run();

            Mocks.SERVICE_1.Materials.Clear();

            Publish(Messages.REQUEST_SAVE_SERVICE, Mocks.SERVICE_1);
            Publish(Messages.REQUEST_SAVE_MATERIAL, Mocks.MATERIAL_1);

            Subscribe(Messages.REQUEST_SELECTED_SERVICE, obj =>
                Publish(Messages.REQUEST_SELECTED_SERVICE_RESPONSE, Mocks.SERVICE_1));

            var viewModel = new ViewModel();

            // Test
            viewModel.SelectedMaterialFromCache = viewModel.Materials.FirstOrDefault();
            viewModel.Add.Execute(null);

            // Verify
            var expected = viewModel.AssignedMaterials.Single() != null &&
                           !viewModel.Materials.Any();

            Assert.IsTrue(expected);
        }

        public void new_service_material()
        {
            // Setup
            new Bootstrap().Run();
            Publish(Messages.REQUEST_SAVE_SERVICE, Mocks.SERVICE_1);

            Publish(Messages.REQUEST_SAVE_MATERIAL, Mocks.MATERIAL_1);
            Publish(Messages.REQUEST_SAVE_MATERIAL, Mocks.MATERIAL_2);
            Publish(Messages.REQUEST_SAVE_MATERIAL, Mocks.MATERIAL_3);

            var viewModel = new ManageServiceMaterials.ViewModel();

            var newMaterialRequested = false;
            Subscribe(Messages.REQUEST_VIEW_NEW_MATERIAL, obj => newMaterialRequested = true);

            // Test
            viewModel.New.Execute(null);

            // Verify
            Assert.IsTrue(newMaterialRequested);
        }

        [TestMethod]
        public void remove_service_material()
        {
            // Setup
            new Bootstrap().Run();

            Publish(Messages.REQUEST_SAVE_SERVICE, Mocks.SERVICE_1);
            Publish(Messages.REQUEST_SAVE_MATERIAL, Mocks.MATERIAL_1);
            Publish(Messages.REQUEST_SAVE_MATERIAL, Mocks.MATERIAL_2);
            Publish(Messages.REQUEST_SAVE_MATERIAL, Mocks.MATERIAL_3);

            Mocks.SERVICE_1.Materials.Clear();

            Subscribe(Messages.REQUEST_SELECTED_SERVICE, obj =>
                Publish(Messages.REQUEST_SELECTED_SERVICE_RESPONSE, Mocks.SERVICE_1));

            var viewModel = new ManageServiceMaterials.ViewModel();

            viewModel.Service = Mocks.SERVICE_1;
            viewModel.SelectedMaterialFromCache = viewModel.Materials[0];
            viewModel.Add.Execute(null);
            viewModel.SelectedAssignedMaterial = viewModel.AssignedMaterials[0];

            // Test
            viewModel.Remove.Execute(null);

            // Verify
            var expected = !viewModel.AssignedMaterials.Any();

            Assert.IsTrue(expected);
        }

        [TestMethod]
        public void load_materials_cache()
        {
            // Setup
            new Bootstrap().Run();

            Publish(Messages.REQUEST_SAVE_SERVICE, Mocks.SERVICE_1);
            Publish(Messages.REQUEST_SAVE_MATERIAL, Mocks.MATERIAL_1);
            Mocks.SERVICE_1.Materials.Clear();

            Subscribe(Messages.REQUEST_SELECTED_SERVICE, obj =>
                Publish(Messages.REQUEST_SELECTED_SERVICE_RESPONSE, Mocks.SERVICE_1));

            // Test
            var viewModel = new ViewModel();

            // Verify
            var expected = viewModel.Materials.Any();
            Assert.IsTrue(expected);
        }
    }
}