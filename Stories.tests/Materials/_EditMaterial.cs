using Bizmonger.Patterns;
using static Bizmonger.Patterns.MessageBus;
using Entities;
using Mediation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestAPI;
using static TestAPI.Gimme;
using System.Diagnostics;

namespace EditMaterial.Tests
{
    [DebuggerNonUserCode]
    [TestClass]
    public class _EditMaterial
    {
        [TestInitialize]
        public void TestSetup() => ClearSubscriptions();

        

        [TestMethod]
        public void update_material()
        {
            // Setup
            new Bootstrap().Run();

            var viewModel = new ViewModel();
            var selectedMaterial = new Material() { Name = SOME_TEXT, BaseCost = SOME_DECIMAL_VALUE, Description = SOME_TEXT, Quantity = SOME_DECIMAL_VALUE, MarkupPrice = SOME_DECIMAL_VALUE, UnitType = SOME_TEXT };
            Publish(Messages.REQUEST_SELECTED_MATERIAL_RESPONSE, selectedMaterial);

            // Test
            viewModel.Name = SOME_TEXT;
            viewModel.Save.Execute(null);

            // Verify
            var expected = viewModel.Name == SOME_TEXT && viewModel.IsUpdated;
            Assert.IsTrue(expected);
        }

        [TestMethod]
        public void cancel_edit()
        {
            // Setup
            new Bootstrap().Run();

            var viewModel = new ViewModel();
            viewModel.MaterialToUpdate = new Material() { Name = SOME_TEXT };

            // Test
            viewModel.Name = SOME_OTHER_TEXT;
            viewModel.Cancel.Execute(null);

            // Verify
            var expected = viewModel.Name == SOME_TEXT && !viewModel.IsUpdated;
            Assert.IsTrue(expected);
        }
    }
}