namespace iFood
{
    public partial class Form1 : Form
    {
        // Felder für deine DBs
        private DbManager gerichteDb;
        private DbManager bestellungenDb;

        public Form1()
        {
            InitializeComponent();

            // DBs initialisieren
            gerichteDb = new DbManager("gerichte.db");
            bestellungenDb = new DbManager("bestellungen.db");

            // Tabellen anlegen (nur beim ersten Start nötig, schadet aber nicht)
            gerichteDb.Execute(@"
                CREATE TABLE IF NOT EXISTS Gerichte (
                    Id    INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name  TEXT NOT NULL,
                    Preis REAL
                )");

            bestellungenDb.Execute(@"
                CREATE TABLE IF NOT EXISTS Bestellungen (
                    Id        INTEGER PRIMARY KEY AUTOINCREMENT,
                    GerichtId INTEGER,
                    Menge     INTEGER
                )");
        }

        // Beispiel: Button-Click zum Einfügen
        private void btnAddGericht_Click(object sender, EventArgs e)
        {
            using var conn = gerichteDb.OpenConnection();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "INSERT INTO Gerichte (Name, Preis) VALUES ($name, $preis)";
            cmd.Parameters.AddWithValue("$name", "Pizza Margherita");
            cmd.Parameters.AddWithValue("$preis", 8.50);
            cmd.ExecuteNonQuery();

            MessageBox.Show("Gericht hinzugefügt!");
        }
    }
}