using Connectivity;
using Entities;

namespace Mediation.Validation
{
    public class CustomerValidator
    {
        public bool Validate(Customer customer) => 
                !string.IsNullOrWhiteSpace(customer.FirstName) &&
                !string.IsNullOrWhiteSpace(customer.LastName) &&
                Connection.IsValidEmailAddress(customer.Email) &&
                !string.IsNullOrWhiteSpace(customer.Phone) &&
                !string.IsNullOrWhiteSpace(customer.Email);
    }
}