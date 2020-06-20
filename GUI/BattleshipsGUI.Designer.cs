using System.Data;
using Vsite.Oom.Battleship.Model;

namespace Vsite.Oom.Battleship.GUI
{
    partial class BattleshipsGUI
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
            this.newGameButton = new System.Windows.Forms.Button();
            this.enemyFleetGrid = new Vsite.Oom.Battleship.GUI.DrawFleetGrid();
            this.playerFleetGrid = new Vsite.Oom.Battleship.GUI.DrawFleetGrid();
            this.SuspendLayout();
            // 
            // newGameButton
            // 
            this.newGameButton.Location = new System.Drawing.Point(525, 595);
            this.newGameButton.Name = "newGameButton";
            this.newGameButton.Size = new System.Drawing.Size(111, 46);
            this.newGameButton.TabIndex = 1;
            this.newGameButton.Text = "New game";
            this.newGameButton.UseVisualStyleBackColor = true;
            this.newGameButton.Click += new System.EventHandler(this.newGameButton_Click);
            // 
            // enemyFleetGrid
            // 
            this.enemyFleetGrid.BackColor = System.Drawing.Color.Aqua;
            this.enemyFleetGrid.Fleet = null;
            this.enemyFleetGrid.Lines = 0;
            this.enemyFleetGrid.Location = new System.Drawing.Point(606, 9);
            this.enemyFleetGrid.Margin = new System.Windows.Forms.Padding(0);
            this.enemyFleetGrid.MinimumSize = new System.Drawing.Size(550, 550);
            this.enemyFleetGrid.Name = "enemyFleetGrid";
            this.enemyFleetGrid.playerGrid = Vsite.Oom.Battleship.GUI.DrawFleetGrid.PlayerGridType.ENEMY;
            this.enemyFleetGrid.Size = new System.Drawing.Size(550, 550);
            this.enemyFleetGrid.SquareTerminator = null;
            this.enemyFleetGrid.TabIndex = 2;
            // 
            // playerFleetGrid
            // 
            this.playerFleetGrid.BackColor = System.Drawing.Color.Aqua;
            this.playerFleetGrid.Fleet = null;
            this.playerFleetGrid.Lines = 0;
            this.playerFleetGrid.Location = new System.Drawing.Point(9, 9);
            this.playerFleetGrid.Margin = new System.Windows.Forms.Padding(0);
            this.playerFleetGrid.MinimumSize = new System.Drawing.Size(550, 550);
            this.playerFleetGrid.Name = "playerFleetGrid";
            this.playerFleetGrid.playerGrid = Vsite.Oom.Battleship.GUI.DrawFleetGrid.PlayerGridType.PLAYER;
            this.playerFleetGrid.Size = new System.Drawing.Size(550, 550);
            this.playerFleetGrid.SquareTerminator = null;
            this.playerFleetGrid.TabIndex = 0;
            // 
            // BattleshipsGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1165, 653);
            this.Controls.Add(this.newGameButton);
            this.Controls.Add(this.enemyFleetGrid);
            this.Controls.Add(this.playerFleetGrid);
            this.DoubleBuffered = true;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1364, 736);
            this.MinimizeBox = false;
            this.Name = "BattleshipsGUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Battleships";
            this.ResumeLayout(false);

        }

        #endregion

        private DrawFleetGrid playerFleetGrid;
        private System.Windows.Forms.Button newGameButton;
        private DrawFleetGrid enemyFleetGrid;
    }
}

