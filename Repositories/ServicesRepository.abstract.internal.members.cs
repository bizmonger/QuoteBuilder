using Entities;
using Repositories.Core;
using SQLite;
using System.Collections.Generic;

namespace Repositories
{
    public partial class AbstractServicesRepository
    {
        protected List<Service> _services = new List<Service>();
        protected SQLiteConnection _databaseConnection = null;
        protected IDatabase _database = null;
    }
}