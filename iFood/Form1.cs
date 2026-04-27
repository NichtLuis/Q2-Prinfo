namespace iFood
{
    public partial class Form1 : Form
    {
        private DbManager iFoodDb;

        public Form1()
        {
            InitializeComponent();

            iFoodDb = new DbManager("ifood.db");

            // Tabellen anlegen (nur beim ersten Start nötig, schadet aber nicht)
            iFoodDb.Execute(@"
                CREATE TABLE IF NOT EXISTS Lebensmittel (
                    Id    INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name  TEXT NOT NULL,
                    Preis REAL
                )");
            SQLAdd();
            SQLInComboBox();
        }

    
        private void SQLAdd()
        {
            using var conn = iFoodDb.OpenConnection();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "INSERT INTO Lebensmittel (Name, Preis) VALUES ($Name, $Preis)";
            cmd.Parameters.AddWithValue("$Name", "Pizzer");
            cmd.Parameters.AddWithValue("$Preis", 80);
            cmd.ExecuteNonQuery();
        }
        private void SQLInComboBox()
        {
            using var conn = iFoodDb.OpenConnection();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT Id, Name, Preis FROM Lebensmittel";

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int Id = reader.GetInt32(0);
                string Name = reader.GetString(1);
                int Preis = reader.GetInt32(2);
                MessageBox.Show(Id + " " + Name + " " + Preis);
            }
        }
    }
}