namespace Entities.Utilities
{
    public static class CustomerUtilities
    {
        public static void Update(this Customer modified, Customer source)
        {
            source.UserId = modified.UserId;
            source.FirstName = modified.FirstName;
            source.LastName = modified.LastName;
            source.Phone = modified.Phone;
            source.Email = modified.Email;
            source.Description = modified.Description;
        }
    }
}