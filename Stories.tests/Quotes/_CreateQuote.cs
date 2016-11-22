using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Entities;
using Mediation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OperationDependencies;
using TestAPI;
using static Bizmonger.Patterns.MessageBus;
using static TestAPI.Gimme;
using System.Diagnostics;

namespace CreateQuote.Tests
{
    //[DebuggerNonUserCode]
    [TestClass]
    public class _CreateQuote
    {
        [TestInitialize]
        public void TestSetup()
        {
            ClearSubscriptions();
            new Bootstrap().Run();
        }

        [TestMethod]
        public void add_service_and_associated_materials()
        {
            // Setup
            Publish(Messages.REQUEST_SAVE_SERVICE, Mocks.SERVICE_1);
            Publish(Messages.REQUEST_SAVE_SERVICE, Mocks.SERVICE_2);
            Publish(Messages.REQUEST_SAVE_SERVICE, Mocks.SERVICE_3);

            var selectServicesViewModel = new CreateQuote.SelectServices.ViewModel();
            Service service = null;

            // Test
            ManageServiceMaterials.ViewModel ManageServiceMaterialsViewModel = null;
            selectServicesViewModel.SelectedService = selectServicesViewModel.Services.First();

            Subscribe(Messages.REQUEST_VIEW_SERVICE_MATERIALS, obj =>
                {
                    ManageServiceMaterialsViewModel = new ManageServiceMaterials.ViewModel();
                    service = selectServicesViewModel.Services.First();
                    ManageServiceMaterialsViewModel.Continue.Execute(null);
                });

            selectServicesViewModel.ViewMaterials.Execute(null);

            // Verify
            var expected = ManageServiceMaterialsViewModel.Service == service;
            Assert.IsTrue(expected);
        }

        [TestMethod]
        public void load_service_options()
        {
            // Setup
            Publish(Messages.REQUEST_SAVE_SERVICE, Mocks.SERVICE_1);

            // Test
            var viewModel = new CreateQuote.SelectServices.ViewModel();

            // Verify
            Assert.IsTrue(viewModel.Services.Any());
        }

        [TestMethod]
        public void compute_service_with_materials_and_taxes()
        {
            // Setup
            var viewModel = new CalculateTransaction.ViewModel();
            var servicePrice = 199.99m;
            var service = new Service()
            {
                Id = Guid.NewGuid().ToString(),
                Name = SOME_TEXT,
                LaborCost = servicePrice,
                Materials = new ObservableCollection<Material>() { new Material() { Name = SOME_TEXT, MarkupPrice = 59.99m, Quantity = 3, UnitType = SOME_TEXT } },
                TaxPercentage = 10
            };

            // Test
            viewModel.Services.Add(service);
            viewModel.AddService.Execute(service);

            // Verify
            var expected = viewModel.Total == 417.96m;
            Assert.IsTrue(expected);
        }

        [TestMethod]
        public void can_generate_quote()
        {
            // Setup
            Publish(Messages.REQUEST_SAVE_SERVICE, Mocks.SERVICE_1);

            EnterCustomer.ViewModel enterCustomerViewModel = null;

            Subscribe(Messages.REQUEST_VIEW_CUSTOMER_INFORMATION, obj =>
                {
                    enterCustomerViewModel = CreateViewModel();
                    AssignValues(enterCustomerViewModel);
                    enterCustomerViewModel.SelectedServices = new List<Service>(obj as IEnumerable<Service>);
                    enterCustomerViewModel.Load.Execute(null);
                    enterCustomerViewModel.Generate.Execute(null);
                });

            var selectServicesViewModel = new CreateQuote.SelectServices.ViewModel();
            selectServicesViewModel.SelectedServices.Add(Mocks.SERVICE_1);
            selectServicesViewModel.Next.Execute(null);

            var calculateTransactionViewModel = new CalculateTransaction.ViewModel();
            calculateTransactionViewModel.Services.Add(Mocks.SERVICE_1);

            // Test
            calculateTransactionViewModel.AddService.Execute(Mocks.SERVICE_1);

            // Verify
            var expected = enterCustomerViewModel.Generate.CanExecute(null);
            Assert.IsTrue(expected);
        }

