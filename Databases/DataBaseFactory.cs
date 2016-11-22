using Mediation;
using static Bizmonger.Patterns.MessageBus;

namespace Repositories.Core
{
    public partial class DataBaseFactory
    {
        public void PromiseDBs()
        {
            Subscribe(Messages.REQUEST_PROFILE_DATABASE_RESPONSE, obj =>
                Publish(Messages.REQUEST_PROFILE_DATABASE, _profileDB));

            Subscribe(Messages.REQUEST_MATERIALS_DATABASE, obj =>
                Publish(Messages.REQUEST_MATERIALS_DATABASE_RESPONSE, _materialsDB));

            Subscribe(Messages.REQUEST_SERVICES_DATABASE, obj =>
                Publish(Messages.REQUEST_SERVICES_DATABASE_RESPONSE, _servicesDB));

            Subscribe(Messages.REQUEST_SERVICE_MATERIALS_DATABASE, obj =>
                Publish(Messages.REQUEST_SERVICE_MATERIALS_DATABASE_RESPONSE, _serviceMaterialsDB));

            Subscribe(Messages.REQUEST_QUOTES_DATABASE, obj =>
                Publish(Messages.REQUEST_QUOTES_DATABASE_RESPONSE, _quotesDB));

            Subscribe(Messages.REQUEST_CUSTOMERS_DATABASE, obj =>
                Publish(Messages.REQUEST_CUSTOMERS_DATABASE_RESPONSE, _customersDatabase));
        }
    }
}