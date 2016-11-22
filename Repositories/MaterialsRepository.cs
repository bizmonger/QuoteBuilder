using Entities;

namespace Repositories
{
    public class MaterialsRepository : AbstractMaterialsRepository
    {
        public MaterialsRepository()
        {
            MakePromises();
            InitializeDatabase();
            SendRequests();
        }

        protected override void SaveData(Material material) => _database.OnSave(material);

        protected override void Read() => _database.Read();
    }
}