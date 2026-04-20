using Microsoft.Data.Sqlite;
using System.IO;

namespace iFood
{
    internal class DbManager
    {
        private readonly string _connectionString;

        public DbManager(string fileName)
        {
            // Ordner "Databases" neben der .exe anlegen
            string folder = Path.Combine(AppContext.BaseDirectory, "Databases");
            Directory.CreateDirectory(folder);

            string fullPath = Path.Combine(folder, fileName);
            _connectionString = $"Data Source={fullPath}";
        }

        public SqliteConnection OpenConnection()
        {
            var conn = new SqliteConnection(_connectionString);
            conn.Open();
            return conn;
        }

        // Hilfsmethode: SQL ohne Rückgabe ausführen (CREATE, INSERT, UPDATE, DELETE)
        public void Execute(string sql)
        {
            using var conn = OpenConnection();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
        }
    }
}