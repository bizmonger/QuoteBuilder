using static Bizmonger.Patterns.MessageBus;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestAPI;
using static TestAPI.Gimme;
using System.Diagnostics;
using Repositories;

namespace ManageProfile.Tests
{
    //[DebuggerNonUserCode]
    [TestClass]
    public class _ManageProfile
    {
        [TestInitialize]
        public void TestSetup()
        {
            ClearSubscriptions();
            new Bootstrap().Run();
        }

        [TestMethod]
        public void save_changes()
        {
            // Setup
            var viewModel = new ViewModel();
            var save = viewModel.Save;

            // Test
            viewModel.FirstName = SOME_TEXT;
            viewModel.LastName = SOME_TEXT;
            viewModel.BusinessName = SOME_TEXT;
            viewModel.Phone = SOME_PHONE_NUMBER;
            viewModel.Email = SOME_EMAIL_ADDRESS;
            viewModel.Address1 = SOME_TEXT;
            viewModel.Address2 = SOME_TEXT;
            viewModel.City = SOME_TEXT;
            viewModel.State = SOME_TEXT;
            viewModel.Postal = SOME_TEXT;

            save.Execute(null);

            // Verify
            var expected = viewModel.Saved;
            Assert.IsTrue(expected);
        }

        [TestMethod]
        public void cannot_save_profile_with_invalid_email()
        {
            // Setup
            var viewModel = new ViewModel();
            var save = viewModel.Save;

            // Test
            viewModel.FirstName = SOME_TEXT;
            viewModel.LastName = SOME_TEXT;
            viewModel.BusinessName = SOME_TEXT;
            viewModel.Phone = SOME_PHONE_NUMBER;
            viewModel.Email = "some_invalid_email";
            viewModel.Address1 = SOME_TEXT;
            viewModel.Address2 = SOME_TEXT;
            viewModel.City = SOME_TEXT;
            viewModel.State = SOME_TEXT;
            viewModel.Postal = SOME_TEXT;

            save.Execute(null);

            // Verify
            var expected = !viewModel.Saved;
            Assert.IsTrue(expected);
        }

        [TestMethod]
        public void cannot_save_incomplete_changes()
        {
            // Setup
            var viewModel = new ViewModel();
            var save = viewModel.Save;

            // Test
            viewModel.FirstName = SOME_TEXT;
            viewModel.LastName = SOME_TEXT;
            viewModel.BusinessName = SOME_TEXT;
            viewModel.Phone = SOME_PHONE_NUMBER;
            viewModel.Email = null;

            save.Execute(null);

            // Verify
            var expected = !viewModel.Saved;
            Assert.IsTrue(expected);
        }

        [TestMethod]
        public void cancel_changes()
        {
            // Setup
            var viewModel = new ViewModel();
            var cancel = viewModel.Cancel;

            // Test
            viewModel.FirstName = SOME_TEXT;
            viewModel.LastName = SOME_TEXT;
            viewModel.BusinessName = SOME_TEXT;
            viewModel.Phone = SOME_PHONE_NUMBER;
            viewModel.Email = null;

            cancel.Execute(null);

            // Verify
            var expected = !viewModel.Saved;
            Assert.IsTrue(expected);
        }
    }
}