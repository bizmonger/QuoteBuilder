using Entities;

namespace Repositories
{
    public class ServicesRepository : AbstractServicesRepository
    {
        public ServicesRepository()
        {
            MakePromises();
            InitializeDatabase();
            SendRequests();
        }

        protected override void SaveData(Service service) => _database.OnSave(service);
        
        protected override void Read() => _database.Read();
    }
}