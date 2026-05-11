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
            TPUtensilien = new TabPage();
            TCAnzeige.SuspendLayout();
            TPLebensmittel.SuspendLayout();
            GBNaehrwerte.SuspendLayout();
            SuspendLayout();
            // 
            // TCAnzeige
            // 
            TCAnzeige.Controls.Add(TPLebensmittel);
            TCAnzeige.Controls.Add(TPRezepte);
            TCAnzeige.Controls.Add(TPUtensilien);
            TCAnzeige.Dock = DockStyle.Top;
            TCAnzeige.Location = new Point(0, 0);
            TCAnzeige.Margin = new Padding(2);
            TCAnzeige.Name = "TCAnzeige";
            TCAnzeige.SelectedIndex = 0;
            TCAnzeige.Size = new Size(1135, 612);
            TCAnzeige.TabIndex = 0;
            // 
            // TPLebensmittel
            // 
            TPLebensmittel.Controls.Add(GBNaehrwerte);
            TPLebensmittel.Controls.Add(LBErgebnis);
            TPLebensmittel.Controls.Add(BTSuche);
            TPLebensmittel.Controls.Add(TBSuche);
            TPLebensmittel.Location = new Point(4, 34);
            TPLebensmittel.Margin = new Padding(2);
            TPLebensmittel.Name = "TPLebensmittel";
            TPLebensmittel.Padding = new Padding(2);
            TPLebensmittel.Size = new Size(1127, 574);
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
            GBNaehrwerte.Location = new Point(346, 6);
            GBNaehrwerte.Margin = new Padding(2);
            GBNaehrwerte.Name = "GBNaehrwerte";
            GBNaehrwerte.Padding = new Padding(2);
            GBNaehrwerte.Size = new Size(417, 336);
            GBNaehrwerte.TabIndex = 7;
            GBNaehrwerte.TabStop = false;
            GBNaehrwerte.Text = "Nährwerte";
            // 
            // TBSalz
            // 
            TBSalz.Location = new Point(91, 245);
            TBSalz.Margin = new Padding(2);
            TBSalz.Name = "TBSalz";
            TBSalz.PlaceholderText = "g";
            TBSalz.Size = new Size(155, 31);
            TBSalz.TabIndex = 21;
            // 
            // LBSalz
            // 
            LBSalz.AutoSize = true;
            LBSalz.Location = new Point(5, 245);
            LBSalz.Margin = new Padding(2, 0, 2, 0);
            LBSalz.Name = "LBSalz";
            LBSalz.Size = new Size(47, 25);
            LBSalz.TabIndex = 20;
            LBSalz.Text = "Salz:";
            // 
            // TBEiweiss
            // 
            TBEiweiss.Location = new Point(91, 209);
            TBEiweiss.Margin = new Padding(2);
            TBEiweiss.Name = "TBEiweiss";
            TBEiweiss.PlaceholderText = "g";
            TBEiweiss.Size = new Size(155, 31);
            TBEiweiss.TabIndex = 19;
            // 
            // TBZucker
            // 
            TBZucker.Location = new Point(91, 174);
            TBZucker.Margin = new Padding(2);
            TBZucker.Name = "TBZucker";
            TBZucker.PlaceholderText = "g";
            TBZucker.Size = new Size(155, 31);
            TBZucker.TabIndex = 18;
            // 
            // TBKohlenhydrate
            // 
            TBKohlenhydrate.Location = new Point(143, 139);
            TBKohlenhydrate.Margin = new Padding(2);
            TBKohlenhydrate.Name = "TBKohlenhydrate";
            TBKohlenhydrate.PlaceholderText = "g";
            TBKohlenhydrate.Size = new Size(155, 31);
            TBKohlenhydrate.TabIndex = 17;
            // 
            // TBDavonGesaettigteFettsaeuren
            // 
            TBDavonGesaettigteFettsaeuren.Location = new Point(204, 104);
            TBDavonGesaettigteFettsaeuren.Margin = new Padding(2);
            TBDavonGesaettigteFettsaeuren.Name = "TBDavonGesaettigteFettsaeuren";
            TBDavonGesaettigteFettsaeuren.PlaceholderText = "g";
            TBDavonGesaettigteFettsaeuren.Size = new Size(155, 31);
            TBDavonGesaettigteFettsaeuren.TabIndex = 16;
            // 
            // TBFett
            // 
            TBFett.Location = new Point(91, 69);
            TBFett.Margin = new Padding(2);
            TBFett.Name = "TBFett";
            TBFett.PlaceholderText = "g";
            TBFett.Size = new Size(155, 31);
            TBFett.TabIndex = 15;
            // 
            // TBKalorien
            // 
            TBKalorien.Location = new Point(91, 34);
            TBKalorien.Margin = new Padding(2);
            TBKalorien.Name = "TBKalorien";
            TBKalorien.PlaceholderText = "kcal";
            TBKalorien.Size = new Size(155, 31);
            TBKalorien.TabIndex = 14;
            // 
            // LBEiweiss
            // 
            LBEiweiss.AutoSize = true;
            LBEiweiss.Location = new Point(5, 209);
            LBEiweiss.Margin = new Padding(2, 0, 2, 0);
            LBEiweiss.Name = "LBEiweiss";
            LBEiweiss.Size = new Size(65, 25);
            LBEiweiss.TabIndex = 13;
            LBEiweiss.Text = "Eiweiß:";
            // 
            // LBDavonGesaettigteFettsaeuren
            // 
            LBDavonGesaettigteFettsaeuren.AutoSize = true;
            LBDavonGesaettigteFettsaeuren.Location = new Point(5, 102);
            LBDavonGesaettigteFettsaeuren.Margin = new Padding(2, 0, 2, 0);
            LBDavonGesaettigteFettsaeuren.Name = "LBDavonGesaettigteFettsaeuren";
            LBDavonGesaettigteFettsaeuren.Size = new Size(190, 25);
            LBDavonGesaettigteFettsaeuren.TabIndex = 11;
            LBDavonGesaettigteFettsaeuren.Text = "davon ges. Fettsäuren:";
            // 
            // LBZucker
            // 
            LBZucker.AutoSize = true;
            LBZucker.Location = new Point(5, 174);
            LBZucker.Margin = new Padding(2, 0, 2, 0);
            LBZucker.Name = "LBZucker";
            LBZucker.Size = new Size(68, 25);
            LBZucker.TabIndex = 9;
            LBZucker.Text = "Zucker:";
            // 
            // LBFett
            // 
            LBFett.AutoSize = true;
            LBFett.Location = new Point(5, 69);
            LBFett.Margin = new Padding(2, 0, 2, 0);
            LBFett.Name = "LBFett";
            LBFett.Size = new Size(46, 25);
            LBFett.TabIndex = 7;
            LBFett.Text = "Fett:";
            // 
            // LBKohlenhydrate
            // 
            LBKohlenhydrate.AutoSize = true;
            LBKohlenhydrate.Location = new Point(5, 139);
            LBKohlenhydrate.Margin = new Padding(2, 0, 2, 0);
            LBKohlenhydrate.Name = "LBKohlenhydrate";
            LBKohlenhydrate.Size = new Size(130, 25);
            LBKohlenhydrate.TabIndex = 5;
            LBKohlenhydrate.Text = "Kohlenhydrate:";
            // 
            // LBKalorien
            // 
            LBKalorien.AutoSize = true;
            LBKalorien.Location = new Point(5, 34);
            LBKalorien.Margin = new Padding(2, 0, 2, 0);
            LBKalorien.Name = "LBKalorien";
            LBKalorien.Size = new Size(79, 25);
            LBKalorien.TabIndex = 0;
            LBKalorien.Text = "Kalorien:";
            // 
            // LBErgebnis
            // 
            LBErgebnis.FormattingEnabled = true;
            LBErgebnis.HorizontalScrollbar = true;
            LBErgebnis.ItemHeight = 25;
            LBErgebnis.Location = new Point(9, 236);
            LBErgebnis.Margin = new Padding(2);
            LBErgebnis.Name = "LBErgebnis";
            LBErgebnis.Size = new Size(338, 354);
            LBErgebnis.TabIndex = 6;
            LBErgebnis.SelectedIndexChanged += LBErgebnis_SelectedIndexChanged;
            // 
            // BTSuche
            // 
            BTSuche.Location = new Point(211, 5);
            BTSuche.Margin = new Padding(2);
            BTSuche.Name = "BTSuche";
            BTSuche.Size = new Size(131, 30);
            BTSuche.TabIndex = 5;
            BTSuche.Text = "Suchen";
            BTSuche.UseVisualStyleBackColor = true;
            BTSuche.Click += BTSuche_Click;
            // 
            // TBSuche
            // 
            TBSuche.Location = new Point(5, 5);
            TBSuche.Margin = new Padding(2);
            TBSuche.Name = "TBSuche";
            TBSuche.PlaceholderText = "Suche nach z.B. Apfel";
            TBSuche.Size = new Size(201, 31);
            TBSuche.TabIndex = 4;
            // 
            // TPRezepte
            // 
            TPRezepte.Location = new Point(4, 34);
            TPRezepte.Margin = new Padding(2);
            TPRezepte.Name = "TPRezepte";
            TPRezepte.Padding = new Padding(2);
            TPRezepte.Size = new Size(1127, 574);
            TPRezepte.TabIndex = 1;
            TPRezepte.Text = "Rezepte";
            TPRezepte.UseVisualStyleBackColor = true;
            // 
            // TPUtensilien
            // 
            TPUtensilien.Location = new Point(4, 34);
            TPUtensilien.Margin = new Padding(2);
            TPUtensilien.Name = "TPUtensilien";
            TPUtensilien.Padding = new Padding(2);
            TPUtensilien.Size = new Size(1127, 574);
            TPUtensilien.TabIndex = 2;
            TPUtensilien.Text = "Utensilien";
            TPUtensilien.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new Size(1135, 673);
            Controls.Add(TCAnzeige);
            Name = "Form1";
            Text = "iFood-Lebensmittelverwaltung";
            TCAnzeige.ResumeLayout(false);
            TPLebensmittel.ResumeLayout(false);
            TPLebensmittel.PerformLayout();
            GBNaehrwerte.ResumeLayout(false);
            GBNaehrwerte.PerformLayout();
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
    }
}
