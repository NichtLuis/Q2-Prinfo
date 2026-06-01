using System.Web;

namespace iFood
{
    public partial class Form1 : Form
    {
        private DbManager iFoodDb;
        List<TextBox> TBNaherwerte = new List<TextBox>();
        private static readonly Dictionary<string, double> Toleranzen = new()
        {
            { "Kalorien",      25.0 },  // kcal
            { "Eiweiss",        2.0 },  // g
            { "Fett",           2.0 },  // g
            { "DavonGesFettsaeuren",           2.0 },  // g
            { "Kohlenhydrate",  2.0 },  // g
            { "DavonZucker",         2.0 },  // g
            { "Salz",           0.2 },  // g
        };

        private int _aktuellesRezeptId = -1;

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
            BTUtensielienHinzufuegen.Click += BTHinzufuegen_Click;
            BTUtensilienLoeschen.Click += BTUtensilienLoeschen_Click;
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
            TBKalorien.Text = result == null || result == DBNull.Value
                ? ""
                : Math.Round(Convert.ToDouble(result), 1).ToString();

            cmd.Parameters.Clear();
            cmd.CommandText = "SELECT Kohlenhydrate FROM Naehrwerte WHERE LebensmittelID = $id";
            cmd.Parameters.AddWithValue("$id", id);
            result = cmd.ExecuteScalar();
            TBKohlenhydrate.Text = result == null || result == DBNull.Value
                ? ""
                : Math.Round(Convert.ToDouble(result), 1).ToString();

            cmd.Parameters.Clear();
            cmd.CommandText = "SELECT DavonZucker FROM Naehrwerte WHERE LebensmittelID = $id";
            cmd.Parameters.AddWithValue("$id", id);
            result = cmd.ExecuteScalar();
            TBZucker.Text = result == null || result == DBNull.Value
                ? ""
                : Math.Round(Convert.ToDouble(result), 1).ToString();

            cmd.Parameters.Clear();
            cmd.CommandText = "SELECT Fett FROM Naehrwerte WHERE LebensmittelID = $id";
            cmd.Parameters.AddWithValue("$id", id);
            result = cmd.ExecuteScalar();
            TBFett.Text = result == null || result == DBNull.Value
                ? ""
                : Math.Round(Convert.ToDouble(result), 1).ToString();

            cmd.Parameters.Clear();
            cmd.CommandText = "SELECT DavonGesFettsaeuren FROM Naehrwerte WHERE LebensmittelID = $id";
            cmd.Parameters.AddWithValue("$id", id);
            result = cmd.ExecuteScalar();
            TBDavonGesaettigteFettsaeuren.Text = result == null || result == DBNull.Value
                ? ""
                : Math.Round(Convert.ToDouble(result), 1).ToString();

            cmd.Parameters.Clear();
            cmd.CommandText = "SELECT Eiweiss FROM Naehrwerte WHERE LebensmittelID = $id";
            cmd.Parameters.AddWithValue("$id", id);
            result = cmd.ExecuteScalar();
            TBEiweiss.Text = result == null || result == DBNull.Value
                ? ""
                : Math.Round(Convert.ToDouble(result), 1).ToString();

