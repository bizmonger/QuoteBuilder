using System.IO;
using System.Linq;
using Bizmonger.Patterns; using static Bizmonger.Patterns.MessageBus; using Repositories.Core;
using SQLite;
using Xamarin.Forms;
using QuoteBuilder.Droid;

[assembly: Dependency(typeof(Database_Android))]
namespace QuoteBuilder.Droid
{
    public class Database_Android : IDatabaseConnection
    {
        public SQLiteConnection Connect()
        {
            var fileName = "QuoteBuilder_SQLite.db3";
            var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

            var path = Path.Combine(documentsPath, fileName);
            var connection = new SQLiteConnection(path);

            return connection;
        }

        public bool TableExists(SQLiteConnection connection, string tableName) => connection.GetTableInfo(tableName).Any();
    }
}