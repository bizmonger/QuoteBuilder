using Entities;
using Mediation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Servers;
using System.Diagnostics;
using System.Linq;
using TestAPI;
using static Bizmonger.Patterns.MessageBus;
using static TestAPI.Gimme;

namespace AddService.Tests
{
    [DebuggerNonUserCode]
    [TestClass]
    public partial class _AddService
    {
        [TestInitialize]
        public void TestSetup() => ClearSubscriptions();

        [TestMethod]
        public void add_service()
        {
            // Setup
            new Bootstrap().Run();

            var viewModel = new ViewModel();

            // Test
            viewModel.Name = SOME_TEXT;
            viewModel.LaborCost = SOME_DECIMAL_VALUE.ToString();
            viewModel.TaxPercentage = SOME_DECIMAL_VALUE.ToString();
            viewModel.Description = SOME_TEXT;
            viewModel.SaveCommand.Execute(null);

            // Verify
            Assert.IsTrue(viewModel.Saved);
        }

        [TestMethod]
        public void add_service_with_material()
        {
            // Setup
            new Bootstrap().Run();
            var addServiceviewModel = new AddService.ViewModel();

            Subscribe(Messages.REQUEST_VIEW_SERVICE_MATERIALS, obj =>
                {
                    var manageServiceMaterialViewModel = new ManageServiceMaterials.ViewModel() { Service = obj as Service };
                    manageServiceMaterialViewModel.Continue.Execute(null);
                });

            // Test
            CreateServiceWithMaterial(addServiceviewModel);
            addServiceviewModel.ViewMaterials.Execute(null);
            addServiceviewModel.SaveCommand.Execute(null);

            // Verify
            var expected = addServiceviewModel.Saved &&
                addServiceviewModel.Materials.Single() != null;
            Assert.IsTrue(expected);
        }

        [TestMethod]
        public void cancel_service()
        {
            // Setup
            new Bootstrap().Run();
            var viewModel = new ViewModel();

            // Test
            viewModel.Name = SOME_TEXT;
            viewModel.LaborCost = SOME_DECIMAL_VALUE.ToString();
            viewModel.TaxPercentage = SOME_DECIMAL_VALUE.ToString();
            viewModel.Description = SOME_TEXT;
            viewModel.Cancel.Execute(null);

            // Verify
            var expected = !viewModel.Saved;
            Assert.IsTrue(expected);
        }

        [TestMethod]
        public void profile_created_on_new_service()
        {
            // Setup
            new Bootstrap().Run();

            var viewModel = new ViewModel();
            viewModel.Name = SOME_TEXT;
            viewModel.LaborCost = SOME_DECIMAL_VALUE.ToString();
            viewModel.TaxPercentage = SOME_DECIMAL_VALUE.ToString();
            viewModel.Description = SOME_TEXT;

            // Test
            viewModel.SaveCommand.Execute(null);

            // Verify
            var profileCreated = new ProfileServer().GetProfile() != null;
            Assert.IsTrue(profileCreated);
        }
    }
}