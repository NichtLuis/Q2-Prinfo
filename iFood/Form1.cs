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
                    UtensilId INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL
                )");
            iFoodDb.Execute(@"
                CREATE TABLE IF NOT EXISTS RezeptLebensmitteln (
                    RezeptId INTEGER NOT NULL,
                    LebensmittelId INTEGER NOT NULL,
                    Menge REAL,
                    FOREIGN KEY (RezeptId) REFERENCES Rezepte(Id),
                    FOREIGN KEY (LebensmittelId) REFERENCES Lebensmittel(Id)
                )");
            iFoodDb.Execute(@"
                CREATE TABLE IF NOT EXISTS RezeptUtensilien (
                    RezeptId INTEGER NOT NULL,
                    UtensilId INTEGER NOT NULL,
                    PRIMARY KEY (RezeptID, UtensilId),
                    FOREIGN KEY (RezeptID) REFERENCES Rezepte(RezeptId),
                    FOREIGN KEY (UtensilId) REFERENCES Utensilien(UtensilId)
                )");
            //SQLAdd();
            SQLInComboBox();
        }


        private void SQLAdd()
        {
            using var conn = iFoodDb.OpenConnection();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "INSERT INTO Lebensmittel (Name) VALUES ($Name)";
            cmd.Parameters.AddWithValue("$Name", "Pizzer");
            cmd.ExecuteNonQuery();
        }
        private void SQLInComboBox()
        {
            using var conn = iFoodDb.OpenConnection();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT Id, Name FROM Lebensmittel";

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int Id = reader.GetInt32(0);
                string Name = reader.GetString(1);
                //string Hersteller = reader.GetString(2);
                CBRezeptAuswahl.Items.Add(Id + " " + Name + " ");
            }
        }
        private void Suche()
        {
            string suche = Convert.ToString(TBSuche.Text);
            if (suche != string.Empty)
            {
                using var conn = iFoodDb.OpenConnection();
                var cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT Name FROM Lebensmittel WHERE Name LIKE $s";
                cmd.Parameters.AddWithValue("$s", "%" + suche + "%");

                using var reader = cmd.ExecuteReader();
                var treffer = new List<string>();
                while (reader.Read())
                {
                    treffer.Add(reader.GetString(0));
                }

                LBErgebnis.Items.Clear();
                if (treffer.Count == 0)
                    LBErgebnis.Items.Add("Keine Treffer");
                else
                    foreach (var t in treffer)
                        LBErgebnis.Items.Add(t);
            }
        }

        private void BTSuche_Click(object sender, EventArgs e)
        {
            Suche();
        }

    }
}