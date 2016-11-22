using OperationDependencies;

namespace Documents
{
    public interface IDocumentGenerator
    {
        string ExecuteAsync(ViewQuoteDependencies dependencies);
    }
}