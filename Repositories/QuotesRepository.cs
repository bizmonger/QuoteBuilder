using Entities;

namespace Repositories
{
    public class QuotesRepository : AbstractQuotesRepository
    {
        public QuotesRepository()
        {
            MakePromises();
            InitializeDatabase();
            SendRequests();
        }

        protected override void SaveData(Quote quote) => _database.OnSave(quote);
        protected override void Read() => _database.Read();

        protected void GetByCustomerId(string customerId) => _database.Read(customerId);
    }
}