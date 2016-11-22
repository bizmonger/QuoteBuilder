using Repositories.Details;

namespace Repositories.Core
{
    public partial class DataBaseFactory
    {
        ProfileDatabase _profileDB = new ProfileDatabase();
        MaterialsDatabase _materialsDB = new MaterialsDatabase();
        ServicesDatabase _servicesDB = new ServicesDatabase();
        ServiceMaterialsDatabase _serviceMaterialsDB = new ServiceMaterialsDatabase();
        QuotesDatabase _quotesDB = new QuotesDatabase();
        CustomersDatabase _customersDatabase = new CustomersDatabase();
    }
}