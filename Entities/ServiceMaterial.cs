using SQLite;

namespace Entities
{
    public class ServiceMaterial
    {
        [PrimaryKey]
        public string Id { get; set; }
        public string UserId { get; set; }
        public string ServiceId { get; set; }
        public string MaterialId { get; set; }
        public decimal Quantity { get; set; }
        public bool CloudSynced { get; set; }
    }
}