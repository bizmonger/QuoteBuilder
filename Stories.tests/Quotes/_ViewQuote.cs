using static Bizmonger.Patterns.MessageBus;
using Mediation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OperationDependencies;
using TestAPI;
using static TestAPI.Gimme;
using Servers;
using System.Diagnostics;

namespace ViewQuote.Tests
{
    [DebuggerNonUserCode]
    [TestClass]
    public class _ViewQuote
    {
        [TestInitialize]
        public void TestSetup() => ClearSubscriptions();

        [TestMethod]
        public void button_state_updates_after_sending_quote()
        {
            // Setup
            new Bootstrap().Run();

            var viewModel = new ViewQuote.ViewModel();

            new Mock().PromiseEmailClient();
            MockQuoteDependencies();

            // Test
            viewModel.Send.Execute(null);

            // Verify
            var expected = viewModel.State == "Sent" && !viewModel.Send.CanExecute(null);
            Assert.IsTrue(expected);
        }

        [TestMethod]
        public void email_quote()
        {
            // Setup
            new Bootstrap().Run();

            var viewModel = new ViewQuote.ViewModel();

            new Mock().PromiseEmailClient();
            MockQuoteDependencies();

            // Test
            viewModel.Send.Execute(null);

            // Verify
            Assert.IsTrue(viewModel.IsSent);
        }

        [TestMethod]
        public void request_home()
        {
            // Setup
            var viewModel = new ViewModel();

            bool homeRequested = false;
            Subscribe(Messages.REQUEST_VIEW_VIEW_MENU, obj => homeRequested = true);

            // Test
            viewModel.Home.Execute(null);

            // Verify
            var expected = !viewModel.IsSent && homeRequested;
            Assert.IsTrue(expected);
        }

        #region Helpers
        void MockQuoteDependencies()
        {
            var dependencies = new ViewQuoteDependencies()
            {
                Customer = TestAPI.Mocks.Customer_1,
                Logo = SOME_TEXT,
                FileReader = new MockFileServer(),
                Quote = TestAPI.Mocks.Quote_1
            };

            Subscribe(Messages.REQUEST_QUOTE_DEPENDENCIES, obj =>
                Publish(Messages.REQUEST_QUOTE_DEPENDENCIES_RESPONSE, dependencies));

            dependencies.Quote.Profile = new ProfileServer().GetProfile();
        }
        #endregion
    }
}
