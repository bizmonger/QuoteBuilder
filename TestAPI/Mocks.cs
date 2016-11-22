using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Entities;
using System.Diagnostics;

namespace TestAPI
{
    [DebuggerNonUserCode]
    public static class Mocks
    {
        public static Profile Profile_1 = new Profile()
        {
            Id = Guid.NewGuid().ToString(),
            FirstName = "profile.firstName",
            LastName = "profile.lastName",
            BusinessName = "profile.businessName",
            Phone = "profile.phone",
            Email = "profile.email",
            Address1 = "profile.address1",
            Address2 = "profile.address2",
            City = "profile.city",
            State = "profile.state",
            Postal = "profile.postal",
        };

        public static Material MATERIAL_1 = new Material() { Id = Guid.NewGuid().ToString(), Name = "material_1", Description = "material_1_description", Quantity = 1, BaseCost = 99.99m, MarkupPrice = 99.99m, UnitType = "units", UserId = Profile_1.Id };
        public static Material MATERIAL_2 = new Material() { Id = Guid.NewGuid().ToString(), Name = "material_2", Description = "material_2_description", Quantity = 1, BaseCost = 99.99m, MarkupPrice = 99.99m, UnitType = "units", UserId = Profile_1.Id };
        public static Material MATERIAL_3 = new Material() { Id = Guid.NewGuid().ToString(), Name = "material_3", Description = "material_3_description", Quantity = 1, BaseCost = 99.99m, MarkupPrice = 99.99m, UnitType = "units", UserId = Profile_1.Id };

        public static Service SERVICE_1 = new Service()
        {
            Id = Guid.NewGuid().ToString(),
            Name = "service_1",
            Description = "service_1_description",
            LaborCost = 99.99m,
            Materials = new ObservableCollection<Material>() { Mocks.MATERIAL_1 },
            UserId = Profile_1.Id,
            TaxPercentage = 10,
            ServiceMaterials = new ObservableCollection<ServiceMaterial>()
        };

        public static Service SERVICE_2 = new Service()
        {
            Id = Guid.NewGuid().ToString(),
            Name = "service_2",
            Description = "service_2_description",
            LaborCost = 99.99m,
            Materials = new ObservableCollection<Material>() { Mocks.MATERIAL_2 }
        };

        public static Service SERVICE_3 = new Service()
        {
            Id = Guid.NewGuid().ToString(),
            Name = "service_3",
            Description = "service_3_description",
            LaborCost = 99.99m,
            Materials = new ObservableCollection<Material>() { Mocks.MATERIAL_1, Mocks.MATERIAL_2, Mocks.MATERIAL_3 }
        };
        public static ServiceMaterial SERVICE_MATERIAL_1 = new ServiceMaterial() { Id = Guid.NewGuid().ToString(), ServiceId = SERVICE_1.Id, MaterialId = SERVICE_1.Materials.Single().Id, UserId = Profile_1.Id, Quantity = 2 };
        
        public static Customer Customer_1 = new Customer()
        {
            FirstName = "customer_1.firstname",
            LastName = "customer_1.lastname",
            Email = "customer_1@email.com",
            Phone = "customer_1.phone",
            Description = "customer_1.description"
        };

        public static Quote Quote_1 = new Quote()
        {
            Profile = Profile_1,
            TypeName = "Quote",
            CustomerId = Customer_1.Id,
            Services = new List<Service>() { SERVICE_1 },
            Title = "quote_1.title",
            Address = new Address()
            {
                Address1 = "address1",
                Address2 = "address2",
                City = "city",
                State = "state",
                Name = "name",
                Postal = "postal",
            }
        };
    }
}