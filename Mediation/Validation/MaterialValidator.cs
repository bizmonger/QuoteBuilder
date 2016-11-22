using Entities;

namespace Mediation.Validation
{
    public class MaterialValidator
    {
        public bool Validate(Material material) => 
                    !string.IsNullOrWhiteSpace(material.Name) &&
                    !string.IsNullOrWhiteSpace(material.UnitType) &&
                    material.BaseCost >= 0 &&
                    material.Quantity >= 0 &&
                    material.MarkupPrice >= 0;
    }
}