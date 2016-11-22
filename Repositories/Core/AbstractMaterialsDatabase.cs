using System;
using System.Collections.Generic;
using Entities;
using Mediation;
using Servers;
using static Bizmonger.Patterns.MessageBus;

namespace Repositories.Core
{
    public abstract class AbstractMaterialsDatabase : IDatabase
    {
        public void Read(string id)
        {
            var material = ReadFromMaterialId(id);
            Publish(Messages.REQUEST_MATERIAL_RESPONSE, material);
        }

        public abstract void Initialize();

        public void OnSave(object entity)
        {
            var material = entity as Material;
            var existingMaterial = ReadFromMaterialId(material.Id);

            if (existingMaterial != null)
            {
                Update(material);
            }
            else
            {
                material.Id = Guid.NewGuid().ToString();
                material.UserId = new ProfileServer().GetProfile().Id;

                Add(material);
                Publish(Messages.MATERIAL_ADDED, material);
            }
        }

        public void Read()
        {
            var profile = new ProfileServer().GetProfile();
            var materials = Get(profile.Id);

            Publish(Messages.REQUEST_MATERIALS_RESPONSE, materials);
        }

        protected abstract Material ReadFromMaterialId(string materialId);

        protected abstract void Update(Material material);

        protected abstract void Add(Material material);
        protected abstract IEnumerable<Material> Get(string profileId);
    }
}