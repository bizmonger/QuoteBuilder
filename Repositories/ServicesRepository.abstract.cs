using Entities;
using Mediation;
using static Bizmonger.Patterns.MessageBus;
using Repositories.Core;

namespace Repositories
{
    public abstract partial class AbstractServicesRepository : AbstractPromise
    {
        protected override void MakePromises()
        {
            Subscribe(Messages.REQUEST_SAVE_SERVICE, OnSaveService);
            Subscribe(Messages.REQUEST_SERVICE, OnRequestService);
            Subscribe(Messages.REQUEST_SERVICES, OnRequestServices);
            Subscribe(Messages.SERVICE_MATERIAL_ADDED, OnServiceMaterialAdded);
        }

        protected override void BreakPromises()
        {
            Unsubscribe(Messages.REQUEST_SAVE_SERVICE, OnSaveService);
            Unsubscribe(Messages.REQUEST_SERVICE, OnRequestService);
            Unsubscribe(Messages.REQUEST_SERVICES, OnRequestServices);
            Unsubscribe(Messages.SERVICE_MATERIAL_ADDED, OnServiceMaterialAdded);
        }

        protected void SendRequests() => Read();

        protected abstract void SaveData(Service service);

        protected abstract void Read();
    }
}