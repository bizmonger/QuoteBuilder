using System;
using System.Collections.ObjectModel;
using Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static TestAPI.Gimme;

namespace CalculateTransaction.Tests
{
    [TestClass]
    public class _CalculateTransaction
    {
        [TestMethod]
        public void compute_service_with_no_tax()
        {
            // Setup
            var viewModel = new ViewModel();
            var servicePrice = 199.99m;
            var service = new Service() { Id = Guid.NewGuid().ToString(), Name = SOME_TEXT, LaborCost = servicePrice };
            viewModel.Services.Add(service);

            // Test
            viewModel.AddService.Execute(service);

            // Verify
            var expected = viewModel.Total == servicePrice;
            Assert.IsTrue(expected);
        }

        [TestMethod]
        public void compute_service_with_tax()
        {
            // Setup
            var viewModel = new ViewModel();
            var servicePrice = 199.99m;
            var service = new Service() { Id = Guid.NewGuid().ToString(), Name = SOME_TEXT, LaborCost = servicePrice, TaxPercentage = 10 };
            viewModel.Services.Add(service);

            // Test
            viewModel.AddService.Execute(service);

            // Verify
            var expected = viewModel.Total == 219.99m;
            Assert.IsTrue(expected);
        }

        [TestMethod]
        public void compute_service_with_materials()
        {
            // Setup
            var viewModel = new ViewModel();
            var servicePrice = 199.99m;
            var service = new Service()
            {
                Id = Guid.NewGuid().ToString(),
                Name = SOME_TEXT,
                LaborCost = servicePrice,
                Materials = new ObservableCollection<Material>() { new Material() { Name = SOME_TEXT, MarkupPrice = 59.99m, Quantity = 3, UnitType = SOME_TEXT } }
            };
            viewModel.Services.Add(service);

            // Test
            viewModel.AddService.Execute(service);

            // Verify
            var expected = viewModel.Total == 379.96m;
            Assert.IsTrue(expected);
        }

        [TestMethod]
        public void compute_service_with_materials_and_taxes()
        {
            // Setup
            var viewModel = new ViewModel();
            var servicePrice = 199.99m;
            var service = new Service()
            {
                Id = Guid.NewGuid().ToString(),
                Name = SOME_TEXT,
                LaborCost = servicePrice,
                Materials = new ObservableCollection<Material>() { new Material() { Name = SOME_TEXT, MarkupPrice = 59.99m, Quantity = 3, UnitType = SOME_TEXT } },
                TaxPercentage = 10
            };

            viewModel.Services.Add(service);

            // Test
            viewModel.AddService.Execute(service);

            // Verify
            var expected = viewModel.Total == 417.96m;
            Assert.IsTrue(expected);
        }
    }
}