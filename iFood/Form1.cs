using System.Web;

namespace iFood
{
    public partial class Form1 : Form
    {
        private DbManager iFoodDb;
        List<TextBox> TBNaherwerte = new List<TextBox>();

        public Form1()
        {
            InitializeComponent();

            iFoodDb = new DbManager("ifood.db");
            foreach (Object Obj in GBNaehrwerte.Controls)
            {
                if (Obj is TextBox)
                {
                    TBNaherwerte.Add(Obj as TextBox);
                }
            }
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
            //SQLInComboBox();
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
            cmd.CommandText = "SELECT Name FROM Lebensmittel WHERE Name LIKE $s";
            cmd.Parameters.AddWithValue("$s", "%" + TBSuche.Text + "%");

            LBErgebnis.Items.Clear();
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                LBErgebnis.Items.Add(reader.GetString(0));
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
        private void Ausgabe(string Lebensmittel)
        {
            using var conn = iFoodDb.OpenConnection();
            var cmd = conn.CreateCommand();

            cmd.Parameters.Clear();
            cmd.CommandText = "SELECT Id FROM Lebensmittel WHERE Name LIKE $s LIMIT 1";
            cmd.Parameters.AddWithValue("$s", "%" + Lebensmittel + "%");
            var result = cmd.ExecuteScalar();
            int id = result == null ? -1 : Convert.ToInt32(result);

            cmd.Parameters.Clear();
            cmd.CommandText = "SELECT Kalorien FROM Naehrwerte WHERE LebensmittelID = $id";
            cmd.Parameters.AddWithValue("$id", id);
            result = cmd.ExecuteScalar();
            TBKalorien.Text = result == null ? "" : result.ToString();

            cmd.Parameters.Clear();
            cmd.CommandText = "SELECT Kohlenhydrate FROM Naehrwerte WHERE LebensmittelID = $id";
            cmd.Parameters.AddWithValue("$id", id);
            result = cmd.ExecuteScalar();
            TBKohlenhydrate.Text = result == null ? "" : result.ToString();

            cmd.Parameters.Clear();
            cmd.CommandText = "SELECT DavonZucker FROM Naehrwerte WHERE LebensmittelID = $id";
            cmd.Parameters.AddWithValue("$id", id);
            result = cmd.ExecuteScalar();
            TBZucker.Text = result == null ? "" : result.ToString();

            cmd.Parameters.Clear();
            cmd.CommandText = "SELECT Fett FROM Naehrwerte WHERE LebensmittelID = $id";
            cmd.Parameters.AddWithValue("$id", id);
            result = cmd.ExecuteScalar();
            TBFett.Text = result == null ? "" : result.ToString();

            cmd.Parameters.Clear();
            cmd.CommandText = "SELECT DavonGesFettsaeuren FROM Naehrwerte WHERE LebensmittelID = $id";
            cmd.Parameters.AddWithValue("$id", id);
            result = cmd.ExecuteScalar();
            TBDavonGesaettigteFettsaeuren.Text = result == null ? "" : result.ToString();

            cmd.Parameters.Clear();
            cmd.CommandText = "SELECT Eiweiss FROM Naehrwerte WHERE LebensmittelID = $id";
            cmd.Parameters.AddWithValue("$id", id);
            result = cmd.ExecuteScalar();
            TBEiweiss.Text = result == null ? "" : result.ToString();

            cmd.Parameters.Clear();
            cmd.CommandText = "SELECT Salz FROM Naehrwerte WHERE LebensmittelID = $id";
            cmd.Parameters.AddWithValue("$id", id);
            result = cmd.ExecuteScalar();
            TBSalz.Text = result == null ? "" : result.ToString();

        }
        private void BTSuche_Click(object sender, EventArgs e)
        {
            LBErgebnis.Items.Clear();
            bool block = false;
            foreach (TextBox TB in TBNaherwerte)
            {
                if (TB.Text == "")
                {
                    block = false;
                }
                else
                {
                    block = true;
                    break;
                }
            }
            if (block == false)  // Suche Nach name
            {
                SQLInComboBox();
            }
            if (block == true)   //Suche nach Naehrwert
            {
                string gesucht = null;
                int anzahl = 0;
                if (TBKalorien.Text != "")
                {
                    anzahl = Convert.ToInt32(TBKalorien.Text);
                    gesucht = "Kalorien";
                }
                if (TBKohlenhydrate.Text != "")
                {
                    anzahl = Convert.ToInt32(TBKohlenhydrate.Text);
                    gesucht = "Kohlenhydrate";
                }
                if (TBZucker.Text != "")
                {
                    anzahl = Convert.ToInt32(TBZucker.Text);
                    gesucht = "DavonZucker";
                }
                if (TBFett.Text != "")
                {
                    anzahl = Convert.ToInt32(TBFett.Text);
                    gesucht = "Fett";
                }
                if (TBDavonGesaettigteFettsaeuren.Text != "")
                {
                    anzahl = Convert.ToInt32(TBDavonGesaettigteFettsaeuren.Text);
                    gesucht = "DavonGesFettsaeuren";
                }
                if (TBEiweiss.Text != "")
                {
                    anzahl = Convert.ToInt32(TBEiweiss.Text);
                    gesucht = "Eiweiss";
                }
                if (TBSalz.Text != "")
                {
                    anzahl = Convert.ToInt32(TBSalz.Text);
                    gesucht = "Salz";
                }
                SucheNachAtt(gesucht, anzahl);
            }
        }
        private void SucheNachAtt(string Attribute, int Anzahl)
        {
            // Whitelist — nur diese Spaltennamen sind erlaubt
            var erlaubt = new HashSet<string> {
        "Kalorien", "Kohlenhydrate", "DavonZucker",
        "Fett", "DavonGesFettsaeuren", "Eiweiss", "Salz"
    };
            if (!erlaubt.Contains(Attribute)) return;

            using var conn = iFoodDb.OpenConnection();
            var cmd = conn.CreateCommand();
            cmd.CommandText = $@"
        SELECT L.Name
        FROM Lebensmittel L
        JOIN Naehrwerte N ON N.LebensmittelID = L.Id
        WHERE N.{Attribute} = $wert";
            cmd.Parameters.AddWithValue("$wert", Anzahl);

            LBErgebnis.Items.Clear();
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                LBErgebnis.Items.Add(reader.GetString(0));
            }
        }

        private void LBErgebnis_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Item = Convert.ToString(LBErgebnis.SelectedItem);
            Ausgabe(Item);
        }
    }
}