using Connectivity;
using Entities;

namespace Mediation.Validation
{
    public class ProfileValidator
    {
        public bool Validate(Profile profile) => !string.IsNullOrWhiteSpace(profile.FirstName) &&
                !string.IsNullOrWhiteSpace(profile.LastName) &&
                Connection.IsValidEmailAddress(profile.Email) &&
                !string.IsNullOrWhiteSpace(profile.Phone) &&
                !string.IsNullOrWhiteSpace(profile.BusinessName) &&
                !string.IsNullOrWhiteSpace(profile.State) &&
                !string.IsNullOrWhiteSpace(profile.City) &&
                !string.IsNullOrWhiteSpace(profile.Postal) &&
                !string.IsNullOrWhiteSpace(profile.Address1);
    }
}