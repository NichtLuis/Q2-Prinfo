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
            // Whitelist � nur diese Spaltennamen sind erlaubt
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
            if (voll == true)
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

        // ===================== Rezepte =====================

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
                LBRezepte.Items.Add(new RezeptItem { Id = reader.GetInt64(0), Name = reader.GetString(1) });
            }
        }

        private void ZutatenLaden(long rezeptId)
        {
            LBZutaten.Items.Clear();
            using var conn = iFoodDb.OpenConnection();
            var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                SELECT L.Id, L.Name, RL.Menge
                FROM RezeptLebensmitteln RL
                JOIN Lebensmittel L ON L.Id = RL.LebensmittelId
                WHERE RL.RezeptId = $id";
            cmd.Parameters.AddWithValue("$id", rezeptId);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                LBZutaten.Items.Add(new ZutatItem
                {
                    LebensmittelId = reader.GetInt64(0),
                    Name = reader.GetString(1),
                    Menge = reader.IsDBNull(2) ? 0 : reader.GetDouble(2)
                });
            }
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
            if (LBRezepte.SelectedItem is not RezeptItem r)
                return;

            aktuellesRezeptId = r.Id;

            using var conn = iFoodDb.OpenConnection();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT Name, Dauer, Anleitung, AnzahlPortionen FROM Rezepte WHERE Id = $id";
            cmd.Parameters.AddWithValue("$id", r.Id);
            using (var reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    TBRezepteDetailsName.Text = reader.IsDBNull(0) ? "" : reader.GetString(0);
                    TBRezepteDetailsDauer.Text = reader.IsDBNull(1) ? "" : reader.GetInt64(1).ToString();
                    TBRezepteAnleitung.Text = reader.IsDBNull(2) ? "" : reader.GetString(2);
                    TBRezepteDetailsPortionen.Text = reader.IsDBNull(3) ? "" : reader.GetInt64(3).ToString();
                }
            }

            ZutatenLaden(r.Id);
            RezeptUtensilienLaden(r.Id);
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
        }

        private void BTRezeptSpeichern_Click(object sender, EventArgs e)
        {
            string name = TBRezepteDetailsName.Text.Trim();
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Bitte geben Sie einen Namen für das Rezept ein.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            object dauer = int.TryParse(TBRezepteDetailsDauer.Text, out var d) ? d : DBNull.Value;
            object portionen = int.TryParse(TBRezepteDetailsPortionen.Text, out var p) ? p : DBNull.Value;
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
                VALUES ($RezeptId, $LebensmittelId, $Menge)";
            cmd.Parameters.AddWithValue("$RezeptId", aktuellesRezeptId.Value);
            cmd.Parameters.AddWithValue("$LebensmittelId", l.Id);
            cmd.Parameters.AddWithValue("$Menge", menge);
            cmd.ExecuteNonQuery();

            ZutatenLaden(aktuellesRezeptId.Value);
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
    }
}