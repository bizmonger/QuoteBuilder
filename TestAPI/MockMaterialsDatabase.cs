using System.Collections.Generic;
using System.Linq;
using Entities;
using Entities.Utilities;
using Repositories.Core;
using System.Diagnostics;

namespace TestAPI
{
    [DebuggerNonUserCode]
    public class MockMaterialsDatabase : AbstractMaterialsDatabase
    {
        public List<Material> Materials { get; set; } = new List<Material>();

        public override void Initialize() { }

        protected override IEnumerable<Material> Get(string profileId) =>
            Materials.Where(m => m.UserId == profileId);

        protected override Material ReadFromMaterialId(string materialId) =>
            Materials.FirstOrDefault(m => m.Id == materialId);

        protected override void Add(Material material) =>
            Materials.Add(material);

        protected override void Update(Material material)
        {
            var existing = Materials.FirstOrDefault(m => m.Id == material.Id);
            if (existing == null) return;

            material.Update(existing);
        }
    }
}