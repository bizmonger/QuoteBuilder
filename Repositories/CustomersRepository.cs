namespace Repositories
{
    public class CustomersRepository : AbstractCustomersRepository
    {
        public CustomersRepository()
        {
            MakePromises();
            InitializeDatabase();
        }
    }
}