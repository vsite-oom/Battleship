
namespace Battleships
{
    partial class Battleship
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
            this.StartGame = new System.Windows.Forms.Button();
            this.PCFleetControl = new Battleships.FleetControl();
            this.PlayerFleetControl = new Battleships.FleetControl();
            this.SuspendLayout();
            // 
            // StartGame
            // 
            this.StartGame.Location = new System.Drawing.Point(724, 459);
            this.StartGame.Name = "StartGame";
            this.StartGame.Size = new System.Drawing.Size(75, 23);
            this.StartGame.TabIndex = 2;
            this.StartGame.Text = "Place ships";
            this.StartGame.UseVisualStyleBackColor = true;
            this.StartGame.Click += new System.EventHandler(this.StartGame_Click);
            // 
            // PCFleetControl
            // 
            this.PCFleetControl.Location = new System.Drawing.Point(415, 37);
            this.PCFleetControl.Name = "PCFleetControl";
            this.PCFleetControl.Size = new System.Drawing.Size(399, 401);
            this.PCFleetControl.TabIndex = 1;
            // 
            // PlayerFleetControl
            // 
            this.PlayerFleetControl.Location = new System.Drawing.Point(12, 37);
            this.PlayerFleetControl.Name = "PlayerFleetControl";
            this.PlayerFleetControl.Size = new System.Drawing.Size(397, 401);
            this.PlayerFleetControl.TabIndex = 0;
            // 
            // Battleship
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(830, 514);
            this.Controls.Add(this.StartGame);
            this.Controls.Add(this.PCFleetControl);
            this.Controls.Add(this.PlayerFleetControl);
            this.Name = "Battleship";
            this.Text = "Battleship";
            this.ResumeLayout(false);

        }

        #endregion

        private FleetControl PlayerFleetControl;
        private FleetControl PCFleetControl;
        private System.Windows.Forms.Button StartGame;
    }
}

