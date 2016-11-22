using System.Collections.Generic;
using Entities;
using SQLite;
using Repositories.Core;

namespace Repositories
{
    public partial class AbstractCustomersRepository
    {
        protected List<Customer> _services = new List<Customer>();
        protected SQLiteConnection _databaseConnection = null;
        protected IDatabase _database = null;
    }
}