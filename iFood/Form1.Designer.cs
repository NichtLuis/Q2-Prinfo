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
            CBRezeptAuswahl = new ComboBox();
            TBSuche = new TextBox();
            BTSuche = new Button();
            LBDebug = new Label();
            LBErgebnis = new ListBox();
            SuspendLayout();
            // 
            // CBRezeptAuswahl
            // 
            CBRezeptAuswahl.FormattingEnabled = true;
            CBRezeptAuswahl.Location = new Point(358, 119);
            CBRezeptAuswahl.Name = "CBRezeptAuswahl";
            CBRezeptAuswahl.Size = new Size(182, 33);
            CBRezeptAuswahl.TabIndex = 0;
            // 
            // TBSuche
            // 
            TBSuche.Location = new Point(374, 191);
            TBSuche.Name = "TBSuche";
            TBSuche.PlaceholderText = "Stichworte zur suche eingeben";
            TBSuche.Size = new Size(438, 31);
            TBSuche.TabIndex = 1;
            // 
            // BTSuche
            // 
            BTSuche.Location = new Point(895, 209);
            BTSuche.Name = "BTSuche";
            BTSuche.Size = new Size(163, 93);
            BTSuche.TabIndex = 2;
            BTSuche.Text = "Suchen";
            BTSuche.UseVisualStyleBackColor = true;
            BTSuche.Click += BTSuche_Click;
            // 
            // LBDebug
            // 
            LBDebug.AutoSize = true;
            LBDebug.Location = new Point(696, 127);
            LBDebug.Name = "LBDebug";
            LBDebug.Size = new Size(66, 25);
            LBDebug.TabIndex = 3;
            LBDebug.Text = "Debug";
            // 
            // LBErgebnis
            // 
            LBErgebnis.FormattingEnabled = true;
            LBErgebnis.ItemHeight = 25;
            LBErgebnis.Location = new Point(12, 302);
            LBErgebnis.Name = "LBErgebnis";
            LBErgebnis.Size = new Size(727, 429);
            LBErgebnis.TabIndex = 4;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1745, 774);
            Controls.Add(LBErgebnis);
            Controls.Add(LBDebug);
            Controls.Add(BTSuche);
            Controls.Add(TBSuche);
            Controls.Add(CBRezeptAuswahl);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox CBRezeptAuswahl;
        private TextBox TBSuche;
        private Button BTSuche;
        private Label LBDebug;
        private ListBox LBErgebnis;
    }
}
