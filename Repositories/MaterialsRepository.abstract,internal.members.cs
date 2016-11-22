using System.Collections.Generic;
using Entities;
using SQLite;
using Bizmonger.Patterns; using static Bizmonger.Patterns.MessageBus; using Repositories.Core;

namespace Repositories
{
    public partial class AbstractMaterialsRepository
    {
        protected List<Material> _materials = new List<Material>();
        protected SQLiteConnection _databaseConnection = null;
        protected IDatabase _database = null;
    }
}