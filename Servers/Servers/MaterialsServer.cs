using Entities;
using Mediation;
using static Bizmonger.Patterns.MessageBus;

namespace Servers
{
    public static class MaterialsServer
    {
        public static Material ToMaterial(this string materialId)
        {
            Material material = null;
            SubscribeFirstPublication(Messages.REQUEST_MATERIAL_RESPONSE, payload => material = payload as Material);
            Publish(Messages.REQUEST_MATERIAL, materialId);

            return material;
        }
    }
}