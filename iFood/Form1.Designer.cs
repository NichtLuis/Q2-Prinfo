namespace iFood
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            TCAnzeige = new TabControl();
            TPLebensmittel = new TabPage();
            GBNaehrwerte = new GroupBox();
            TBSalz = new TextBox();
            LBSalz = new Label();
            TBEiweiss = new TextBox();
            TBZucker = new TextBox();
            TBKohlenhydrate = new TextBox();
            TBDavonGesaettigteFettsaeuren = new TextBox();
            TBFett = new TextBox();
            TBKalorien = new TextBox();
            LBEiweiss = new Label();
            LBDavonGesaettigteFettsaeuren = new Label();
            LBZucker = new Label();
            LBFett = new Label();
            LBKohlenhydrate = new Label();
            LBKalorien = new Label();
            LBErgebnis = new ListBox();
            BTSuche = new Button();
            TBSuche = new TextBox();
            TPRezepte = new TabPage();
            GBRezepteZutaten = new GroupBox();
            LBZutatenHinzufuegen = new ListBox();
            BTRezepteZutatenSuchen = new Button();
            TBRezepteZutatenHinzufuegen = new TextBox();
            BTRezepteZutatenEntfernen = new Button();
            LBRezepteZutatenHinzufuegen = new Label();
            LBZutaten = new ListBox();
            LBRezepteZutatenImRezept = new Label();
            GBRezepteDetails = new GroupBox();
            LBRezepteDetailsAnleitung = new Label();
            LBRezepteDetailsPortionen = new Label();
            LBRezepteDetailsDauer = new Label();
            TBRezepteAnleitung = new TextBox();
            TBRezepteDetailsPortionen = new TextBox();
            TBRezepteDetailsDauer = new TextBox();
            TBRezepteDetailsName = new TextBox();
            LBRezepteDetailsName = new Label();
            BTRezepteLoeschen = new Button();
            BTRezeptSpeichern = new Button();
            BTRezepteNeu = new Button();
            LBRezepte = new ListBox();
            TPUtensilien = new TabPage();
            LBZutatenAnzahl = new Label();
            TBZutatenAnzahl = new TextBox();
            LBZutatenMenge = new Label();
            TBZutatenMenge = new TextBox();
            GBRezepteUtensilien = new GroupBox();
            LBUtensilienImRezept = new Label();
            LBRezepteUtensilien = new ListBox();
            BTRezepteUtensilienEntfernen = new Button();
            BTZutatenHinzufuegen = new Button();
            LBUtensilienHinzufuegen = new Label();
            CBUtensilienHinzufuegen = new ComboBox();
            BTRezepteUtensilienHinzufuegen = new Button();
            LBUtensilien = new ListBox();
            LBUtensilienVerwalten = new Label();
            TBUtensielName = new TextBox();
            BTUtensielienHinzufuegen = new Button();
            BTUtensilienLoeschen = new Button();
            BTDatenLoeschen = new Button();
            TCAnzeige.SuspendLayout();
            TPLebensmittel.SuspendLayout();
            GBNaehrwerte.SuspendLayout();
            TPRezepte.SuspendLayout();
            GBRezepteZutaten.SuspendLayout();
            GBRezepteDetails.SuspendLayout();
            TPUtensilien.SuspendLayout();
            GBRezepteUtensilien.SuspendLayout();
            SuspendLayout();
            // 
            // TCAnzeige
            // 
            TCAnzeige.Controls.Add(TPLebensmittel);
            TCAnzeige.Controls.Add(TPRezepte);
            TCAnzeige.Controls.Add(TPUtensilien);
            TCAnzeige.Dock = DockStyle.Top;
            TCAnzeige.Location = new Point(0, 0);
            TCAnzeige.Name = "TCAnzeige";
            TCAnzeige.SelectedIndex = 0;
            TCAnzeige.Size = new Size(2118, 1023);
            TCAnzeige.TabIndex = 0;
            // 
            // TPLebensmittel
            // 
            TPLebensmittel.Controls.Add(BTDatenLoeschen);
            TPLebensmittel.Controls.Add(GBNaehrwerte);
            TPLebensmittel.Controls.Add(LBErgebnis);
            TPLebensmittel.Controls.Add(BTSuche);
            TPLebensmittel.Controls.Add(TBSuche);
            TPLebensmittel.Location = new Point(8, 46);
            TPLebensmittel.Name = "TPLebensmittel";
            TPLebensmittel.Padding = new Padding(3);
            TPLebensmittel.Size = new Size(2102, 969);
            TPLebensmittel.TabIndex = 0;
            TPLebensmittel.Text = "Lebensmittel";
            TPLebensmittel.UseVisualStyleBackColor = true;
            // 
            // GBNaehrwerte
            // 
            GBNaehrwerte.Controls.Add(TBSalz);
            GBNaehrwerte.Controls.Add(LBSalz);
            GBNaehrwerte.Controls.Add(TBEiweiss);
            GBNaehrwerte.Controls.Add(TBZucker);
            GBNaehrwerte.Controls.Add(TBKohlenhydrate);
            GBNaehrwerte.Controls.Add(TBDavonGesaettigteFettsaeuren);
            GBNaehrwerte.Controls.Add(TBFett);
            GBNaehrwerte.Controls.Add(TBKalorien);
            GBNaehrwerte.Controls.Add(LBEiweiss);
            GBNaehrwerte.Controls.Add(LBDavonGesaettigteFettsaeuren);
            GBNaehrwerte.Controls.Add(LBZucker);
            GBNaehrwerte.Controls.Add(LBFett);
            GBNaehrwerte.Controls.Add(LBKohlenhydrate);
            GBNaehrwerte.Controls.Add(LBKalorien);
            GBNaehrwerte.Location = new Point(450, 8);
            GBNaehrwerte.Name = "GBNaehrwerte";
            GBNaehrwerte.Size = new Size(542, 430);
            GBNaehrwerte.TabIndex = 7;
            GBNaehrwerte.TabStop = false;
            GBNaehrwerte.Text = "Nährwerte";
            // 
            // TBSalz
            // 
            TBSalz.Location = new Point(118, 313);
            TBSalz.Name = "TBSalz";
            TBSalz.PlaceholderText = "g";
            TBSalz.Size = new Size(200, 39);
            TBSalz.TabIndex = 21;
            // 
            // LBSalz
            // 
            LBSalz.AutoSize = true;
            LBSalz.Location = new Point(6, 313);
            LBSalz.Name = "LBSalz";
            LBSalz.Size = new Size(61, 32);
            LBSalz.TabIndex = 20;
            LBSalz.Text = "Salz:";
            // 
            // TBEiweiss
            // 
            TBEiweiss.Location = new Point(118, 268);
            TBEiweiss.Name = "TBEiweiss";
            TBEiweiss.PlaceholderText = "g";
            TBEiweiss.Size = new Size(200, 39);
            TBEiweiss.TabIndex = 19;
            // 
            // TBZucker
            // 
            TBZucker.Location = new Point(118, 223);
            TBZucker.Name = "TBZucker";
            TBZucker.PlaceholderText = "g";
            TBZucker.Size = new Size(200, 39);
            TBZucker.TabIndex = 18;
            // 
            // TBKohlenhydrate
            // 
            TBKohlenhydrate.Location = new Point(186, 178);
            TBKohlenhydrate.Name = "TBKohlenhydrate";
            TBKohlenhydrate.PlaceholderText = "g";
            TBKohlenhydrate.Size = new Size(200, 39);
            TBKohlenhydrate.TabIndex = 17;
            // 
            // TBDavonGesaettigteFettsaeuren
            // 
            TBDavonGesaettigteFettsaeuren.Location = new Point(265, 133);
            TBDavonGesaettigteFettsaeuren.Name = "TBDavonGesaettigteFettsaeuren";
            TBDavonGesaettigteFettsaeuren.PlaceholderText = "g";
            TBDavonGesaettigteFettsaeuren.Size = new Size(200, 39);
            TBDavonGesaettigteFettsaeuren.TabIndex = 16;
            // 
            // TBFett
            // 
            TBFett.Location = new Point(118, 88);
            TBFett.Name = "TBFett";
            TBFett.PlaceholderText = "g";
            TBFett.Size = new Size(200, 39);
            TBFett.TabIndex = 15;
            // 
            // TBKalorien
            // 
            TBKalorien.Location = new Point(118, 43);
            TBKalorien.Name = "TBKalorien";
            TBKalorien.PlaceholderText = "kcal";
            TBKalorien.Size = new Size(200, 39);
            TBKalorien.TabIndex = 14;
            // 
            // LBEiweiss
            // 
            LBEiweiss.AutoSize = true;
            LBEiweiss.Location = new Point(6, 268);
            LBEiweiss.Name = "LBEiweiss";
            LBEiweiss.Size = new Size(86, 32);
            LBEiweiss.TabIndex = 13;
            LBEiweiss.Text = "Eiweiß:";
            // 
            // LBDavonGesaettigteFettsaeuren
            // 
            LBDavonGesaettigteFettsaeuren.AutoSize = true;
            LBDavonGesaettigteFettsaeuren.Location = new Point(6, 130);
            LBDavonGesaettigteFettsaeuren.Name = "LBDavonGesaettigteFettsaeuren";
            LBDavonGesaettigteFettsaeuren.Size = new Size(253, 32);
            LBDavonGesaettigteFettsaeuren.TabIndex = 11;
            LBDavonGesaettigteFettsaeuren.Text = "davon ges. Fettsäuren:";
            // 
            // LBZucker
            // 
            LBZucker.AutoSize = true;
            LBZucker.Location = new Point(6, 223);
            LBZucker.Name = "LBZucker";
            LBZucker.Size = new Size(91, 32);
            LBZucker.TabIndex = 9;
            LBZucker.Text = "Zucker:";
            // 
            // LBFett
            // 
            LBFett.AutoSize = true;
            LBFett.Location = new Point(6, 88);
            LBFett.Name = "LBFett";
            LBFett.Size = new Size(60, 32);
            LBFett.TabIndex = 7;
            LBFett.Text = "Fett:";
            // 
            // LBKohlenhydrate
            // 
            LBKohlenhydrate.AutoSize = true;
            LBKohlenhydrate.Location = new Point(6, 178);
            LBKohlenhydrate.Name = "LBKohlenhydrate";
            LBKohlenhydrate.Size = new Size(174, 32);
            LBKohlenhydrate.TabIndex = 5;
            LBKohlenhydrate.Text = "Kohlenhydrate:";
            // 
            // LBKalorien
            // 
            LBKalorien.AutoSize = true;
            LBKalorien.Location = new Point(6, 43);
            LBKalorien.Name = "LBKalorien";
            LBKalorien.Size = new Size(106, 32);
            LBKalorien.TabIndex = 0;
            LBKalorien.Text = "Kalorien:";
            // 
            // LBErgebnis
            // 
            LBErgebnis.FormattingEnabled = true;
            LBErgebnis.HorizontalScrollbar = true;
            LBErgebnis.Location = new Point(6, 51);
            LBErgebnis.Name = "LBErgebnis";
            LBErgebnis.Size = new Size(438, 452);
            LBErgebnis.TabIndex = 6;
            // 
            // BTSuche
            // 
            BTSuche.Location = new Point(274, 6);
            BTSuche.Name = "BTSuche";
            BTSuche.Size = new Size(170, 39);
            BTSuche.TabIndex = 5;
            BTSuche.Text = "Suchen";
            BTSuche.UseVisualStyleBackColor = true;
            // 
            // TBSuche
            // 
            TBSuche.Location = new Point(6, 6);
            TBSuche.Name = "TBSuche";
            TBSuche.PlaceholderText = "Suche nach z.B. Apfel";
            TBSuche.Size = new Size(260, 39);
            TBSuche.TabIndex = 4;
            // 
            // TPRezepte
            // 
            TPRezepte.Controls.Add(GBRezepteUtensilien);
            TPRezepte.Controls.Add(GBRezepteZutaten);
            TPRezepte.Controls.Add(GBRezepteDetails);
            TPRezepte.Controls.Add(BTRezepteLoeschen);
            TPRezepte.Controls.Add(BTRezeptSpeichern);
            TPRezepte.Controls.Add(BTRezepteNeu);
            TPRezepte.Controls.Add(LBRezepte);
            TPRezepte.Location = new Point(8, 46);
            TPRezepte.Name = "TPRezepte";
            TPRezepte.Padding = new Padding(3);
            TPRezepte.Size = new Size(2102, 969);
            TPRezepte.TabIndex = 1;
            TPRezepte.Text = "Rezepte";
            TPRezepte.UseVisualStyleBackColor = true;
            // 
            // GBRezepteZutaten
            // 
            GBRezepteZutaten.Controls.Add(BTZutatenHinzufuegen);
            GBRezepteZutaten.Controls.Add(TBZutatenMenge);
            GBRezepteZutaten.Controls.Add(LBZutatenMenge);
            GBRezepteZutaten.Controls.Add(TBZutatenAnzahl);
            GBRezepteZutaten.Controls.Add(LBZutatenAnzahl);
            GBRezepteZutaten.Controls.Add(LBZutatenHinzufuegen);
            GBRezepteZutaten.Controls.Add(BTRezepteZutatenSuchen);
            GBRezepteZutaten.Controls.Add(TBRezepteZutatenHinzufuegen);
            GBRezepteZutaten.Controls.Add(BTRezepteZutatenEntfernen);
            GBRezepteZutaten.Controls.Add(LBRezepteZutatenHinzufuegen);
            GBRezepteZutaten.Controls.Add(LBZutaten);
            GBRezepteZutaten.Controls.Add(LBRezepteZutatenImRezept);
            GBRezepteZutaten.Location = new Point(462, 341);
            GBRezepteZutaten.Name = "GBRezepteZutaten";
            GBRezepteZutaten.Size = new Size(821, 406);
            GBRezepteZutaten.TabIndex = 5;
            GBRezepteZutaten.TabStop = false;
            GBRezepteZutaten.Text = "Zutaten:";
            // 
            // LBZutatenHinzufuegen
            // 
            LBZutatenHinzufuegen.FormattingEnabled = true;
            LBZutatenHinzufuegen.HorizontalScrollbar = true;
            LBZutatenHinzufuegen.Location = new Point(275, 118);
            LBZutatenHinzufuegen.Name = "LBZutatenHinzufuegen";
            LBZutatenHinzufuegen.Size = new Size(414, 164);
            LBZutatenHinzufuegen.TabIndex = 6;
            // 
            // BTRezepteZutatenSuchen
            // 
            BTRezepteZutatenSuchen.Location = new Point(576, 66);
            BTRezepteZutatenSuchen.Name = "BTRezepteZutatenSuchen";
            BTRezepteZutatenSuchen.Size = new Size(113, 46);
            BTRezepteZutatenSuchen.TabIndex = 5;
            BTRezepteZutatenSuchen.Text = "Suchen";
            BTRezepteZutatenSuchen.UseVisualStyleBackColor = true;
            // 
            // TBRezepteZutatenHinzufuegen
            // 
            TBRezepteZutatenHinzufuegen.Location = new Point(275, 70);
            TBRezepteZutatenHinzufuegen.Name = "TBRezepteZutatenHinzufuegen";
            TBRezepteZutatenHinzufuegen.PlaceholderText = "z.B. Apfel";
            TBRezepteZutatenHinzufuegen.Size = new Size(295, 39);
            TBRezepteZutatenHinzufuegen.TabIndex = 4;
            // 
            // BTRezepteZutatenEntfernen
            // 
            BTRezepteZutatenEntfernen.Location = new Point(6, 240);
            BTRezepteZutatenEntfernen.Name = "BTRezepteZutatenEntfernen";
            BTRezepteZutatenEntfernen.Size = new Size(152, 46);
            BTRezepteZutatenEntfernen.TabIndex = 3;
            BTRezepteZutatenEntfernen.Text = "Entfernen";
            BTRezepteZutatenEntfernen.UseVisualStyleBackColor = true;
            // 
            // LBRezepteZutatenHinzufuegen
            // 
            LBRezepteZutatenHinzufuegen.AutoSize = true;
            LBRezepteZutatenHinzufuegen.Location = new Point(275, 35);
            LBRezepteZutatenHinzufuegen.Name = "LBRezepteZutatenHinzufuegen";
            LBRezepteZutatenHinzufuegen.Size = new Size(144, 32);
            LBRezepteZutatenHinzufuegen.TabIndex = 2;
            LBRezepteZutatenHinzufuegen.Text = "Hinzufügen:";
            // 
            // LBZutaten
            // 
            LBZutaten.FormattingEnabled = true;
            LBZutaten.HorizontalScrollbar = true;
            LBZutaten.Location = new Point(6, 70);
            LBZutaten.Name = "LBZutaten";
            LBZutaten.Size = new Size(240, 164);
            LBZutaten.TabIndex = 1;
            // 
            // LBRezepteZutatenImRezept
            // 
            LBRezepteZutatenImRezept.AutoSize = true;
            LBRezepteZutatenImRezept.Location = new Point(6, 35);
            LBRezepteZutatenImRezept.Name = "LBRezepteZutatenImRezept";
            LBRezepteZutatenImRezept.Size = new Size(125, 32);
            LBRezepteZutatenImRezept.TabIndex = 0;
            LBRezepteZutatenImRezept.Text = "Im Rezept:";
            // 
            // GBRezepteDetails
            // 
            GBRezepteDetails.Controls.Add(LBRezepteDetailsAnleitung);
            GBRezepteDetails.Controls.Add(LBRezepteDetailsPortionen);
            GBRezepteDetails.Controls.Add(LBRezepteDetailsDauer);
            GBRezepteDetails.Controls.Add(TBRezepteAnleitung);
            GBRezepteDetails.Controls.Add(TBRezepteDetailsPortionen);
            GBRezepteDetails.Controls.Add(TBRezepteDetailsDauer);
            GBRezepteDetails.Controls.Add(TBRezepteDetailsName);
            GBRezepteDetails.Controls.Add(LBRezepteDetailsName);
            GBRezepteDetails.Location = new Point(462, 6);
            GBRezepteDetails.Name = "GBRezepteDetails";
            GBRezepteDetails.Size = new Size(821, 329);
            GBRezepteDetails.TabIndex = 4;
            GBRezepteDetails.TabStop = false;
            GBRezepteDetails.Text = "Details:";
            // 
            // LBRezepteDetailsAnleitung
            // 
            LBRezepteDetailsAnleitung.AutoSize = true;
            LBRezepteDetailsAnleitung.Location = new Point(6, 194);
            LBRezepteDetailsAnleitung.Name = "LBRezepteDetailsAnleitung";
            LBRezepteDetailsAnleitung.Size = new Size(123, 32);
            LBRezepteDetailsAnleitung.TabIndex = 7;
            LBRezepteDetailsAnleitung.Text = "Anleitung:";
            // 
            // LBRezepteDetailsPortionen
            // 
            LBRezepteDetailsPortionen.AutoSize = true;
            LBRezepteDetailsPortionen.Location = new Point(6, 149);
            LBRezepteDetailsPortionen.Name = "LBRezepteDetailsPortionen";
            LBRezepteDetailsPortionen.Size = new Size(122, 32);
            LBRezepteDetailsPortionen.TabIndex = 6;
            LBRezepteDetailsPortionen.Text = "Portionen:";
            // 
            // LBRezepteDetailsDauer
            // 
            LBRezepteDetailsDauer.AutoSize = true;
            LBRezepteDetailsDauer.Location = new Point(6, 104);
            LBRezepteDetailsDauer.Name = "LBRezepteDetailsDauer";
            LBRezepteDetailsDauer.Size = new Size(83, 32);
            LBRezepteDetailsDauer.TabIndex = 5;
            LBRezepteDetailsDauer.Text = "Dauer:";
            // 
            // TBRezepteAnleitung
            // 
            TBRezepteAnleitung.Location = new Point(135, 191);
            TBRezepteAnleitung.Name = "TBRezepteAnleitung";
            TBRezepteAnleitung.ScrollBars = ScrollBars.Vertical;
            TBRezepteAnleitung.Size = new Size(292, 39);
            TBRezepteAnleitung.TabIndex = 4;
            // 
            // TBRezepteDetailsPortionen
            // 
            TBRezepteDetailsPortionen.Location = new Point(134, 146);
            TBRezepteDetailsPortionen.Name = "TBRezepteDetailsPortionen";
            TBRezepteDetailsPortionen.Size = new Size(293, 39);
            TBRezepteDetailsPortionen.TabIndex = 3;
            // 
            // TBRezepteDetailsDauer
            // 
            TBRezepteDetailsDauer.Location = new Point(95, 101);
            TBRezepteDetailsDauer.Name = "TBRezepteDetailsDauer";
            TBRezepteDetailsDauer.Size = new Size(332, 39);
            TBRezepteDetailsDauer.TabIndex = 2;
            // 
            // TBRezepteDetailsName
            // 
            TBRezepteDetailsName.Location = new Point(95, 56);
            TBRezepteDetailsName.Name = "TBRezepteDetailsName";
            TBRezepteDetailsName.Size = new Size(332, 39);
            TBRezepteDetailsName.TabIndex = 1;
            // 
            // LBRezepteDetailsName
            // 
            LBRezepteDetailsName.AutoSize = true;
            LBRezepteDetailsName.Location = new Point(6, 59);
            LBRezepteDetailsName.Name = "LBRezepteDetailsName";
            LBRezepteDetailsName.Size = new Size(83, 32);
            LBRezepteDetailsName.TabIndex = 0;
            LBRezepteDetailsName.Text = "Name:";
            // 
            // BTRezepteLoeschen
            // 
            BTRezepteLoeschen.Location = new Point(306, 701);
            BTRezepteLoeschen.Name = "BTRezepteLoeschen";
            BTRezepteLoeschen.Size = new Size(150, 46);
            BTRezepteLoeschen.TabIndex = 3;
            BTRezepteLoeschen.Text = "Löschen";
            BTRezepteLoeschen.UseVisualStyleBackColor = true;
            // 
            // BTRezeptSpeichern
            // 
            BTRezeptSpeichern.Location = new Point(150, 701);
            BTRezeptSpeichern.Name = "BTRezeptSpeichern";
            BTRezeptSpeichern.Size = new Size(150, 46);
            BTRezeptSpeichern.TabIndex = 2;
            BTRezeptSpeichern.Text = "Speichern";
            BTRezeptSpeichern.UseVisualStyleBackColor = true;
            // 
            // BTRezepteNeu
            // 
            BTRezepteNeu.Location = new Point(6, 701);
            BTRezepteNeu.Name = "BTRezepteNeu";
            BTRezepteNeu.Size = new Size(138, 46);
            BTRezepteNeu.TabIndex = 1;
            BTRezepteNeu.Text = "Neu";
            BTRezepteNeu.UseVisualStyleBackColor = true;
            // 
            // LBRezepte
            // 
            LBRezepte.FormattingEnabled = true;
            LBRezepte.HorizontalScrollbar = true;
            LBRezepte.Location = new Point(6, 6);
            LBRezepte.Name = "LBRezepte";
            LBRezepte.Size = new Size(450, 676);
            LBRezepte.TabIndex = 0;
            // 
            // TPUtensilien
            // 
            TPUtensilien.Controls.Add(BTUtensilienLoeschen);
            TPUtensilien.Controls.Add(BTUtensielienHinzufuegen);
            TPUtensilien.Controls.Add(TBUtensielName);
            TPUtensilien.Controls.Add(LBUtensilienVerwalten);
            TPUtensilien.Controls.Add(LBUtensilien);
            TPUtensilien.Location = new Point(8, 46);
            TPUtensilien.Name = "TPUtensilien";
            TPUtensilien.Padding = new Padding(3);
            TPUtensilien.Size = new Size(2102, 969);
            TPUtensilien.TabIndex = 2;
            TPUtensilien.Text = "Utensilien";
            TPUtensilien.UseVisualStyleBackColor = true;
            // 
            // LBZutatenAnzahl
            // 
            LBZutatenAnzahl.AutoSize = true;
            LBZutatenAnzahl.Location = new Point(275, 285);
            LBZutatenAnzahl.Name = "LBZutatenAnzahl";
            LBZutatenAnzahl.Size = new Size(91, 32);
            LBZutatenAnzahl.TabIndex = 7;
            LBZutatenAnzahl.Text = "Anzahl:";
            // 
            // TBZutatenAnzahl
            // 
            TBZutatenAnzahl.Location = new Point(372, 288);
            TBZutatenAnzahl.Name = "TBZutatenAnzahl";
            TBZutatenAnzahl.Size = new Size(47, 39);
            TBZutatenAnzahl.TabIndex = 8;
            // 
            // LBZutatenMenge
            // 
            LBZutatenMenge.AutoSize = true;
            LBZutatenMenge.Location = new Point(447, 285);
            LBZutatenMenge.Name = "LBZutatenMenge";
            LBZutatenMenge.Size = new Size(143, 32);
            LBZutatenMenge.TabIndex = 9;
            LBZutatenMenge.Text = "Menge in g:";
            // 
            // TBZutatenMenge
            // 
            TBZutatenMenge.Location = new Point(596, 288);
            TBZutatenMenge.Name = "TBZutatenMenge";
            TBZutatenMenge.Size = new Size(93, 39);
            TBZutatenMenge.TabIndex = 10;
            // 
            // GBRezepteUtensilien
            // 
            GBRezepteUtensilien.Controls.Add(BTRezepteUtensilienHinzufuegen);
            GBRezepteUtensilien.Controls.Add(CBUtensilienHinzufuegen);
            GBRezepteUtensilien.Controls.Add(LBUtensilienHinzufuegen);
            GBRezepteUtensilien.Controls.Add(BTRezepteUtensilienEntfernen);
            GBRezepteUtensilien.Controls.Add(LBRezepteUtensilien);
            GBRezepteUtensilien.Controls.Add(LBUtensilienImRezept);
            GBRezepteUtensilien.Location = new Point(1289, 6);
            GBRezepteUtensilien.Name = "GBRezepteUtensilien";
            GBRezepteUtensilien.Size = new Size(558, 741);
            GBRezepteUtensilien.TabIndex = 6;
            GBRezepteUtensilien.TabStop = false;
            GBRezepteUtensilien.Text = "Utensilien:";
            // 
            // LBUtensilienImRezept
            // 
            LBUtensilienImRezept.AutoSize = true;
            LBUtensilienImRezept.Location = new Point(6, 35);
            LBUtensilienImRezept.Name = "LBUtensilienImRezept";
            LBUtensilienImRezept.Size = new Size(125, 32);
            LBUtensilienImRezept.TabIndex = 0;
            LBUtensilienImRezept.Text = "Im Rezept:";
            // 
            // LBRezepteUtensilien
            // 
            LBRezepteUtensilien.FormattingEnabled = true;
            LBRezepteUtensilien.HorizontalScrollbar = true;
            LBRezepteUtensilien.Location = new Point(6, 70);
            LBRezepteUtensilien.Name = "LBRezepteUtensilien";
            LBRezepteUtensilien.Size = new Size(240, 260);
            LBRezepteUtensilien.TabIndex = 1;
            // 
            // BTRezepteUtensilienEntfernen
            // 
            BTRezepteUtensilienEntfernen.Location = new Point(6, 336);
            BTRezepteUtensilienEntfernen.Name = "BTRezepteUtensilienEntfernen";
            BTRezepteUtensilienEntfernen.Size = new Size(240, 46);
            BTRezepteUtensilienEntfernen.TabIndex = 2;
            BTRezepteUtensilienEntfernen.Text = "Entfernen";
            BTRezepteUtensilienEntfernen.UseVisualStyleBackColor = true;
            // 
            // BTZutatenHinzufuegen
            // 
            BTZutatenHinzufuegen.Location = new Point(275, 354);
            BTZutatenHinzufuegen.Name = "BTZutatenHinzufuegen";
            BTZutatenHinzufuegen.Size = new Size(414, 46);
            BTZutatenHinzufuegen.TabIndex = 11;
            BTZutatenHinzufuegen.Text = "Hinzufügen";
            BTZutatenHinzufuegen.UseVisualStyleBackColor = true;
            // 
            // LBUtensilienHinzufuegen
            // 
            LBUtensilienHinzufuegen.AutoSize = true;
            LBUtensilienHinzufuegen.Location = new Point(275, 35);
            LBUtensilienHinzufuegen.Name = "LBUtensilienHinzufuegen";
            LBUtensilienHinzufuegen.Size = new Size(144, 32);
            LBUtensilienHinzufuegen.TabIndex = 3;
            LBUtensilienHinzufuegen.Text = "Hinzufügen:";
            // 
            // CBUtensilienHinzufuegen
            // 
            CBUtensilienHinzufuegen.FormattingEnabled = true;
            CBUtensilienHinzufuegen.Location = new Point(275, 70);
            CBUtensilienHinzufuegen.Name = "CBUtensilienHinzufuegen";
            CBUtensilienHinzufuegen.Size = new Size(277, 40);
            CBUtensilienHinzufuegen.TabIndex = 4;
            // 
            // BTRezepteUtensilienHinzufuegen
            // 
            BTRezepteUtensilienHinzufuegen.Location = new Point(275, 335);
            BTRezepteUtensilienHinzufuegen.Name = "BTRezepteUtensilienHinzufuegen";
            BTRezepteUtensilienHinzufuegen.Size = new Size(277, 46);
            BTRezepteUtensilienHinzufuegen.TabIndex = 5;
            BTRezepteUtensilienHinzufuegen.Text = "Hinzufügen";
            BTRezepteUtensilienHinzufuegen.UseVisualStyleBackColor = true;
            // 
            // LBUtensilien
            // 
            LBUtensilien.FormattingEnabled = true;
            LBUtensilien.HorizontalScrollbar = true;
            LBUtensilien.Location = new Point(6, 41);
            LBUtensilien.Name = "LBUtensilien";
            LBUtensilien.Size = new Size(540, 324);
            LBUtensilien.TabIndex = 0;
            // 
            // LBUtensilienVerwalten
            // 
            LBUtensilienVerwalten.AutoSize = true;
            LBUtensilienVerwalten.Location = new Point(6, 6);
            LBUtensilienVerwalten.Name = "LBUtensilienVerwalten";
            LBUtensilienVerwalten.Size = new Size(235, 32);
            LBUtensilienVerwalten.TabIndex = 1;
            LBUtensilienVerwalten.Text = "Utensilien verwalten:";
            // 
            // TBUtensielName
            // 
            TBUtensielName.Location = new Point(6, 371);
            TBUtensielName.Name = "TBUtensielName";
            TBUtensielName.PlaceholderText = "Name des Utensiels";
            TBUtensielName.Size = new Size(235, 39);
            TBUtensielName.TabIndex = 2;
            // 
            // BTUtensielienHinzufuegen
            // 
            BTUtensielienHinzufuegen.Location = new Point(247, 371);
            BTUtensielienHinzufuegen.Name = "BTUtensielienHinzufuegen";
            BTUtensielienHinzufuegen.Size = new Size(150, 46);
            BTUtensielienHinzufuegen.TabIndex = 3;
            BTUtensielienHinzufuegen.Text = "Hinzufügen:";
            BTUtensielienHinzufuegen.UseVisualStyleBackColor = true;
            // 
            // BTUtensilienLoeschen
            // 
            BTUtensilienLoeschen.Location = new Point(403, 371);
            BTUtensilienLoeschen.Name = "BTUtensilienLoeschen";
            BTUtensilienLoeschen.Size = new Size(143, 46);
            BTUtensilienLoeschen.TabIndex = 4;
            BTUtensilienLoeschen.Text = "Löschen";
            BTUtensilienLoeschen.UseVisualStyleBackColor = true;
            // 
            // BTDatenLoeschen
            // 
            BTDatenLoeschen.Location = new Point(450, 457);
            BTDatenLoeschen.Name = "BTDatenLoeschen";
            BTDatenLoeschen.Size = new Size(542, 46);
            BTDatenLoeschen.TabIndex = 8;
            BTDatenLoeschen.Text = "Daten Löschen";
            BTDatenLoeschen.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new Size(2118, 1035);
            Controls.Add(TCAnzeige);
            Margin = new Padding(4);
            Name = "Form1";
            Text = "iFood-Lebensmittelverwaltung";
            TCAnzeige.ResumeLayout(false);
            TPLebensmittel.ResumeLayout(false);
            TPLebensmittel.PerformLayout();
            GBNaehrwerte.ResumeLayout(false);
            GBNaehrwerte.PerformLayout();
            TPRezepte.ResumeLayout(false);
            GBRezepteZutaten.ResumeLayout(false);
            GBRezepteZutaten.PerformLayout();
            GBRezepteDetails.ResumeLayout(false);
            GBRezepteDetails.PerformLayout();
            TPUtensilien.ResumeLayout(false);
            TPUtensilien.PerformLayout();
            GBRezepteUtensilien.ResumeLayout(false);
            GBRezepteUtensilien.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TabControl TCAnzeige;
        private TabPage TPLebensmittel;
        private GroupBox GBNaehrwerte;
        private TextBox TBSalz;
        private Label LBSalz;
        private TextBox TBEiweiss;
        private TextBox TBZucker;
        private TextBox TBKohlenhydrate;
        private TextBox TBDavonGesaettigteFettsaeuren;
        private TextBox TBFett;
        private TextBox TBKalorien;
        private Label LBEiweiss;
        private Label LBDavonGesaettigteFettsaeuren;
        private Label LBZucker;
        private Label LBFett;
        private Label LBKohlenhydrate;
        private Label LBKalorien;
        private ListBox LBErgebnis;
        private Button BTSuche;
        private TextBox TBSuche;
        private TabPage TPRezepte;
        private TabPage TPUtensilien;
        private Button BTRezepteLoeschen;
        private Button BTRezeptSpeichern;
        private Button BTRezepteNeu;
        private ListBox LBRezepte;
        private GroupBox GBRezepteDetails;
        private TextBox TBRezepteAnleitung;
        private TextBox TBRezepteDetailsPortionen;
        private TextBox TBRezepteDetailsDauer;
        private TextBox TBRezepteDetailsName;
        private Label LBRezepteDetailsName;
        private Label LBRezepteDetailsPortionen;
        private Label LBRezepteDetailsDauer;
        private GroupBox GBRezepteZutaten;
        private Label LBRezepteDetailsAnleitung;
        private ListBox LBZutaten;
        private Label LBRezepteZutatenImRezept;
        private Button BTRezepteZutatenEntfernen;
        private Label LBRezepteZutatenHinzufuegen;
        private Button BTRezepteZutatenSuchen;
        private TextBox TBRezepteZutatenHinzufuegen;
        private ListBox LBZutatenHinzufuegen;
        private Label LBZutatenAnzahl;
        private TextBox TBZutatenMenge;
        private Label LBZutatenMenge;
        private TextBox TBZutatenAnzahl;
        private GroupBox GBRezepteUtensilien;
        private ListBox LBRezepteUtensilien;
        private Label LBUtensilienImRezept;
        private Label LBUtensilienHinzufuegen;
        private Button BTRezepteUtensilienEntfernen;
        private Button BTZutatenHinzufuegen;
        private Button BTRezepteUtensilienHinzufuegen;
        private ComboBox CBUtensilienHinzufuegen;
        private ListBox LBUtensilien;
        private Label LBUtensilienVerwalten;
        private Button BTUtensilienLoeschen;
        private Button BTUtensielienHinzufuegen;
        private TextBox TBUtensielName;
        private Button BTDatenLoeschen;
    }
}
