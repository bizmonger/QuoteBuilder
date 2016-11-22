namespace Repositories.Core
{
    public partial class Autonomy
    {
        public void Activate()
        {
            _profileRepository = new ProfileRepository();
            _materialsRepository = new MaterialsRepository();
            _servicesRepository = new ServicesRepository();
            _serviceMaterialRepository = new ServiceMaterialsRepository();
            _quotesRepository = new QuotesRepository();
            _customersRepository = new CustomersRepository();
        }
    }
}