using System;

namespace Entities
{
    public class Address : EntityBase
    {
        public Guid AddressId { get; set; }
        public string Name { get; set; }        
        public string Address1 { get; set; }        
        public string Address2 { get; set; }        
        public string City { get; set; }        
        public string State { get; set; }        
        public string Postal { get; set; }        
        public Guid UserId { get; set; }        
        public bool CloudSynced { get; set; }
    }
}