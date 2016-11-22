using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repositories.Core;
using Servers;
using TestAPI;
using static Bizmonger.Patterns.MessageBus;
using static TestAPI.Gimme;
using System.Diagnostics;

namespace AddMaterial.Tests
{
    [DebuggerNonUserCode]
    [TestClass]
    public class _AddMaterial
    {
        [TestInitialize]
        public void TestSetup()
        {
            ClearSubscriptions();
            new Bootstrap().Run();
        }

        [TestMethod]
        public void add_material()
        {
            // Setup
            var mock = new Mock();
            mock.PrepareProfileDB();
            mock.PrepareServicesDB();
            mock.PrepareServiceMaterialsDB();
            mock.PrepareQuotesDB();
            mock.PrepareCustomersDB();
            var materialsDB = mock.PrepareMaterialsDB();
            new Autonomy().Activate();

            var manageMaterialsViewModel = new ManageMaterials.ViewModel();
            var viewModel = new ViewModel();

            // Test
            viewModel.Name = SOME_TEXT;
            viewModel.Description = SOME_TEXT;
            viewModel.Quantity = SOME_DECIMAL_VALUE.ToString();
            viewModel.UnitType = SOME_TEXT;
            viewModel.BaseCost = SOME_DECIMAL_VALUE.ToString();
            viewModel.MarkupPrice = SOME_DECIMAL_VALUE.ToString();
            viewModel.Save.Execute(null);

            // Verify
            var expected = viewModel.IsSaved && manageMaterialsViewModel.Materials.Single().Name == SOME_TEXT;
            Assert.IsTrue(expected);
        }

        [TestMethod]
        public void cancel_material()
        {
            // Setup
            new Bootstrap().Run();

            var manageMaterialsViewModel = new ManageMaterials.ViewModel();
            var viewModel = new ViewModel();

            // Test
            viewModel.Name = SOME_TEXT;
            viewModel.Description = SOME_TEXT;
            viewModel.Quantity = SOME_DECIMAL_VALUE.ToString();
            viewModel.UnitType = SOME_TEXT;
            viewModel.BaseCost = SOME_DECIMAL_VALUE.ToString();
            viewModel.MarkupPrice = SOME_DECIMAL_VALUE.ToString();
            viewModel.Cancel.Execute(null);

            // Verify
            var expected = !viewModel.IsSaved && !manageMaterialsViewModel.Materials.Any();
            Assert.IsTrue(expected);
        }

        [TestMethod]
        public void profile_created_on_new_material()
        {
            // Setup
            new Bootstrap().Run();

            var manageMaterialsViewModel = new ManageMaterials.ViewModel();
            var viewModel = new ViewModel();

            // Test
            viewModel.Name = SOME_TEXT;
            viewModel.Description = SOME_TEXT;
            viewModel.Quantity = SOME_DECIMAL_VALUE.ToString();
            viewModel.UnitType = SOME_TEXT;
            viewModel.BaseCost = SOME_DECIMAL_VALUE.ToString();
            viewModel.MarkupPrice = SOME_DECIMAL_VALUE.ToString();
            viewModel.Save.Execute(null);

            // Verify
            var expected = new ProfileServer().GetProfile() != null;
            Assert.IsTrue(expected);
        }
    }
}