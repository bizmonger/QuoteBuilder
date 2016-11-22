using static Bizmonger.Patterns.MessageBus;
using Mediation;
using System.Diagnostics;

namespace TestAPI
{
    //[DebuggerNonUserCode]
    public class Mock
    {
        public void PromiseProfile()
        {
            Subscribe(Messages.REQUEST_PROFILE, obj =>
                Publish(Messages.REQUEST_PROFILE_RESPONSE, Mocks.Profile_1));
        }

        public void PromiseFileReader()
        {
            Subscribe(Messages.REQUEST_FILE_READER, obj =>
                Publish(Messages.REQUEST_FILE_READER_RESPONSE, new MockFileServer()));
        }

        public void PromiseEmailClient() =>
            Subscribe(Messages.REQUEST_EMAIL_CLIENT, obj =>
                Publish(Messages.REQUEST_EMAIL_CLIENT_RESPONSE, new MockEmailClient()));

        public void PrepareProfileDB()
        {
            new Mock().PromiseProfile();

            var profileDatabase = new MockProfileDatabase();
            Subscribe(Messages.REQUEST_PROFILE_DATABASE, obj =>
                Publish(Messages.REQUEST_PROFILE_DATABASE_RESPONSE, profileDatabase));
        }

        public MockServicesDatabase PrepareServicesDB()
        {
            var servicesDatabase = new MockServicesDatabase();
            Subscribe(Messages.REQUEST_SERVICES_DATABASE, obj =>
                Publish(Messages.REQUEST_SERVICES_DATABASE_RESPONSE, servicesDatabase));
            return servicesDatabase;
        }

        public MockCustomersDatabase PrepareCustomersDB()
        {
            var customersDatabase = new MockCustomersDatabase();
            Subscribe(Messages.REQUEST_CUSTOMERS_DATABASE, obj =>
                Publish(Messages.REQUEST_CUSTOMERS_DATABASE_RESPONSE, customersDatabase));
            return customersDatabase;
        }

        public MockQuotesDatabase PrepareQuotesDB()
        {
            var quotesDatabase = new MockQuotesDatabase();
            Subscribe(Messages.REQUEST_QUOTES_DATABASE, obj =>
                Publish(Messages.REQUEST_QUOTES_DATABASE_RESPONSE, quotesDatabase));
            return quotesDatabase;
        }

        public MockServiceMaterialsDatabase PrepareServiceMaterialsDB()
        {
            var serviceMaterialsDatabase = new MockServiceMaterialsDatabase();
            Subscribe(Messages.REQUEST_SERVICE_MATERIALS_DATABASE, obj =>
                Publish(Messages.REQUEST_SERVICE_MATERIALS_DATABASE_RESPONSE, serviceMaterialsDatabase));
            return serviceMaterialsDatabase;
        }

        public MockMaterialsDatabase PrepareMaterialsDB()
        {
            var materialsDatabase = new MockMaterialsDatabase();
            Subscribe(Messages.REQUEST_MATERIALS_DATABASE, obj =>
                Publish(Messages.REQUEST_MATERIALS_DATABASE_RESPONSE, materialsDatabase));
            return materialsDatabase;
        }

        public MockMaterialsDatabase PrepareMaterialsPromiseDependencies()
        {
            PrepareProfileDB();
            var materialsDatabase = PrepareMaterialsDB();
            var servicesDatabase = PrepareServicesDB();
            var serviceMaterialsDatabase = PrepareServiceMaterialsDB();
            var quotesDatabase = PrepareQuotesDB();
            var customersDatabase = PrepareCustomersDB();
            return materialsDatabase;
        }

        public MockServiceMaterialsDatabase PrepareServiceMaterialsDependencies()
        {
            PrepareProfileDB();
            var servicesDatabase = PrepareServicesDB();
            var serviceMaterialsDatabase = PrepareServiceMaterialsDB();
            var materialsDatabase = PrepareMaterialsDB();
            var quotesDatabase = PrepareQuotesDB();
            var customersDatabase = PrepareCustomersDB();
            return serviceMaterialsDatabase;
        }

        public void PrepareDatabases()
        {
            PrepareProfileDB();
            PrepareServicesDB();
            PrepareServiceMaterialsDB();
            PrepareMaterialsDB();
            PrepareQuotesDB();
            PrepareCustomersDB();
        }
    }
}
