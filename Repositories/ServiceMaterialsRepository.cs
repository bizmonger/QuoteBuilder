using Entities;
using Mediation;
using static Bizmonger.Patterns.MessageBus;

namespace Repositories
{
    public class ServiceMaterialsRepository : AbstractServiceMaterialsRepository
    {
        public ServiceMaterialsRepository()
        {
            MakePromises();
            InitializeDatabase();
            SendRequests();
        }

        protected override void SaveData(ServiceMaterial serviceMaterial) => _database.OnSave(serviceMaterial);

        protected override void Read() => _database.Read();

        //public static Material GetMaterial(string serviceMaterialId)
        //{
        //    Material material = null;
        //    Subscribe(Messages.REQUEST_MATERIAL_RESPONSE, payload => material = payload as Material);
        //    Publish(Messages.REQUEST_MATERIAL, serviceMaterialId);
        //    return material;
        //}
    }
}