namespace Repositories
{
    public partial class ProfileRepository : AbstractProfileRepository
    {
        public ProfileRepository()
        {
            MakePromises();
            InitializeDatabase();
            SendRequests();
        }
    }
}