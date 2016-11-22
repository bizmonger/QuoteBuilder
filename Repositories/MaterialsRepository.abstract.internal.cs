using Entities;
using Mediation;

namespace Repositories
{
    public partial class AbstractMaterialsRepository
    {
        void OnSaveMaterial(object obj) => Bizmonger.Patterns.MessageBus.Publish(Messages.REQUEST_SAVE_MATERIAL_RESPONSE, Save(obj));

        void OnRequestMaterialsResponse(object obj) => _database.Read();

        protected void OnRequestMaterialResponse(object obj)
        {
            var id = obj as string;
            _database.Read(id);
        }
    }
}