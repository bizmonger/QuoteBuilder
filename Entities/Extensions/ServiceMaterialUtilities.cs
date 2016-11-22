using System;

namespace Entities.Utilities
{
    public static class ServiceMaterialUtilities
    {
        public static void Update(this ServiceMaterial modified, ServiceMaterial existing)
        {
            existing.Id = modified.Id ?? Guid.NewGuid().ToString();
            existing.MaterialId = modified.Id;
            existing.ServiceId = modified.Id ?? Guid.NewGuid().ToString();
            existing.Quantity = modified.Quantity;
            existing.UserId = modified.UserId;
        }
    }
}