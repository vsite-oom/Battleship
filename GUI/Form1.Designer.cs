namespace GUI
{
    partial class BattleshipWindow
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
            this.ShipPlacingButton = new System.Windows.Forms.Button();
            this.ResetShipPlacingButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ShipPlacingButton
            // 
            this.ShipPlacingButton.BackColor = System.Drawing.Color.CornflowerBlue;
            this.ShipPlacingButton.Font = new System.Drawing.Font("Courier New", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ShipPlacingButton.Location = new System.Drawing.Point(502, 12);
            this.ShipPlacingButton.Name = "ShipPlacingButton";
            this.ShipPlacingButton.Size = new System.Drawing.Size(235, 85);
            this.ShipPlacingButton.TabIndex = 0;
            this.ShipPlacingButton.Text = "Place Ships";
            this.ShipPlacingButton.UseVisualStyleBackColor = false;
            this.ShipPlacingButton.Click += new System.EventHandler(this.ShipPlacingButton_Click);
            // 
            // ResetShipPlacingButton
            // 
            this.ResetShipPlacingButton.BackColor = System.Drawing.Color.CornflowerBlue;
            this.ResetShipPlacingButton.Cursor = System.Windows.Forms.Cursors.Default;
            this.ResetShipPlacingButton.Font = new System.Drawing.Font("Courier New", 14.25F, System.Drawing.FontStyle.Bold);
            this.ResetShipPlacingButton.Location = new System.Drawing.Point(502, 103);
            this.ResetShipPlacingButton.Name = "ResetShipPlacingButton";
            this.ResetShipPlacingButton.Size = new System.Drawing.Size(235, 31);
            this.ResetShipPlacingButton.TabIndex = 1;
            this.ResetShipPlacingButton.Text = "Reset";
            this.ResetShipPlacingButton.UseVisualStyleBackColor = false;
            this.ResetShipPlacingButton.Visible = false;
            this.ResetShipPlacingButton.Click += new System.EventHandler(this.ResetShipPlacingButton_Click);
            // 
            // BattleshipWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(759, 514);
            this.Controls.Add(this.ResetShipPlacingButton);
            this.Controls.Add(this.ShipPlacingButton);
            this.Name = "BattleshipWindow";
            this.Text = "Battleship";
            this.Load += new System.EventHandler(this.BattleshipWindow_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button ShipPlacingButton;
        private System.Windows.Forms.Button ResetShipPlacingButton;
    }
}

