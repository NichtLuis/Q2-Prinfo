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
            TPRezepte = new TabPage();
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
            TCAnzeige.Name = "TCAnzeige";
            TCAnzeige.SelectedIndex = 0;
            TCAnzeige.Size = new Size(1476, 784);
            TCAnzeige.TabIndex = 0;
            // 
            // TPLebensmittel
            // 
            TPLebensmittel.Controls.Add(GBNaehrwerte);
            TPLebensmittel.Controls.Add(LBErgebnis);
            TPLebensmittel.Controls.Add(BTSuche);
            TPLebensmittel.Controls.Add(TBSuche);
            TPLebensmittel.Location = new Point(8, 46);
            TPLebensmittel.Name = "TPLebensmittel";
            TPLebensmittel.Padding = new Padding(3);
            TPLebensmittel.Size = new Size(1460, 730);
            TPLebensmittel.TabIndex = 0;
            TPLebensmittel.Text = "Lebensmittel";
            TPLebensmittel.UseVisualStyleBackColor = true;
            // 
            // TPRezepte
            // 
            TPRezepte.Location = new Point(8, 46);
            TPRezepte.Name = "TPRezepte";
            TPRezepte.Padding = new Padding(3);
            TPRezepte.Size = new Size(1460, 730);
            TPRezepte.TabIndex = 1;
            TPRezepte.Text = "Rezepte";
            TPRezepte.UseVisualStyleBackColor = true;
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
            LBErgebnis.ItemHeight = 25;
            LBErgebnis.Location = new Point(12, 302);
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
            // TPUtensilien
            // 
            TPUtensilien.Location = new Point(8, 46);
            TPUtensilien.Name = "TPUtensilien";
            TPUtensilien.Padding = new Padding(3);
            TPUtensilien.Size = new Size(1460, 730);
            TPUtensilien.TabIndex = 2;
            TPUtensilien.Text = "Utensilien";
            TPUtensilien.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new Size(1476, 861);
            Controls.Add(TCAnzeige);
            Margin = new Padding(4);
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
