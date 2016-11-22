using System.Linq;
using Entities;
using SQLite;
using Xamarin.Forms;
using Repositories.Core;

namespace Repositories.Details
{
    public class ProfileDatabase : AbstractProfileDatabase
    {
        public ProfileDatabase()
        {

        }
        SQLiteConnection _databaseConnection = null;

        public override void Initialize()
        {
            _databaseConnection = DependencyService.Get<IDatabaseConnection>().Connect();

            var tableExists = DependencyService.Get<IDatabaseConnection>().TableExists(_databaseConnection, "Profile");

            if (!tableExists)
            {
                _databaseConnection.CreateTable<Profile>();
            }
        }

        protected override Profile ExecuteReadFromProfileId() =>
            _databaseConnection.Table<Profile>().SingleOrDefault();

        protected override void Update(Profile profile) => _databaseConnection.Update(profile);

        protected override void Add(Profile profile) => _databaseConnection.Insert(profile);
    }
}