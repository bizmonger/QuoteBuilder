using SQLite;
using System;

namespace Entities
{
    public class Invoice : Quote
    {
        [PrimaryKey]
        public Guid InvoiceId { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsPaid { get; set; }
        public bool IsActive { get; set; }
        public DateTime? DateClosed { get; set; }
    }
}
