using System.Collections.Generic;
using System.Linq;
using Entities;
using Entities.Utilities;
using Mediation;
using static Bizmonger.Patterns.MessageBus;
using Repositories.Core;

namespace Repositories
{
    public abstract partial class AbstractMaterialsRepository
    {
        protected void MakePromises()
        {
            Subscribe(Messages.REQUEST_MATERIALS, OnRequestMaterialsResponse);
            Subscribe(Messages.REQUEST_MATERIAL, OnRequestMaterialResponse);
            Subscribe(Messages.REQUEST_SAVE_MATERIAL, OnSaveMaterial);
        }

        protected void BreakPromises()
        {
            Unsubscribe(Messages.REQUEST_MATERIALS, OnRequestMaterialsResponse);
            Unsubscribe(Messages.REQUEST_MATERIAL, OnRequestMaterialResponse);
            Unsubscribe(Messages.REQUEST_SAVE_MATERIAL, OnSaveMaterial);
        }

        protected void SendRequests()
        {
            Bizmonger.Patterns.MessageBus.Publish(Messages.REQUEST_PROFILE);

            Read();
            Publish();
        }

        protected void Publish()
        {
            if (_materials == null) _materials = new List<Material>();

            Bizmonger.Patterns.MessageBus.Publish(Messages.REQUEST_MATERIALS_RESPONSE, _materials);
        }

        protected void InitializeDatabase()
        {
            Subscribe(Messages.REQUEST_MATERIALS_DATABASE_RESPONSE, obj => _database = obj as IDatabase);
            Bizmonger.Patterns.MessageBus.Publish(Messages.REQUEST_MATERIALS_DATABASE);

            _database.Initialize();
        }

        protected abstract void Read();

        protected bool Save(object entity)
        {
            var material = entity as Material;
            var result = _materials.FirstOrDefault(m => m.Id == material.Id);

            if (result != null)
            {
                material.Update(result);
                SaveData(result);
            }
            else
            {
                SaveData(material);
                _materials.Add(material);
            }
            return true;
        }

        protected abstract void SaveData(Material material);
    }
}