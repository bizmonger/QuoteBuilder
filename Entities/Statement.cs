using SQLite;
using System;
using System.Collections.Generic;

namespace Entities
{
    public class Statement : EntityBase
    {
        public string TypeName { get; set; }
        public int StatementNumber { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        [Ignore]
        public List<Service> Services { get; set; }
        public decimal MaterialsCost { get; set; }
        public decimal LaborCost { get; set; }
        public decimal Tax { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }
        public string CustomerId { get; set; }
        [Ignore]
        public Address Address { get; set; }
        public bool CloudSynced { get; set; }
        public string UserId { get; set; }
        [Ignore]
        public Profile Profile { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}