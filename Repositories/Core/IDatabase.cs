namespace Repositories.Core
{
    public interface IDatabase
    {
        void Initialize();
        void OnSave(object entity);
        void Read(string id);
        void Read();
    }
}