using SQLite;

namespace Entities
{
    public class Quote : Statement
    {
        [PrimaryKey]
        public string Id { get; set; }
    }
}