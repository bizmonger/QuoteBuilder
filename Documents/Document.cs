using Entities;

namespace Documents
{
    public class Document
    {
        public Statement Statement { get; set; }
        public Customer Customer { get; set; }
        public string File { get; set; }
        public string Logo { get; set; }
    }
}