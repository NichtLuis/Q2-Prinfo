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
        }

        // Beispiel: Button-Click zum Einfügen
        private void btnAddGericht_Click(object sender, EventArgs e)
        {
            //using var conn = gerichteDb.OpenConnection();
            //var cmd = conn.CreateCommand();
            //cmd.CommandText = "INSERT INTO Gerichte (Name, Preis) VALUES ($name, $preis)";
            //cmd.Parameters.AddWithValue("$name", "Pizza Margherita");
            //cmd.Parameters.AddWithValue("$preis", 8.50);
            //cmd.ExecuteNonQuery();

            //MessageBox.Show("Gericht hinzugefügt!");
        }
    }
}