        [TestMethod]
        public void cannot_create_quote_with_invalid_email()
        {
            // Setup
            Publish(Messages.REQUEST_SAVE_SERVICE, Mocks.SERVICE_1);

            EnterCustomer.ViewModel enterCustomerViewModel = null;

            Subscribe(Messages.REQUEST_VIEW_CUSTOMER_INFORMATION, obj =>
                {
                    enterCustomerViewModel = CreateViewModel();
                    AssignValues(enterCustomerViewModel);
                    enterCustomerViewModel.Email = "some_invalid_email";
                    enterCustomerViewModel.SelectedServices = new List<Service>(obj as IEnumerable<Service>);
                    enterCustomerViewModel.Load.Execute(null);
                    enterCustomerViewModel.Generate.Execute(null);
                });

            var selectServicesViewModel = new CreateQuote.SelectServices.ViewModel();
            selectServicesViewModel.SelectedServices.Add(Mocks.SERVICE_1);
            selectServicesViewModel.Next.Execute(null);

            var calculateTransactionViewModel = new CalculateTransaction.ViewModel();
            calculateTransactionViewModel.Services.Add(Mocks.SERVICE_1);

            // Test
            calculateTransactionViewModel.AddService.Execute(Mocks.SERVICE_1);
            enterCustomerViewModel.Generate.Execute(null);

            // Verify
            var expected = !enterCustomerViewModel.Generate.CanExecute(null);
            Assert.IsTrue(expected);
        }

        [TestMethod]
        public void generate_quote()
        {
            // Setup
            new Mock().PromiseFileReader();

            Subscribe(Messages.REQUEST_VIEW_CUSTOMER_INFORMATION, obj =>
                {
                    var enterCustomerViewModel = CreateViewModel();
                    AssignValues(enterCustomerViewModel);
                    enterCustomerViewModel.SelectedServices = new List<Service>(obj as IEnumerable<Service>);
                    enterCustomerViewModel.Load.Execute(null);
                    enterCustomerViewModel.Generate.Execute(null);
                });

            Publish(Messages.REQUEST_SAVE_SERVICE, Mocks.SERVICE_1);

            var selectServicesViewModel = new CreateQuote.SelectServices.ViewModel();
            selectServicesViewModel.SelectedServices.Add(Mocks.SERVICE_1);

            selectServicesViewModel.Next.Execute(null);

            var calculateTransactionViewModel = new CalculateTransaction.ViewModel();
            calculateTransactionViewModel.Services.Add(Mocks.SERVICE_1);

            // Test
            calculateTransactionViewModel.AddService.Execute(Mocks.SERVICE_1);

            var viewQuoteViewModel = new ViewQuote.ViewModel();

            // Verify
            var expected = !string.IsNullOrWhiteSpace(viewQuoteViewModel.File.Html);
            Assert.IsTrue(expected);
        }

        [TestMethod]
        public void select_service()
        {
            // Setup
            new Mock().PromiseFileReader();

            Publish(Messages.REQUEST_SAVE_SERVICE, Mocks.SERVICE_1);

            // Test
            var selectServicesViewModel = new CreateQuote.SelectServices.ViewModel();
            selectServicesViewModel.SelectedServices.Add(Mocks.SERVICE_1);

            // Verify
            var expected = selectServicesViewModel.SelectedServices.Count == 1;
            Assert.IsTrue(expected);
        }

        [TestMethod]
        public void maintain_profile_when_generating_quote()
        {
            // Setup
            new Mock().PromiseEmailClient();
            new Mock().PromiseFileReader();

            Publish(Messages.REQUEST_SAVE_SERVICE, Mocks.SERVICE_1);

            EnterCustomer.ViewModel enterCustomerViewModel = null;

            Subscribe(Messages.REQUEST_VIEW_CUSTOMER_INFORMATION, obj =>
                {
                    enterCustomerViewModel = CreateViewModel();
                    AssignValues(enterCustomerViewModel);
                    enterCustomerViewModel.SelectedServices = new List<Service>(obj as IEnumerable<Service>);
                    enterCustomerViewModel.Load.Execute(null);
                    enterCustomerViewModel.Generate.Execute(null);
                });

            var selectServicesViewModel = new CreateQuote.SelectServices.ViewModel();
            selectServicesViewModel.SelectedServices.Add(Mocks.SERVICE_1);
            selectServicesViewModel.Next.Execute(null);

            var calculateTransactionViewModel = new CalculateTransaction.ViewModel();
            calculateTransactionViewModel.Services.Add(Mocks.SERVICE_1);

            calculateTransactionViewModel.AddService.Execute(Mocks.SERVICE_1);
            enterCustomerViewModel.Generate.Execute(null);

            var viewQuoteViewModel = new ViewQuote.ViewModel();
            viewQuoteViewModel.Send.Execute(null);

            // Test
            MockEmailClient emailClient = null;
            Subscribe(Messages.REQUEST_EMAIL_CLIENT_RESPONSE, obj => emailClient = obj as MockEmailClient);
            Publish(Messages.REQUEST_EMAIL_CLIENT);

            // Verify
            var expected = emailClient != null && !string.IsNullOrEmpty(viewQuoteViewModel.Quote.Profile.Email);
            Assert.IsTrue(expected);
        }

