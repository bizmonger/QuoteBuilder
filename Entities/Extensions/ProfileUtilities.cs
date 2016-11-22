namespace Entities.Utilities
{
    public static class ProfileUtilities
    {
        public static void Update(this Profile modified, Profile existing)
        {
            existing.Address1 = modified.Address1;
            existing.Address2 = modified.Address2;
            existing.City = modified.City;
            existing.State = modified.State;
            existing.Postal = modified.Postal;
            existing.Phone = modified.Phone;
            existing.FirstName = modified.FirstName;
            existing.LastName = modified.LastName;
            existing.Email = modified.Email;
            existing.BusinessName = modified.BusinessName;
        }
    }
}