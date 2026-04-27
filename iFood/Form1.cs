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
                    Hersteller TEXT
                )");


            iFoodDb.Execute(@"
                CREATE TABLE IF NOT EXISTS Naehrwerte (
                    Id    INTEGER PRIMARY KEY AUTOINCREMENT,
                    LebensmittelID INTEGER NOT NULL UNIQUE,
                    Kalorien REAL,
                    Kohlenhydrate REAL,
                    DavonZucker REAL,
                    Fett REAL,
                    DavonGesFettsaeuren REAL,
                    Eiweiss REAL,
                    Salz REAL,
                    FOREIGN KEY (LebensmittelID) REFERENCES Lebensmittel(Id)
                )");
            iFoodDb.Execute(@"
                CREATE TABLE IF NOT EXISTS Rezepte (
                    Id              INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name            TEXT NOT NULL,
                    Dauer           INTEGER,
                    Anleitung       TEXT,
                    AnzahlPortionen INTEGER

                )");
            iFoodDb.Execute(@"
                CREATE TABLE IF NOT EXISTS Utensilien
                (
                    Name TEXT NOT NULL
                )");
            iFoodDb.Execute(@"
                CREATE TABLE IF NOT EXISTS RezeptLebensmitteln (
                    RezeptID INTEGER NOT NULL,
                    LebensmittelID INTEGER NOT NULL,
                    Menge REAL,
                    FOREIGN KEY (RezeptID) REFERENCES Rezepte(Id),
                    FOREIGN KEY (LebensmittelID) REFERENCES Lebensmittel(Id)
                )");
            iFoodDb.Execute(@"
                CREATE TABLE IF NOT EXISTS RezeptUtensilien (
                    RezeptID INTEGER NOT NULL,
                    UtensilName TEXT NOT NULL,
                    PRIMARY KEY (RezeptID, UtensilId),
                    FOREIGN KEY (RezeptID) REFERENCES Rezepte(Id),
                    FOREIGN KEY (UtensilName) REFERENCES Utensilien(Name)
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