        [TestMethod]
        public void no_email_results_in_no_statement_number()
        {
            // Setup
            new Mock().PromiseFileReader();
            new Mock().PromiseEmailClient();

            Publish(Messages.REQUEST_SAVE_SERVICE, Mocks.SERVICE_1);

            Subscribe(Messages.REQUEST_VIEW_CUSTOMER_INFORMATION, obj =>
                {
                    var enterCustomerViewModel = CreateViewModel();
                    AssignValues(enterCustomerViewModel);
                    enterCustomerViewModel.SelectedServices = new List<Service>(obj as IEnumerable<Service>);
                    enterCustomerViewModel.Load.Execute(null);
                    enterCustomerViewModel.Generate.Execute(null);
                });

            var selectServicesViewModel = new CreateQuote.SelectServices.ViewModel();
            selectServicesViewModel.SelectedServices.Add(Mocks.SERVICE_1);
            selectServicesViewModel.Next.Execute(null);

            var calculateTransactionViewModel = new CalculateTransaction.ViewModel();
            calculateTransactionViewModel.Services.Add(Mocks.SERVICE_1);

            calculateTransactionViewModel.AddService.Execute(Mocks.SERVICE_1);

            // Test
            var viewQuoteViewModel = new ViewQuote.ViewModel();
            viewQuoteViewModel.Send.Execute(null);
            viewQuoteViewModel.Home.Execute(null);

            // Verify
            var expected = viewQuoteViewModel.Quote.StatementNumber == 1;
            Assert.IsTrue(expected);
        }

        [TestMethod]
        public void generate_statement_number()
        {
            // Setup
            new Mock().PromiseFileReader();
            new Mock().PromiseEmailClient();

            Publish(Messages.REQUEST_SAVE_SERVICE, Mocks.SERVICE_1);

            EnterCustomer.ViewModel enterCustomerViewModel = null;

            Subscribe(Messages.REQUEST_VIEW_CUSTOMER_INFORMATION, obj =>
                {
                    enterCustomerViewModel = CreateViewModel();
                    AssignValues(enterCustomerViewModel);
                    enterCustomerViewModel.SelectedServices = new List<Service>(obj as IEnumerable<Service>);
                    enterCustomerViewModel.Load.Execute(null);
                    enterCustomerViewModel.Generate.Execute(null);
                });

            var selectServicesViewModel = new CreateQuote.SelectServices.ViewModel();
            selectServicesViewModel.SelectedServices.Add(Mocks.SERVICE_1);
            selectServicesViewModel.Next.Execute(null);

            var calculateTransactionViewModel = new CalculateTransaction.ViewModel();
            calculateTransactionViewModel.Services.Add(Mocks.SERVICE_1);

            calculateTransactionViewModel.AddService.Execute(Mocks.SERVICE_1);
            enterCustomerViewModel.Generate.Execute(null);

            // Test
            var viewQuoteViewModel_1 = new ViewQuote.ViewModel();
            viewQuoteViewModel_1.Send.Execute(null);
            viewQuoteViewModel_1.Home.Execute(null);

            var viewQuoteViewModel_2 = new ViewQuote.ViewModel();
            viewQuoteViewModel_2.Send.Execute(null);
            viewQuoteViewModel_2.Home.Execute(null);

            // Verify
            var expected = viewQuoteViewModel_1.Quote.StatementNumber == 1 &&
                           viewQuoteViewModel_2.Quote.StatementNumber == 2;
            Assert.IsTrue(expected);
        }

        void AssignValues(EnterCustomer.ViewModel enterCustomerViewModel)
        {
            enterCustomerViewModel.FirstName = SOME_TEXT;
            enterCustomerViewModel.LastName = SOME_TEXT;
            enterCustomerViewModel.Phone = SOME_PHONE_NUMBER;
            enterCustomerViewModel.Email = SOME_EMAIL_ADDRESS;
        }

        EnterCustomer.ViewModel CreateViewModel()
        {
            var enterCustomerViewModel = new CreateQuote.EnterCustomer.ViewModel();
            Subscribe(Messages.REQUEST_VIEW_QUOTE, obj =>
            {
                Publish(Messages.REQUEST_QUOTE_DEPENDENCIES_RESPONSE,
                    new ViewQuoteDependencies()
                    {
                        Customer = new Customer(),
                        FileReader = new MockFileServer(),
                        Quote = new Quote(),
                        Logo = null
                    });
            });
            return enterCustomerViewModel;
        }
    }
}