using Mediation;
using Repositories.Details;
using static Bizmonger.Patterns.MessageBus;

namespace Messaging.Databases
{
    public class PromiseProfileDB
    {
        readonly ProfileDatabase _profileDB = new ProfileDatabase();

        public void Execute() =>
            Subscribe(Messages.REQUEST_PROFILE_DATABASE, OnRequestProfileDB);

        public void Revert() =>
            Unsubscribe(Messages.REQUEST_PROFILE_DATABASE, OnRequestProfileDB);

        void OnRequestProfileDB(object obj) =>
            Publish(Messages.REQUEST_PROFILE_DATABASE_RESPONSE, _profileDB);
    }
}