            cmd.Parameters.Clear();
            cmd.CommandText = "SELECT Salz FROM Naehrwerte WHERE LebensmittelID = $id";
            cmd.Parameters.AddWithValue("$id", id);
            result = cmd.ExecuteScalar();
            TBSalz.Text = result == null || result == DBNull.Value
                ? ""
                : Math.Round(Convert.ToDouble(result), 1).ToString();

        }
        private void BTSuche_Click(object sender, EventArgs e)
        {
            foreach (TextBox TB in TBNaherwerte)
            {
                TB.Text = "";
            }
            TBSuche.Text = "";
            if (TBSuche.Text.Length > 2)
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
                    double anzahl = 0;
                    if (TBKalorien.Text != "")
                    {
                        anzahl = Convert.ToDouble(TBKalorien.Text);
                        gesucht = "Kalorien";
                    }
                    if (TBKohlenhydrate.Text != "")
                    {
                        anzahl = Convert.ToDouble(TBKohlenhydrate.Text);
                        gesucht = "Kohlenhydrate";
                    }
                    if (TBZucker.Text != "")
                    {
                        anzahl = Convert.ToDouble(TBZucker.Text);
                        gesucht = "DavonZucker";
                    }
                    if (TBFett.Text != "")
                    {
                        anzahl = Convert.ToDouble(TBFett.Text);
                        gesucht = "Fett";
                    }
                    if (TBDavonGesaettigteFettsaeuren.Text != "")
                    {
                        anzahl = Convert.ToDouble(TBDavonGesaettigteFettsaeuren.Text);
                        gesucht = "DavonGesFettsaeuren";
                    }
                    if (TBEiweiss.Text != "")
                    {
                        anzahl = Convert.ToDouble(TBEiweiss.Text);
                        gesucht = "Eiweiss";
                    }
                    if (TBSalz.Text != "")
                    {
                        anzahl = Convert.ToDouble(TBSalz.Text);
                        gesucht = "Salz";
                    }
                    SucheNachAtt(gesucht, anzahl);
                }
            }
            else
            {
                LBErgebnis.Items.Clear();
            }
        }
        private void SucheNachAtt(string Attribute, double Anzahl)
        {
            // Whitelist — nur diese Spaltennamen sind erlaubt
            var erlaubt = new HashSet<string> {
        "Kalorien", "Kohlenhydrate", "DavonZucker",
        "Fett", "DavonGesFettsaeuren", "Eiweiss", "Salz"
    };
            if (!Toleranzen.TryGetValue(Attribute, out var toleranz))
                throw new ArgumentException("Ungültiges Attribut");
            if (!erlaubt.Contains(Attribute)) return;

            using var conn = iFoodDb.OpenConnection();
            var cmd = conn.CreateCommand();
            cmd.CommandText = $@"
        SELECT L.Name
        FROM Lebensmittel L
        JOIN Naehrwerte N ON N.LebensmittelID = L.Id
        WHERE N.{Attribute} BETWEEN $wert - $tol AND $wert + $tol";
            cmd.Parameters.AddWithValue("$wert", Anzahl);
            cmd.Parameters.AddWithValue("$tol", toleranz);

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
        private void BTDatenLoeschen_Click(object sender, EventArgs e)
        {
            foreach (TextBox TB in TBNaherwerte)
            {
                TB.Text = "";
            }
            TBSuche.Text = "";
        }
        private void UtensilienListeLaden()
        {
            LBUtensilien.Items.Clear();
            CBUtensilienHinzufuegen.Items.Clear();
            using var conn = iFoodDb.OpenConnection();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT Name FROM Utensilien ORDER BY Name";
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string n = reader.GetString(0);
                LBUtensilien.Items.Add(n);
                CBUtensilienHinzufuegen.Items.Add(n);
            }
        }
        private void BTHinzufuegen_Click(object sender, EventArgs e)
        {
            string name = TBUtensielName.Text.Trim();
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Bitte geben Sie einen Namen für das Utensil ein.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            using var conn = iFoodDb.OpenConnection(); // Verbindung öffnen
            var cmd = conn.CreateCommand();
            cmd.CommandText = "INSERT OR IGNORE INTO Utensilien (Name) VALUES ($Name)"; // Verhindert doppelte Einträge
            cmd.Parameters.AddWithValue("$Name", name);
            int rows = cmd.ExecuteNonQuery();    // nur einmal
            if (rows == 0)
                MessageBox.Show($"'{name}' existiert bereits.", "Hinweis", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                TBUtensielName.Clear();
                UtensilienListeLaden();
            }
        }
        private void BTUtensilienLoeschen_Click(object sender, EventArgs e)
        {
            string name = Convert.ToString(LBUtensilien.SelectedItem);
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Bitte wählen Sie ein Utensil aus der Liste aus.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            using var conn = iFoodDb.OpenConnection();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "DELETE FROM Utensilien WHERE Name = $Name";
            cmd.Parameters.AddWithValue("$Name", name);
            int rows = cmd.ExecuteNonQuery();
            if (rows == 0)
            {
                MessageBox.Show("Das Utensil konnte nicht gelöscht werden.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                UtensilienListeLaden();
            }
        }

        private void BTDatenEntfernen_Click(object sender, EventArgs e)
        {

        }

        private void BTDatenHinzufuegen_Click(object sender, EventArgs e)
        {

        }

        private void BTRezepteUtensilienHinzufuegen_Click(object sender, EventArgs e)
        {
            if ((CBUtensilienHinzufuegen.SelectedItem == null)) return;

            string utensilName = CBUtensilienHinzufuegen.SelectedItem.ToString();
            if (!string.IsNullOrEmpty(utensilName))
            {
                if (_aktuellesRezeptId < 0)
                {
                    MessageBox.Show("Bitte zuerst das Rezept speichern.", "Hinweis",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                using var conn = iFoodDb.OpenConnection();
                var cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT UtensilId FROM Utensilien WHERE Name = $Name";
                cmd.Parameters.AddWithValue("$Name", utensilName);
                var result = cmd.ExecuteScalar();

                if (result == null)
                {
                    MessageBox.Show("Das ausgewählte Utensil existiert nicht.", "Fehler",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                cmd.Parameters.Clear();
                cmd.CommandText = @"
                    INSERT OR IGNORE INTO RezeptUtensilien (RezeptId, UtensilId)
                    VALUES ($RezeptId, $UtensilId)";
                cmd.Parameters.AddWithValue("$RezeptId", _aktuellesRezeptId);
                cmd.Parameters.AddWithValue("$UtensilId", Convert.ToInt32(result));
                int rows = cmd.ExecuteNonQuery();

                //GBRezepteUtensilienLaden();
            }
        }
    }
}