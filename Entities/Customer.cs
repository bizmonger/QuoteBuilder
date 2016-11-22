using SQLite;

namespace Entities
{
    public class Customer : EntityBase
    {
        [PrimaryKey]
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Description { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool CloudSynced { get; set; }
    }
}