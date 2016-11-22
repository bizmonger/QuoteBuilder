using System.Collections.Generic;
using System.Linq;
using Entities;
using SQLite;
using Xamarin.Forms;
using Repositories.Core;

namespace Repositories.Details
{
    public class MaterialsDatabase : AbstractMaterialsDatabase
    {
        SQLiteConnection _databaseConnection = null;

        protected override Material ReadFromMaterialId(string materialId) =>
            _databaseConnection.Table<Material>().FirstOrDefault(m => m.Id == materialId);

        protected override IEnumerable<Material> Get(string profileId) =>
            _databaseConnection.Table<Material>().Where(m => m.UserId == profileId);

        public override void Initialize()
        {
            _databaseConnection = DependencyService.Get<IDatabaseConnection>().Connect();

            var tableExists = DependencyService.Get<IDatabaseConnection>().TableExists(_databaseConnection, "Material");

            if (!tableExists)
            {
                _databaseConnection.CreateTable<Material>();
            }
        }

        protected override void Update(Material material) => _databaseConnection.Update(material);

        protected override void Add(Material material) => _databaseConnection.Insert(material);
    }
}