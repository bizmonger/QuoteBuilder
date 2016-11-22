using Bizmonger.Patterns;
using Entities;
using Mediation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestAPI;
using static TestAPI.Gimme;

namespace EditServiceMaterial.Tests
{
    [TestClass]
    public class _EditServiceMaterial
    {
        readonly MessageBus _messagebus = MessageBus.Instance;

        //[TestMethod]
        //public void update_service_material()
        //{
        //    // Setup
        //    var repositoryCreated = new MockMaterialsRepository() != null;
        //    _messagebus.Publish(Messages.REQUEST_SERVICES);

        //    var viewModel = new ManageServiceMaterial.ViewModel();
        //    var selectedMaterial = new Material() { Name = SOME_TEXT, BaseCost = SOME_DECIMAL_VALUE, Description = SOME_TEXT, Quantity = SOME_DECIMAL_VALUE, MarkupPrice = SOME_DECIMAL_VALUE, UnitType = SOME_TEXT };
        //    _messagebus.Publish(Messages.REQUEST_SELECTED_MATERIAL_RESPONSE, selectedMaterial);

        //    // Test
        //    viewModel.Name = SOME_OTHER_TEXT;
        //    viewModel.Update.Execute(null);


        //    // Verify
        //    var expected = repositoryCreated &&
        //                    viewModel.IsUpdated &&
        //                    viewModel.Name == SOME_OTHER_TEXT;
        //    Assert.IsTrue(expected);
        //}

        //[TestMethod]
        //public void cancel_service_material_to_update()
        //{
        //    // Setup
        //    var repositoryCreated = new MockMaterialsRepository() != null;
        //    _messagebus.Publish(Messages.REQUEST_SERVICES);

        //    var viewModel = new EditServiceMaterial.ViewModel();
        //    var selectedMaterial = new Material() { Name = SOME_TEXT, BaseCost = SOME_DECIMAL_VALUE, Description = SOME_TEXT, Quantity = SOME_DECIMAL_VALUE, MarkupPrice = SOME_DECIMAL_VALUE, UnitType = SOME_TEXT };
        //    _messagebus.Publish(Messages.REQUEST_SELECTED_MATERIAL_RESPONSE, selectedMaterial);

        //    // Test
        //    viewModel.Name = SOME_OTHER_TEXT;
        //    viewModel.Cancel.Execute(null);
            
        //    // Verify
        //    var expected = repositoryCreated &&
        //                    !viewModel.IsUpdated &&
        //                    viewModel.Name == SOME_TEXT;
        //    Assert.IsTrue(expected);
        //}
    }
}