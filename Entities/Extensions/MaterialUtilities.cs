namespace Entities.Utilities
{
    public static class MaterialUtilities
    {
        public static void Update(this Material modified, Material source)
        {
            source.UserId = modified.UserId;
            source.Name = modified.Name;
            source.Quantity = modified.Quantity;
            source.UnitType = modified.UnitType;
            source.MarkupPrice = modified.MarkupPrice;
            source.BaseCost = modified.BaseCost;
            source.Description = modified.Description;
        }
    }
}