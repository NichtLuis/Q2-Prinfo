using Microsoft.Data.Sqlite;
using System.IO;
using System.Runtime.CompilerServices;

namespace iFood
{
    internal class DbManager
    {
        private readonly string _connectionString;

        public DbManager(string fileName)
        {
            string folder = Path.Combine(GetDatabasesFolder(), "");
            Directory.CreateDirectory(folder);

            string fullPath = Path.Combine(folder, fileName);
            _connectionString = $"Data Source={fullPath}";
        }

        private static string GetDatabasesFolder()
        {
#if DEBUG
            // Im Debug: direkt in den Projektordner schreiben (für Git-Sync)
            string projectDir = GetProjectDirectory();
            return Path.Combine(projectDir, "Databases");
#else
            // Im Release: neben die .exe (normales Verhalten)
            return Path.Combine(AppContext.BaseDirectory, "Databases");
#endif
        }

        // Liefert den Ordner, in dem diese .cs-Datei zur Compile-Zeit lag
        private static string GetProjectDirectory([CallerFilePath] string? thisFile = null)
        {
            return Path.GetDirectoryName(thisFile!)!;
        }

        public SqliteConnection OpenConnection()
        {
            var conn = new SqliteConnection(_connectionString);
            conn.Open();
            return conn;
        }

        public void Execute(string sql)
        {
            using var conn = OpenConnection();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
        }
    }
}