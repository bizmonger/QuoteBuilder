using System.Linq;
using Mediation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestAPI;
using static Bizmonger.Patterns.MessageBus;
using static TestAPI.Gimme;
using System.Diagnostics;

namespace ManageMaterials.Tests
{
    [DebuggerNonUserCode]
    [TestClass]
    public class _ManageMaterials
    {
        [TestInitialize]
        public void TestSetup()
        {
            ClearSubscriptions();
            new Bootstrap().Run();
        }

        [TestMethod]
        public void load_materials()
        {
            // Setup
            Publish(Messages.REQUEST_SAVE_MATERIAL, Mocks.MATERIAL_1);

            // Test
            var viewModel = new ViewModel();
            viewModel.Load.Execute(null);

            // Verify
            var expected = viewModel.Materials.Single() == Mocks.MATERIAL_1;
            Assert.IsTrue(expected);
        }

        [TestMethod]
        public void add_material()
        {
            // Setup
            Subscribe(Messages.REQUEST_PROFILE_DATABASE, payload =>
                Publish(Messages.REQUEST_PROFILE_DATABASE_RESPONSE, new MockProfileDatabase()));
            
            var manageMaterialsViewModel = new ViewModel();
            manageMaterialsViewModel.Load.Execute(null);
            manageMaterialsViewModel.New.Execute(null);

            var addMaterialViewModel = new AddMaterial.ViewModel();

            // Test
            addMaterialViewModel.Name = SOME_TEXT;
            addMaterialViewModel.Description = SOME_TEXT;
            addMaterialViewModel.Quantity = SOME_DECIMAL_VALUE.ToString();
            addMaterialViewModel.BaseCost = SOME_CURRENCY_VALUE.ToString();
            addMaterialViewModel.MarkupPrice = SOME_CURRENCY_VALUE.ToString();
            addMaterialViewModel.UnitType = SOME_TEXT;

            addMaterialViewModel.Save.Execute(null);

            // Verify
            var expected = manageMaterialsViewModel.Materials.Single().Name == addMaterialViewModel.Name &&
                           addMaterialViewModel.IsSaved;
            Assert.IsTrue(expected);
        }

        [TestMethod]
        public void remove_material()
        {
            // Setup
            Publish(Messages.REQUEST_SAVE_MATERIAL, Mocks.MATERIAL_1);

            var viewModel = new ViewModel();
            viewModel.Load.Execute(null);

            // Test
            viewModel.Remove.Execute(Mocks.MATERIAL_1);

            // Verify
            var expected = !viewModel.Materials.Contains(Mocks.MATERIAL_1);
            Assert.IsTrue(expected);
        }

        [TestMethod]
        public void edit_material()
        {
            // Setup
            Publish(Messages.REQUEST_SAVE_MATERIAL, Mocks.MATERIAL_1);

            var viewModel = new ViewModel();
            viewModel.Load.Execute(null);

            // Test
            viewModel.Edit.Execute(Mocks.MATERIAL_1);

            // Verify
            var expected = viewModel.Materials.Contains(Mocks.MATERIAL_1);
            Assert.IsTrue(expected);
        }
    }
}