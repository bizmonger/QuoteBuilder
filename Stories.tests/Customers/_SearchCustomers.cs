using System.Linq;
using static Bizmonger.Patterns.MessageBus;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestAPI;
using Entities;
using Mediation;
using System.Diagnostics;
using System;

namespace SearchCustomers.Tests
{
    [DebuggerNonUserCode]
    [TestClass]
    public class _SearchCustomers
    {
        const string FIRST_NAME = "customer1_firstName";

        [TestInitialize]
        public void TestSetup()
        {
            ClearSubscriptions();
            new Bootstrap().Run();
        }

        [TestMethod]
        public void load_customers()
        {
            // Setup
            Publish(Messages.SAVE_CUSTOMER, new Customer() { FirstName = FIRST_NAME });
            var viewModel = new ViewModel();

            // Test
            viewModel.Load.Execute(null);

            // Assert
            var expected = viewModel.Customers.Any();
            Assert.IsTrue(expected);
        }

        [TestMethod]
        public void search_customer_by_name()
        {
            // Setup
            Publish(Messages.SAVE_CUSTOMER, new Customer() { FirstName = FIRST_NAME });

            var viewModel = new ViewModel();
            viewModel.Load.Execute(null);

            // Test
            viewModel.SearchText = FIRST_NAME;
            viewModel.Search.Execute(null);

            // Assert
            var expected = viewModel.Results.Single().FirstName == FIRST_NAME;
            Assert.IsTrue(expected);
        }

        [TestMethod]
        public void searching_customer_has_multiple_matches()
        {
            // Setup
            //var someLastName = "last_name";
            //Publish(Messages.SAVE_CUSTOMER, new Customer() { FirstName = "albert", LastName = someLastName, Id = Guid.NewGuid().ToString() });
            //Publish(Messages.SAVE_CUSTOMER, new Customer() { FirstName = "alvin", LastName = someLastName, Id = Guid.NewGuid().ToString() });
            //Publish(Messages.SAVE_CUSTOMER, new Customer() { FirstName = "alex", LastName = someLastName, Id = Guid.NewGuid().ToString() });
            //Publish(Messages.SAVE_CUSTOMER, new Customer() { FirstName = "ashish", LastName = someLastName, Id = Guid.NewGuid().ToString() });

            var viewModel = new ViewModel();
            viewModel.Load.Execute(null);

            // Test
            viewModel.SearchText = "al";
            viewModel.Search.Execute(null);

            // Assert
            var expectedCustomers = viewModel.Results;
            var expected = expectedCustomers.Count(c => c.FirstName == "albert" || c.FirstName == "alvin" || c.FirstName == "alex") == 3;
            Assert.IsTrue(expected);
        }

        [TestMethod]
        public void view_quote()
        {
            // Setup
            var navigatedToQuote = false;
            Subscribe(Messages.REQUEST_VIEW_QUOTE, obj => navigatedToQuote = true);

            var viewModel = new ViewModel();
            viewModel.Load.Execute(null);

            viewModel.SearchText = "albert";
            viewModel.Search.Execute(null);

            // Test
            viewModel.SelectedCustomer = viewModel.Results.Single();
            viewModel.View.Execute(null);

            // Assert
            Assert.IsTrue(navigatedToQuote);
        }
    }
}