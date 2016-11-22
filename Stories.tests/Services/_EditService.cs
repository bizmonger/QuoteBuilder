using System.Linq;
using Mediation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestAPI;
using static Bizmonger.Patterns.MessageBus;
using static TestAPI.Gimme;
using System.Diagnostics;

namespace EditService.Tests
{
    [DebuggerNonUserCode]
    [TestClass]
    public class _EditService
    {
        [TestInitialize]
        public void TestSetup() => ClearSubscriptions();

        [TestMethod]
        public void update_service()
        {
            // Setup
            new Bootstrap().Run();

            Publish(Messages.REQUEST_SAVE_SERVICE, Mocks.SERVICE_1);

            var servicesViewModel = new ManageServices.ViewModel();
            servicesViewModel.Load.Execute(null);

            var viewModel = new ViewModel();

            // Test
            var service = servicesViewModel.Services.First();
            viewModel.ServiceToUpdate = servicesViewModel.Services.First();
            viewModel.Name = SOME_OTHER_TEXT;
            viewModel.Materials = service.Materials;
            viewModel.TaxPercentage = service.TaxPercentage.ToString();
            viewModel.LaborCost = service.LaborCost.ToString();
            viewModel.Description = service.Description;
            viewModel.Update.Execute(null);

            // Verify
            var expected = viewModel.IsUpdated && Mocks.SERVICE_1.Name == SOME_OTHER_TEXT;
            Assert.IsTrue(expected);
        }

        [TestMethod]
        public void cancel_service_to_update()
        {
            // Setup
            new Bootstrap().Run();

            Publish(Messages.REQUEST_SAVE_SERVICE, Mocks.SERVICE_1);

            var servicesViewModel = new ManageServices.ViewModel();
            servicesViewModel.Load.Execute(null);

            var viewModel = new ViewModel();

            // Test
            var service = servicesViewModel.Services.First();
            var initialName = service.Name;

            viewModel.Name = SOME_OTHER_TEXT;
            viewModel.Cancel.Execute(null);

            // Verify
            var expected = !viewModel.IsUpdated &&
                            service.Name == initialName;
            Assert.IsTrue(expected);
        }
    }
}