using SQLite;

namespace Repositories.Core
{
    public interface IDatabaseConnection
    {
        SQLiteConnection Connect();

        bool TableExists(SQLiteConnection connection, string tableName);
    }
}