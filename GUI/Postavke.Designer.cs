
namespace GUI
{
    partial class Postavke
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblGrid = new System.Windows.Forms.Label();
            this.lblFlota = new System.Windows.Forms.Label();
            this.cBoxFlota = new System.Windows.Forms.ComboBox();
            this.lblRed = new System.Windows.Forms.Label();
            this.lblStupac = new System.Windows.Forms.Label();
            this.cBoxRedak = new System.Windows.Forms.ComboBox();
            this.cBoxStupac = new System.Windows.Forms.ComboBox();
            this.btnReturn = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDodajBrod = new System.Windows.Forms.Button();
            this.btnMakniBrod = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblGrid
            // 
            this.lblGrid.AutoSize = true;
            this.lblGrid.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblGrid.Location = new System.Drawing.Point(12, 9);
            this.lblGrid.Name = "lblGrid";
            this.lblGrid.Size = new System.Drawing.Size(48, 25);
            this.lblGrid.TabIndex = 0;
            this.lblGrid.Text = "Grid";
            // 
            // lblFlota
            // 
            this.lblFlota.AutoSize = true;
            this.lblFlota.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblFlota.Location = new System.Drawing.Point(12, 177);
            this.lblFlota.Name = "lblFlota";
            this.lblFlota.Size = new System.Drawing.Size(55, 25);
            this.lblFlota.TabIndex = 1;
            this.lblFlota.Text = "Flota";
            // 
            // cBoxFlota
            // 
            this.cBoxFlota.FormattingEnabled = true;
            this.cBoxFlota.Location = new System.Drawing.Point(73, 177);
            this.cBoxFlota.Name = "cBoxFlota";
            this.cBoxFlota.Size = new System.Drawing.Size(246, 24);
            this.cBoxFlota.TabIndex = 7;
            this.cBoxFlota.SelectedIndexChanged += new System.EventHandler(this.cBoxFlota_SelectedIndexChanged);
            // 
            // lblRed
            // 
            this.lblRed.AutoSize = true;
            this.lblRed.Location = new System.Drawing.Point(14, 63);
            this.lblRed.Name = "lblRed";
            this.lblRed.Size = new System.Drawing.Size(85, 17);
            this.lblRed.TabIndex = 3;
            this.lblRed.Text = "Broj redaka:";
            // 
            // lblStupac
            // 
            this.lblStupac.AutoSize = true;
            this.lblStupac.Location = new System.Drawing.Point(14, 110);
            this.lblStupac.Name = "lblStupac";
            this.lblStupac.Size = new System.Drawing.Size(91, 17);
            this.lblStupac.TabIndex = 4;
            this.lblStupac.Text = "Broj stupaca:";
            // 
            // cBoxRedak
            // 
            this.cBoxRedak.DisplayMember = "1,2,3,4,5,6,7,8,9,10";
            this.cBoxRedak.FormattingEnabled = true;
            this.cBoxRedak.Items.AddRange(new object[] {
            "7",
            "8",
            "9",
            "10",
            "11",
            "12"});
            this.cBoxRedak.Location = new System.Drawing.Point(244, 56);
            this.cBoxRedak.Name = "cBoxRedak";
            this.cBoxRedak.Size = new System.Drawing.Size(76, 24);
            this.cBoxRedak.TabIndex = 5;
            this.cBoxRedak.SelectedIndexChanged += new System.EventHandler(this.cBoxRedak_SelectedIndexChanged);
            // 
            // cBoxStupac
            // 
            this.cBoxStupac.FormattingEnabled = true;
            this.cBoxStupac.Items.AddRange(new object[] {
            "7",
            "8",
            "9",
            "10",
            "11",
            "12"});
            this.cBoxStupac.Location = new System.Drawing.Point(244, 103);
            this.cBoxStupac.Name = "cBoxStupac";
            this.cBoxStupac.Size = new System.Drawing.Size(75, 24);
            this.cBoxStupac.TabIndex = 6;
            this.cBoxStupac.SelectedIndexChanged += new System.EventHandler(this.cBoxStupac_SelectedIndexChanged);
            // 
            // btnReturn
            // 
            this.btnReturn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReturn.Location = new System.Drawing.Point(13, 387);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(307, 54);
            this.btnReturn.TabIndex = 8;
            this.btnReturn.Text = "POVRATAK NA MENI";
            this.btnReturn.UseVisualStyleBackColor = true;
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(12, 327);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(307, 54);
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "SPREMI";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDodajBrod
            // 
            this.btnDodajBrod.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDodajBrod.Location = new System.Drawing.Point(197, 292);
            this.btnDodajBrod.Name = "btnDodajBrod";
            this.btnDodajBrod.Size = new System.Drawing.Size(121, 29);
            this.btnDodajBrod.TabIndex = 10;
            this.btnDodajBrod.Text = "DODAJ BROD";
            this.btnDodajBrod.UseVisualStyleBackColor = true;
            this.btnDodajBrod.Click += new System.EventHandler(this.btnDodajBrod_Click);
            // 
            // btnMakniBrod
            // 
            this.btnMakniBrod.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnMakniBrod.Location = new System.Drawing.Point(13, 292);
            this.btnMakniBrod.Name = "btnMakniBrod";
            this.btnMakniBrod.Size = new System.Drawing.Size(121, 29);
            this.btnMakniBrod.TabIndex = 11;
            this.btnMakniBrod.Text = "MAKNI BROD";
            this.btnMakniBrod.UseVisualStyleBackColor = true;
            this.btnMakniBrod.Click += new System.EventHandler(this.btnMakniBrod_Click);
            // 
            // Postavke
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(332, 453);
            this.Controls.Add(this.btnMakniBrod);
            this.Controls.Add(this.btnDodajBrod);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnReturn);
            this.Controls.Add(this.cBoxStupac);
            this.Controls.Add(this.cBoxRedak);
            this.Controls.Add(this.lblStupac);
            this.Controls.Add(this.lblRed);
            this.Controls.Add(this.cBoxFlota);
            this.Controls.Add(this.lblFlota);
            this.Controls.Add(this.lblGrid);
            this.MaximumSize = new System.Drawing.Size(350, 500);
            this.MinimumSize = new System.Drawing.Size(350, 500);
            this.Name = "Postavke";
            this.Text = "Postavke";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblGrid;
        private System.Windows.Forms.Label lblFlota;
        private System.Windows.Forms.ComboBox cBoxFlota;
        private System.Windows.Forms.Label lblRed;
        private System.Windows.Forms.Label lblStupac;
        private System.Windows.Forms.ComboBox cBoxRedak;
        private System.Windows.Forms.ComboBox cBoxStupac;
        private System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDodajBrod;
        private System.Windows.Forms.Button btnMakniBrod;
    }
}