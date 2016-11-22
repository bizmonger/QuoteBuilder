using System.Collections.Generic;

namespace Entities.Utilities
{
    public static class ServiceUtilities
    {
        public static void Update(this Service modified, Service existing)
        {
            existing.Description = modified.Description;
            existing.LaborCost = modified.LaborCost;
            existing.Materials = modified.Materials;
            existing.Name = modified.Name;
            existing.ServiceMaterials = modified.ServiceMaterials;
            existing.TaxPercentage = modified.TaxPercentage;
            existing.UserId = modified.UserId;
        }

        public static decimal TotalCost(this Service source)
        {
            var labor = source.LaborCost;
            var materialsCost = source.Materials.Cost();

            return (labor + materialsCost);
        }

        public static decimal Cost(this IEnumerable<Material> source)
        {
            decimal materialsCost = 0;

            foreach (var material in source)
            {
                materialsCost += (material.MarkupPrice * material.Quantity);
            }

            return materialsCost;
        }
    }
}