using Microsoft.Data.Sqlite;
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
            // Tabellen anlegen (nur beim ersten Start n�tig, schadet aber nicht)
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

            // Rezepte-Tab verdrahten
            BTRezepteNeu.Click += BTRezepteNeu_Click;
            BTRezeptSpeichern.Click += BTRezeptSpeichern_Click;
            BTRezepteLoeschen.Click += BTRezepteLoeschen_Click;
            LBRezepte.SelectedIndexChanged += LBRezepte_SelectedIndexChanged;
            BTRezepteZutatenSuchen.Click += BTRezepteZutatenSuchen_Click;
            BTZutatenHinzufuegen.Click += BTZutatenHinzufuegen_Click;
            BTRezepteZutatenEntfernen.Click += BTRezepteZutatenEntfernen_Click;
            BTRezepteUtensilienHinzufuegen.Click += BTRezepteUtensilienHinzufuegen_Click;
            BTRezepteUtensilienEntfernen.Click += BTRezepteUtensilienEntfernen_Click;

            // Listen beim Start laden
            BeispielDatenLaden();
            UtensilienListeLaden();
            RezepteListeLaden();
        }
        private void SQLAdd()
        {
            using var conn = iFoodDb.OpenConnection();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "INSERT INTO Lebensmittel (Name) VALUES ($Name)";
            cmd.Parameters.AddWithValue("$Name", "Pizzer");
            cmd.ExecuteNonQuery();
        }
        private void SQLInComboBox()// Suche nach Name
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
        private void Ausgabe(string Lebensmittel)// Ausgabe der Nährwerte eines Lebensmittels
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
            result = cmd.ExecuteScalar();// Ergebnis der Abfrage (Kalorien) wird in result gespeichert
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
            if (TBSuche.Text != "")
            {
                foreach (TextBox TB in TBNaherwerte)
                {
                    TB.Text = "";
                }
            }
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
            // nur diese Spaltennamen sind erlaubt
            var erlaubt = new HashSet<string> {
        "Kalorien", "Kohlenhydrate", "DavonZucker",
        "Fett", "DavonGesFettsaeuren", "Eiweiss", "Salz"
    };
            if (!Toleranzen.TryGetValue(Attribute, out var toleranz))
                throw new ArgumentException("Ung�ltiges Attribut");
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
                MessageBox.Show("Bitte geben Sie einen Namen f�r das Utensil ein.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            using var conn = iFoodDb.OpenConnection();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "INSERT OR IGNORE INTO Utensilien (Name) VALUES ($Name)";
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
                MessageBox.Show("Bitte w�hlen Sie ein Utensil aus der Liste aus.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            using var conn = iFoodDb.OpenConnection();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "DELETE FROM Utensilien WHERE Name = $Name";
            cmd.Parameters.AddWithValue("$Name", name);
            int rows = cmd.ExecuteNonQuery();
            if (rows == 0)
            {
                MessageBox.Show("Das Utensil konnte nicht gel�scht werden.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                UtensilienListeLaden();
            }
        }
        private void BTDatenHinzufuegen_Click(object sender, EventArgs e)
        {
            bool voll = true;
            foreach (TextBox TB in TBNaherwerte)
            {
                if (TB.Text == "")
                {
                    voll = false;
                    MessageBox.Show("Es müssen alle N�hrwerte ausgefüllt sein.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                }
                else
                {
                }
            }
            if (voll == true)// Alle Nährwerte sind ausgefüllt, jetzt wird das Lebensmittel hinzugefügt
            {
                string name = TBSuche.Text.Trim();
                if (string.IsNullOrEmpty(name))
                {
                    MessageBox.Show("Bitte geben Sie einen Namen für das Lebensmittel ein.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using var conn = iFoodDb.OpenConnection();
                using var transaktion = conn.BeginTransaction();

                var cmd = conn.CreateCommand();
                cmd.Transaction = transaktion;
                cmd.CommandText = "INSERT INTO Lebensmittel (Name) VALUES ($Name); SELECT last_insert_rowid();";
                cmd.Parameters.AddWithValue("$Name", name);
                long lebensmittelId = Convert.ToInt64(cmd.ExecuteScalar());

                cmd.Parameters.Clear();
                cmd.CommandText = @"
                    INSERT INTO Naehrwerte
                        (LebensmittelID, Kalorien, Kohlenhydrate, DavonZucker, Fett, DavonGesFettsaeuren, Eiweiss, Salz)
                    VALUES
                        ($LebensmittelID, $Kalorien, $Kohlenhydrate, $DavonZucker, $Fett, $DavonGesFettsaeuren, $Eiweiss, $Salz)";
                cmd.Parameters.AddWithValue("$LebensmittelID", lebensmittelId);
                cmd.Parameters.AddWithValue("$Kalorien", Convert.ToDouble(TBKalorien.Text));
                cmd.Parameters.AddWithValue("$Kohlenhydrate", Convert.ToDouble(TBKohlenhydrate.Text));
                cmd.Parameters.AddWithValue("$DavonZucker", Convert.ToDouble(TBZucker.Text));
                cmd.Parameters.AddWithValue("$Fett", Convert.ToDouble(TBFett.Text));
                cmd.Parameters.AddWithValue("$DavonGesFettsaeuren", Convert.ToDouble(TBDavonGesaettigteFettsaeuren.Text));
                cmd.Parameters.AddWithValue("$Eiweiss", Convert.ToDouble(TBEiweiss.Text));
                cmd.Parameters.AddWithValue("$Salz", Convert.ToDouble(TBSalz.Text));
                cmd.ExecuteNonQuery();

                transaktion.Commit();

                MessageBox.Show($"'{name}' wurde hinzugefügt.", "Erfolg", MessageBoxButtons.OK, MessageBoxIcon.Information);

                foreach (TextBox TB in TBNaherwerte)
                {
                    TB.Text = "";
                }
                TBSuche.Text = "";
            }
        }
        private void BTDatenEntfernen_Click(object sender, EventArgs e)
        {
            string name = Convert.ToString(LBErgebnis.SelectedItem);
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Bitte wählen Sie ein Lebensmittel aus der Liste aus.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using var conn = iFoodDb.OpenConnection();

            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT Id FROM Lebensmittel WHERE Name = $Name LIMIT 1";
            cmd.Parameters.AddWithValue("$Name", name);
            var result = cmd.ExecuteScalar();
            if (result == null || result == DBNull.Value)
            {
                MessageBox.Show("Das Lebensmittel konnte nicht gefunden werden.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            long lebensmittelId = Convert.ToInt64(result);

            using var transaktion = conn.BeginTransaction();

            cmd.Transaction = transaktion;
            cmd.Parameters.Clear();
            cmd.CommandText = "DELETE FROM Naehrwerte WHERE LebensmittelID = $Id";
            cmd.Parameters.AddWithValue("$Id", lebensmittelId);
            cmd.ExecuteNonQuery();

            cmd.Parameters.Clear();
            cmd.CommandText = "DELETE FROM Lebensmittel WHERE Id = $Id";
            cmd.Parameters.AddWithValue("$Id", lebensmittelId);
            cmd.ExecuteNonQuery();

            transaktion.Commit();

            MessageBox.Show($"'{name}' wurde entfernt.", "Erfolg", MessageBoxButtons.OK, MessageBoxIcon.Information);

            LBErgebnis.Items.Remove(name);
            foreach (TextBox TB in TBNaherwerte)
            {
                TB.Text = "";
            }
        }

        // Aktuell ausgewähltes/bearbeitetes Rezept (null = neues, noch nicht gespeichertes Rezept)
        private long? aktuellesRezeptId = null;

        // Hilfsklassen, damit die ListBoxen den Namen anzeigen, aber die Id mitführen
        private class RezeptItem
        {
            public long Id;
            public string Name = "";
            public override string ToString() => Name;
        }
        private class LebensmittelItem
        {
            public long Id;
            public string Name = "";
            public override string ToString() => Name;
        }
        private class ZutatItem
        {
            public long LebensmittelId;
            public string Name = "";
            public double Menge;
            public override string ToString() => $"{Name} ({Menge:0.#} g)";
        }
        private class UtensilItem
        {
            public long Id;
            public string Name = "";
            public override string ToString() => Name;
        }

        private void RezepteListeLaden()
        {
            LBRezepte.Items.Clear();
            using var conn = iFoodDb.OpenConnection();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT Id, Name FROM Rezepte ORDER BY Name";
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                LBRezepte.Items.Add(new RezeptItem { Id = reader.GetInt64(0), Name = reader.GetString(1) });// macht, dass in der ListBox die Namen der Rezepte angezeigt werden, aber intern die Ids mitgeführt werden
            }
        }

        private void ZutatenLaden(long rezeptId)
        {
            LBZutaten.Items.Clear();
            SqliteConnection conn = iFoodDb.OpenConnection();
            SqliteCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"
        SELECT L.Id, L.Name, RL.Menge
        FROM RezeptLebensmitteln RL
        JOIN Lebensmittel L ON L.Id = RL.LebensmittelId
        WHERE RL.RezeptId = $id";
            cmd.Parameters.AddWithValue("$id", rezeptId);
            SqliteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ZutatItem zutat = new ZutatItem();
                zutat.LebensmittelId = reader.GetInt64(0);
                zutat.Name = reader.GetString(1);
                if (reader.IsDBNull(2))
                    zutat.Menge = 0;
                else
                    zutat.Menge = reader.GetDouble(2);
                LBZutaten.Items.Add(zutat);
            }
            reader.Close();
            conn.Close();
        }

        private void RezeptUtensilienLaden(long rezeptId)
        {
            LBRezepteUtensilien.Items.Clear();
            using var conn = iFoodDb.OpenConnection();
            var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                SELECT U.UtensilId, U.Name
                FROM RezeptUtensilien RU
                JOIN Utensilien U ON U.UtensilId = RU.UtensilId
                WHERE RU.RezeptId = $id";
            cmd.Parameters.AddWithValue("$id", rezeptId);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                LBRezepteUtensilien.Items.Add(new UtensilItem { Id = reader.GetInt64(0), Name = reader.GetString(1) });
            }
        }

        private void LBRezepte_SelectedIndexChanged(object sender, EventArgs e)
        {
            RezeptItem r = LBRezepte.SelectedItem as RezeptItem;

            if (r == null)
                return;

            aktuellesRezeptId = r.Id;

            SqliteConnection conn = iFoodDb.OpenConnection();

            SqliteCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT Name, Dauer, Anleitung, AnzahlPortionen FROM Rezepte WHERE Id = $id";
            cmd.Parameters.AddWithValue("$id", r.Id);

            SqliteDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                if (reader.IsDBNull(0))
                    TBRezepteDetailsName.Text = "";
                else
                    TBRezepteDetailsName.Text = reader.GetString(0);

                if (reader.IsDBNull(1))
                    TBRezepteDetailsDauer.Text = "";
                else
                    TBRezepteDetailsDauer.Text = reader.GetInt64(1).ToString();

                if (reader.IsDBNull(2))
                    TBRezepteAnleitung.Text = "";
                else
                    TBRezepteAnleitung.Text = reader.GetString(2);

                if (reader.IsDBNull(3))
                    TBRezepteDetailsPortionen.Text = "";
                else
                    TBRezepteDetailsPortionen.Text = reader.GetInt64(3).ToString();
            }

            reader.Close();
            conn.Close();

            ZutatenLaden(r.Id);
            RezeptUtensilienLaden(r.Id);
            RezeptNaehrwerteAktualisieren(r.Id);
        }

        private void BTRezepteNeu_Click(object sender, EventArgs e)
        {
            aktuellesRezeptId = null;
            LBRezepte.ClearSelected();

            TBRezepteDetailsName.Text = "";
            TBRezepteDetailsDauer.Text = "";
            TBRezepteDetailsPortionen.Text = "";
            TBRezepteAnleitung.Text = "";

            LBZutaten.Items.Clear();
            LBRezepteUtensilien.Items.Clear();
            LBZutatenHinzufuegen.Items.Clear();
            TBRezepteZutatenHinzufuegen.Text = "";
            TBZutatenAnzahl.Text = "";
            TBZutatenMenge.Text = "";

            LBRezepteNaehrwerteAnzeige.Text = "(Rezept auswählen oder neu speichern)";
        }

        private void BTRezeptSpeichern_Click(object sender, EventArgs e)
        {
            string name = TBRezepteDetailsName.Text.Trim();
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Bitte geben Sie einen Namen für das Rezept ein.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            object dauer;
            if (int.TryParse(TBRezepteDetailsDauer.Text, out int d))
                dauer = d;
            else
                dauer = DBNull.Value;

            object portionen;
            if (int.TryParse(TBRezepteDetailsPortionen.Text, out int p))
                portionen = p;
            else
                portionen = DBNull.Value;
            string anleitung = TBRezepteAnleitung.Text;

            using var conn = iFoodDb.OpenConnection();
            var cmd = conn.CreateCommand();

            if (aktuellesRezeptId == null)
            {
                cmd.CommandText = @"
                    INSERT INTO Rezepte (Name, Dauer, Anleitung, AnzahlPortionen)
                    VALUES ($Name, $Dauer, $Anleitung, $Portionen);
                    SELECT last_insert_rowid();";
                cmd.Parameters.AddWithValue("$Name", name);
                cmd.Parameters.AddWithValue("$Dauer", dauer);
                cmd.Parameters.AddWithValue("$Anleitung", anleitung);
                cmd.Parameters.AddWithValue("$Portionen", portionen);
                aktuellesRezeptId = Convert.ToInt64(cmd.ExecuteScalar());
            }
            else
            {
                cmd.CommandText = @"
                    UPDATE Rezepte
                    SET Name = $Name, Dauer = $Dauer, Anleitung = $Anleitung, AnzahlPortionen = $Portionen
                    WHERE Id = $Id";
                cmd.Parameters.AddWithValue("$Name", name);
                cmd.Parameters.AddWithValue("$Dauer", dauer);
                cmd.Parameters.AddWithValue("$Anleitung", anleitung);
                cmd.Parameters.AddWithValue("$Portionen", portionen);
                cmd.Parameters.AddWithValue("$Id", aktuellesRezeptId.Value);
                cmd.ExecuteNonQuery();
            }

            RezepteListeLaden();

            // Gerade gespeichertes Rezept wieder auswählen
            foreach (var item in LBRezepte.Items)
            {
                if (item is RezeptItem ri && ri.Id == aktuellesRezeptId)
                {
                    LBRezepte.SelectedItem = item;
                    break;
                }
            }

            MessageBox.Show($"Rezept '{name}' wurde gespeichert.", "Erfolg", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BTRezepteLoeschen_Click(object sender, EventArgs e)
        {
            if (LBRezepte.SelectedItem is not RezeptItem r)
            {
                MessageBox.Show("Bitte wählen Sie ein Rezept aus der Liste aus.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using var conn = iFoodDb.OpenConnection();
            using var transaktion = conn.BeginTransaction();
            var cmd = conn.CreateCommand();
            cmd.Transaction = transaktion;

            cmd.CommandText = "DELETE FROM RezeptLebensmitteln WHERE RezeptId = $Id";
            cmd.Parameters.AddWithValue("$Id", r.Id);
            cmd.ExecuteNonQuery();

            cmd.Parameters.Clear();
            cmd.CommandText = "DELETE FROM RezeptUtensilien WHERE RezeptId = $Id";
            cmd.Parameters.AddWithValue("$Id", r.Id);
            cmd.ExecuteNonQuery();

            cmd.Parameters.Clear();
            cmd.CommandText = "DELETE FROM Rezepte WHERE Id = $Id";
            cmd.Parameters.AddWithValue("$Id", r.Id);
            cmd.ExecuteNonQuery();

            transaktion.Commit();

            MessageBox.Show($"Rezept '{r.Name}' wurde gelöscht.", "Erfolg", MessageBoxButtons.OK, MessageBoxIcon.Information);

            RezepteListeLaden();
            BTRezepteNeu_Click(sender, e);
        }

        private void BTRezepteZutatenSuchen_Click(object sender, EventArgs e)
        {
            LBZutatenHinzufuegen.Items.Clear();
            string suche = TBRezepteZutatenHinzufuegen.Text.Trim();

            using var conn = iFoodDb.OpenConnection();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT Id, Name FROM Lebensmittel WHERE Name LIKE $s ORDER BY Name";
            cmd.Parameters.AddWithValue("$s", "%" + suche + "%");
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                LBZutatenHinzufuegen.Items.Add(new LebensmittelItem { Id = reader.GetInt64(0), Name = reader.GetString(1) });
            }

            if (LBZutatenHinzufuegen.Items.Count == 0)
                MessageBox.Show("Keine Lebensmittel gefunden.", "Hinweis", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BTZutatenHinzufuegen_Click(object sender, EventArgs e)
        {
            if (aktuellesRezeptId == null)
            {
                MessageBox.Show("Bitte speichern Sie zuerst das Rezept, bevor Sie Zutaten hinzufügen.", "Hinweis", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (LBZutatenHinzufuegen.SelectedItem is not LebensmittelItem l)
            {
                MessageBox.Show("Bitte wählen Sie eine Zutat aus den Suchergebnissen aus.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            double anzahl = 1;
            if (!string.IsNullOrWhiteSpace(TBZutatenAnzahl.Text) && !double.TryParse(TBZutatenAnzahl.Text, out anzahl))
            {
                MessageBox.Show("Bitte geben Sie eine gültige Anzahl ein.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!double.TryParse(TBZutatenMenge.Text, out double mengeProStueck))
            {
                MessageBox.Show("Bitte geben Sie eine gültige Menge in g ein.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            double menge = anzahl * mengeProStueck;

            using var conn = iFoodDb.OpenConnection();
            var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                INSERT INTO RezeptLebensmitteln (RezeptId, LebensmittelId, Menge)
                VALUES ($RezeptId, $LebensmittelId, $Menge)";// Es wird eine neue Zeile in der Tabelle RezeptLebensmitteln angelegt, die das aktuelle Rezept mit der ausgewählten Zutat und der angegebenen Menge verknüpft.
            cmd.Parameters.AddWithValue("$RezeptId", aktuellesRezeptId.Value);
            cmd.Parameters.AddWithValue("$LebensmittelId", l.Id);
            cmd.Parameters.AddWithValue("$Menge", menge);
            cmd.ExecuteNonQuery();

            ZutatenLaden(aktuellesRezeptId.Value);
            RezeptNaehrwerteAktualisieren(aktuellesRezeptId.Value);
            TBZutatenAnzahl.Text = "";
            TBZutatenMenge.Text = "";
        }

        private void BTRezepteZutatenEntfernen_Click(object sender, EventArgs e)
        {
            if (aktuellesRezeptId == null)
            {
                MessageBox.Show("Bitte wählen Sie zuerst ein Rezept aus.", "Hinweis", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (LBZutaten.SelectedItem is not ZutatItem z)
            {
                MessageBox.Show("Bitte wählen Sie eine Zutat aus dem Rezept aus.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using var conn = iFoodDb.OpenConnection();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "DELETE FROM RezeptLebensmitteln WHERE RezeptId = $RezeptId AND LebensmittelId = $LebensmittelId";
            cmd.Parameters.AddWithValue("$RezeptId", aktuellesRezeptId.Value);
            cmd.Parameters.AddWithValue("$LebensmittelId", z.LebensmittelId);
            cmd.ExecuteNonQuery();

            ZutatenLaden(aktuellesRezeptId.Value);
            RezeptNaehrwerteAktualisieren(aktuellesRezeptId.Value);
        }

        private void BTRezepteUtensilienHinzufuegen_Click(object sender, EventArgs e)
        {
            if (aktuellesRezeptId == null)
            {
                MessageBox.Show("Bitte speichern Sie zuerst das Rezept, bevor Sie Utensilien hinzufügen.", "Hinweis", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string name = Convert.ToString(CBUtensilienHinzufuegen.SelectedItem) ?? "";
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Bitte wählen Sie ein Utensil aus.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using var conn = iFoodDb.OpenConnection();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT UtensilId FROM Utensilien WHERE Name = $Name LIMIT 1";
            cmd.Parameters.AddWithValue("$Name", name);
            var result = cmd.ExecuteScalar();
            if (result == null || result == DBNull.Value)
            {
                MessageBox.Show("Das Utensil konnte nicht gefunden werden.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            long utensilId = Convert.ToInt64(result);

            cmd.Parameters.Clear();
            cmd.CommandText = "INSERT OR IGNORE INTO RezeptUtensilien (RezeptId, UtensilId) VALUES ($RezeptId, $UtensilId)";
            cmd.Parameters.AddWithValue("$RezeptId", aktuellesRezeptId.Value);
            cmd.Parameters.AddWithValue("$UtensilId", utensilId);
            cmd.ExecuteNonQuery();

            RezeptUtensilienLaden(aktuellesRezeptId.Value);
        }

        private void BTRezepteUtensilienEntfernen_Click(object sender, EventArgs e)
        {
            if (aktuellesRezeptId == null)
            {
                MessageBox.Show("Bitte wählen Sie zuerst ein Rezept aus.", "Hinweis", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (LBRezepteUtensilien.SelectedItem is not UtensilItem u)
            {
                MessageBox.Show("Bitte wählen Sie ein Utensil aus dem Rezept aus.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using var conn = iFoodDb.OpenConnection();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "DELETE FROM RezeptUtensilien WHERE RezeptId = $RezeptId AND UtensilId = $UtensilId";
            cmd.Parameters.AddWithValue("$RezeptId", aktuellesRezeptId.Value);
            cmd.Parameters.AddWithValue("$UtensilId", u.Id);
            cmd.ExecuteNonQuery();

            RezeptUtensilienLaden(aktuellesRezeptId.Value);
        }

        private Dictionary<string, double> RezeptNaehrwerteBerechnen(long rezeptId) // Berechnung der Nährwerte für ein Rezept, indem die Nährwerte aller Zutaten addiert und auf die Menge angepasst werden. Gibt ein Dictionary mit den Nährwerten zurück.
        {
            var summe = new Dictionary<string, double>
            {
                { "Kalorien", 0 },
                { "Kohlenhydrate", 0 },
                { "DavonZucker", 0 },
                { "Fett", 0 },
                { "DavonGesFettsaeuren", 0 },
                { "Eiweiss", 0 },
                { "Salz", 0 }
            };

            using var conn = iFoodDb.OpenConnection();
            var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                SELECT RL.Menge,
                       N.Kalorien, N.Kohlenhydrate, N.DavonZucker,
                       N.Fett, N.DavonGesFettsaeuren, N.Eiweiss, N.Salz
                FROM RezeptLebensmitteln RL
                JOIN Naehrwerte N ON N.LebensmittelID = RL.LebensmittelId
                WHERE RL.RezeptId = $id";
            cmd.Parameters.AddWithValue("$id", rezeptId);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                double menge = reader.IsDBNull(0) ? 0 : reader.GetDouble(0);
                double faktor = menge / 100.0; // pro 100 g

                if (!reader.IsDBNull(1)) summe["Kalorien"]            += reader.GetDouble(1) * faktor;
                if (!reader.IsDBNull(2)) summe["Kohlenhydrate"]       += reader.GetDouble(2) * faktor;
                if (!reader.IsDBNull(3)) summe["DavonZucker"]         += reader.GetDouble(3) * faktor;
                if (!reader.IsDBNull(4)) summe["Fett"]                += reader.GetDouble(4) * faktor;
                if (!reader.IsDBNull(5)) summe["DavonGesFettsaeuren"] += reader.GetDouble(5) * faktor;
                if (!reader.IsDBNull(6)) summe["Eiweiss"]             += reader.GetDouble(6) * faktor;
                if (!reader.IsDBNull(7)) summe["Salz"]                += reader.GetDouble(7) * faktor;
            }

            return summe;
        }

        private void RezeptNaehrwerteAktualisieren(long rezeptId)
        {
            var summe = RezeptNaehrwerteBerechnen(rezeptId);

            // portionen aus der datenbank holen
            int portionen = 0;
            using (var conn = iFoodDb.OpenConnection())
            {
                var cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT AnzahlPortionen FROM Rezepte WHERE Id = $id";
                cmd.Parameters.AddWithValue("$id", rezeptId);
                var result = cmd.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    portionen = Convert.ToInt32(result);
                }
            }

            // text für die anzeige zusammenbauen
            string text = "";
            text += $"Kalorien:               {summe["Kalorien"]:0.0} kcal\r\n";
            text += $"Kohlenhydrate:    {summe["Kohlenhydrate"]:0.0} g\r\n";
            text += $"  davon Zucker:    {summe["DavonZucker"]:0.0} g\r\n";
            text += $"Fett:                       {summe["Fett"]:0.0} g\r\n";
            text += $"  davon ges. FS:   {summe["DavonGesFettsaeuren"]:0.0} g\r\n";
            text += $"Eiweiß:                  {summe["Eiweiss"]:0.0} g\r\n";
            text += $"Salz:                      {summe["Salz"]:0.0} g";

            // falls portionen angegeben wurden, auch pro portion anzeigen
            if (portionen > 0)
            {
                text += "\r\n\r\n";
                text += $"Pro Portion ({portionen} Portionen):\r\n";
                text += $"Kalorien:               {summe["Kalorien"] / portionen:0.0} kcal\r\n";
                text += $"Eiweiß:                  {summe["Eiweiss"] / portionen:0.0} g\r\n";
                text += $"Fett:                       {summe["Fett"] / portionen:0.0} g\r\n";
                text += $"Kohlenhydrate:    {summe["Kohlenhydrate"] / portionen:0.0} g";
            }

            LBRezepteNaehrwerteAnzeige.Text = text;
        }

        // ===================== Beispiel-Daten ===================== ---> mit KI gemacht, um ein paar Rezepte zum Testen zu haben. Kann natürlich jederzeit erweitert oder gelöscht werden. Nur zur Demostration

        private void BeispielDatenLaden()
        {
            using var conn = iFoodDb.OpenConnection();

            // schauen ob schon beispieldaten vorhanden sind
            var check = conn.CreateCommand();
            check.CommandText = "SELECT COUNT(*) FROM Rezepte WHERE Name = 'Spaghetti Bolognese'";
            long anzahl = Convert.ToInt64(check.ExecuteScalar());

            // wenn schon vorhanden nichts machen
            if (anzahl > 0)
            {
                return;
            }

            // lebensmittel hinzufügen und id zurückgeben
            long AddLebensmittel(string name, string hersteller,
                double kalorien, double kohlenhydrate, double zucker,
                double fett, double gesFett, double eiweiss, double salz)
            {
                var cmd = conn.CreateCommand();
                cmd.CommandText = @"INSERT INTO Lebensmittel (Name, Hersteller) VALUES ($Name, $Hersteller);
                            SELECT last_insert_rowid();";
                cmd.Parameters.AddWithValue("$Name", name);
                cmd.Parameters.AddWithValue("$Hersteller", hersteller);
                long id = Convert.ToInt64(cmd.ExecuteScalar());

                // nährwerte separat einfügen
                var n = conn.CreateCommand();
                n.CommandText = @"INSERT INTO Naehrwerte
                              (LebensmittelID, Kalorien, Kohlenhydrate, DavonZucker, Fett, DavonGesFettsaeuren, Eiweiss, Salz)
                          VALUES
                              ($Id, $Kal, $KH, $Zucker, $Fett, $GesFS, $Eiweiss, $Salz)";
                n.Parameters.AddWithValue("$Id", id);
                n.Parameters.AddWithValue("$Kal", kalorien);
                n.Parameters.AddWithValue("$KH", kohlenhydrate);
                n.Parameters.AddWithValue("$Zucker", zucker);
                n.Parameters.AddWithValue("$Fett", fett);
                n.Parameters.AddWithValue("$GesFS", gesFett);
                n.Parameters.AddWithValue("$Eiweiss", eiweiss);
                n.Parameters.AddWithValue("$Salz", salz);
                n.ExecuteNonQuery();

                return id;
            }

            // neues rezept anlegen
            long AddRezept(string name, int dauer, int portionen, string anleitung)
            {
                var cmd = conn.CreateCommand();
                cmd.CommandText = @"INSERT INTO Rezepte (Name, Dauer, Anleitung, AnzahlPortionen)
                            VALUES ($Name, $Dauer, $Anleitung, $Portionen);
                            SELECT last_insert_rowid();";
                cmd.Parameters.AddWithValue("$Name", name);
                cmd.Parameters.AddWithValue("$Dauer", dauer);
                cmd.Parameters.AddWithValue("$Anleitung", anleitung);
                cmd.Parameters.AddWithValue("$Portionen", portionen);
                return Convert.ToInt64(cmd.ExecuteScalar());
            }

            // zutat zu einem rezept hinzufügen
            void AddZutat(long rezeptId, long lebensmittelId, double mengeGramm)
            {
                var cmd = conn.CreateCommand();
                cmd.CommandText = @"INSERT INTO RezeptLebensmitteln (RezeptId, LebensmittelId, Menge)
                            VALUES ($R, $L, $M)";
                cmd.Parameters.AddWithValue("$R", rezeptId);
                cmd.Parameters.AddWithValue("$L", lebensmittelId);
                cmd.Parameters.AddWithValue("$M", mengeGramm);
                cmd.ExecuteNonQuery();
            }

            // utensil hinzufügen, falls noch nicht vorhanden
            long AddUtensil(string name)
            {
                var check2 = conn.CreateCommand();
                check2.CommandText = "SELECT UtensilId FROM Utensilien WHERE Name = $Name";
                check2.Parameters.AddWithValue("$Name", name);
                var r = check2.ExecuteScalar();

                // falls schon vorhanden einfach die id zurückgeben
                if (r != null && r != DBNull.Value)
                {
                    return Convert.ToInt64(r);
                }

                var cmd = conn.CreateCommand();
                cmd.CommandText = @"INSERT INTO Utensilien (Name) VALUES ($Name);
                            SELECT last_insert_rowid();";
                cmd.Parameters.AddWithValue("$Name", name);
                return Convert.ToInt64(cmd.ExecuteScalar());
            }

            void AddRezeptUtensil(long rezeptId, long utensilId)
            {
                var cmd = conn.CreateCommand();
                cmd.CommandText = @"INSERT OR IGNORE INTO RezeptUtensilien (RezeptId, UtensilId)
                            VALUES ($R, $U)";
                cmd.Parameters.AddWithValue("$R", rezeptId);
                cmd.Parameters.AddWithValue("$U", utensilId);
                cmd.ExecuteNonQuery();
            }

            // lebensmittel anlegen (nährwerte pro 100g)
            long spaghetti = AddLebensmittel("Spaghetti (roh)", "Barilla", 358, 71.2, 3.5, 1.5, 0.3, 12.5, 0.01);
            long tomate = AddLebensmittel("Tomaten", "frisch", 18, 3.9, 2.6, 0.2, 0.0, 0.9, 0.01);
            long hack = AddLebensmittel("Rinderhackfleisch", "Metzgerei", 250, 0.0, 0.0, 20.0, 8.0, 17.0, 0.10);
            long zwiebel = AddLebensmittel("Zwiebel", "frisch", 40, 9.3, 4.2, 0.1, 0.0, 1.1, 0.01);
            long knoblauch = AddLebensmittel("Knoblauch", "frisch", 149, 33.1, 1.0, 0.5, 0.1, 6.4, 0.02);
            long olivenoel = AddLebensmittel("Olivenöl", "Bertolli", 884, 0.0, 0.0, 100.0, 14.0, 0.0, 0.00);
            long mehl = AddLebensmittel("Weizenmehl Typ 405", "Aurora", 348, 72.3, 0.7, 1.0, 0.2, 10.0, 0.01);
            long ei = AddLebensmittel("Hühnerei", "frisch", 155, 1.1, 1.1, 11.0, 3.3, 13.0, 0.36);
            long milch = AddLebensmittel("Milch 3,5%", "Weihenstephan", 65, 4.8, 4.8, 3.5, 2.3, 3.3, 0.13);
            long zucker = AddLebensmittel("Zucker", "Südzucker", 405, 101.0, 101.0, 0.0, 0.0, 0.0, 0.00);
            long butter = AddLebensmittel("Butter", "Kerrygold", 741, 0.6, 0.6, 82.0, 52.0, 0.7, 1.20);
            long kartoffel = AddLebensmittel("Kartoffel", "frisch", 77, 17.0, 0.8, 0.1, 0.0, 2.0, 0.01);
            long kaese = AddLebensmittel("Gouda", "Frico", 356, 0.0, 0.0, 27.4, 18.0, 25.0, 1.70);

            // utensilien
            long topf = AddUtensil("Topf");
            long pfanne = AddUtensil("Pfanne");
            long messer = AddUtensil("Messer");
            long brett = AddUtensil("Schneidebrett");
            long schuessel = AddUtensil("Schüssel");
            long ruehrer = AddUtensil("Rührgerät");
            long backform = AddUtensil("Backform");

            // rezept 1
            long r1 = AddRezept("Spaghetti Bolognese", 40, 4,
                "1. Zwiebel und Knoblauch fein hacken. " +
                "2. In Olivenöl andünsten. " +
                "3. Hackfleisch dazugeben und scharf anbraten. " +
                "4. Tomaten klein schneiden, dazugeben und 20 Min köcheln lassen. " +
                "5. Spaghetti in Salzwasser kochen. " +
                "6. Soße über die Spaghetti geben.");
            AddZutat(r1, spaghetti, 500);
            AddZutat(r1, hack, 400);
            AddZutat(r1, tomate, 400);
            AddZutat(r1, zwiebel, 100);
            AddZutat(r1, knoblauch, 10);
            AddZutat(r1, olivenoel, 20);
            AddRezeptUtensil(r1, topf);
            AddRezeptUtensil(r1, pfanne);
            AddRezeptUtensil(r1, messer);
            AddRezeptUtensil(r1, brett);

            // rezept 2
            long r2 = AddRezept("Pfannkuchen", 25, 4,
                "1. Mehl, Milch, Eier und eine Prise Zucker in einer Schüssel verquirlen. " +
                "2. Teig 10 Min ruhen lassen. " +
                "3. Etwas Butter in der Pfanne zerlassen. " +
                "4. Eine Kelle Teig hineingeben und von beiden Seiten goldbraun backen. " +
                "5. Mit Zucker bestreuen und servieren.");
            AddZutat(r2, mehl, 250);
            AddZutat(r2, milch, 500);
            AddZutat(r2, ei, 150); // ca. 3 Eier
            AddZutat(r2, zucker, 30);
            AddZutat(r2, butter, 20);
            AddRezeptUtensil(r2, pfanne);
            AddRezeptUtensil(r2, schuessel);
            AddRezeptUtensil(r2, ruehrer);

            // rezept 3
            long r3 = AddRezept("Kartoffelgratin", 70, 4,
                "1. Kartoffeln schälen und in dünne Scheiben schneiden. " +
                "2. Knoblauch fein hacken, mit Milch und Salz aufkochen. " +
                "3. Kartoffeln in eine gefettete Backform schichten. " +
                "4. Milchmischung darüber gießen. " +
                "5. Mit geriebenem Gouda bestreuen. " +
                "6. Bei 180°C ca. 45 Min backen.");
            AddZutat(r3, kartoffel, 1000);
            AddZutat(r3, milch, 400);
            AddZutat(r3, kaese, 150);
            AddZutat(r3, knoblauch, 8);
            AddZutat(r3, butter, 20);
            AddRezeptUtensil(r3, backform);
            AddRezeptUtensil(r3, messer);
            AddRezeptUtensil(r3, brett);
            AddRezeptUtensil(r3, topf);
        }
    }
}