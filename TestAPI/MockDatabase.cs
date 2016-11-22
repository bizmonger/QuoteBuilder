using System.Diagnostics;
using Repositories.Core;

namespace TestAPI
{
    [DebuggerNonUserCode]
    public class MockDatabase : IDatabase
    {
        public virtual void Initialize() => Debug.WriteLine("MockDatabase::Initialize()");

        public virtual void Read() => Debug.WriteLine("MockDatabase::Read()");

        public virtual void OnSave(object entity) => Debug.WriteLine("MockDatabase::OnSave()");

        public virtual void Read(string id) => Debug.WriteLine("MockDatabase::Get()");
    }
}