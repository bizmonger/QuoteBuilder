using Entities;
using IO;

namespace OperationDependencies
{
    public class ViewQuoteDependencies
    {
        public Customer Customer { get; set; }
        public IRead FileReader { get; set; }
        public string Logo { get; set; }
        public Quote Quote { get; set; }
    }
}