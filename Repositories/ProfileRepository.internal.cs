using Entities;
using Mediation;
using static Bizmonger.Patterns.MessageBus;
using Repositories.Core;

namespace Repositories
{
    public partial class ProfileRepository
    {
        void InitializeDatabase()
        {
            Subscribe(Messages.REQUEST_PROFILE_DATABASE_RESPONSE, obj => _database = obj as IDatabase);
            Publish(Messages.REQUEST_PROFILE_DATABASE);

            _database.Initialize();
        }

        protected override void SaveData(Profile profile) => _database.OnSave(profile);

        protected override void Read() => _database.Read();
    }
}