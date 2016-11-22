using Entities;
using System.Collections.Generic;

namespace Repositories.Core
{
    public abstract class AbstractQuotesDatabase : IDatabase
    {
        public abstract void Read(string id);

        public abstract void Initialize();

        public abstract void OnSave(object entity);

        public abstract void Read();

        protected abstract IEnumerable<Quote> ExecuteQueryStrategy(string profileId);
    }
}