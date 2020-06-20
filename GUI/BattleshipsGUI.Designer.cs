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
            this.playerFleetGrid = new Vsite.Oom.Battleship.GUI.DrawFleetGrid();
            this.enemyFleetGrid = new Vsite.Oom.Battleship.GUI.DrawFleetGrid();
            this.SuspendLayout();
            // 
            // newGameButton
            // 
            this.newGameButton.Location = new System.Drawing.Point(664, 639);
            this.newGameButton.Name = "newGameButton";
            this.newGameButton.Size = new System.Drawing.Size(75, 23);
            this.newGameButton.TabIndex = 1;
            this.newGameButton.Text = "New game";
            this.newGameButton.UseVisualStyleBackColor = true;
            this.newGameButton.Click += new System.EventHandler(this.newGameButton_Click);
            // 
            // playerFleetGrid
            // 
            this.playerFleetGrid.Fleet = null;
            this.playerFleetGrid.Lines = 0;
            this.playerFleetGrid.Location = new System.Drawing.Point(72, 23);
            this.playerFleetGrid.MinimumSize = new System.Drawing.Size(550, 550);
            this.playerFleetGrid.Name = "playerFleetGrid";
            this.playerFleetGrid.playerGrid = DrawFleetGrid.PlayerGridType.PLAYER;
            this.playerFleetGrid.SquareTerminator = new SquareTerminator(RulesSingleton.Instance.Rows, RulesSingleton.Instance.Columns);
            this.playerFleetGrid.Shipwright = new Shipwright(RulesSingleton.Instance.Rows, RulesSingleton.Instance.Columns, this.playerFleetGrid.SquareTerminator);
            this.playerFleetGrid.Size = new System.Drawing.Size(550, 550);
            this.playerFleetGrid.TabIndex = 0;
            // 
            // enemyFleetGrid
            // 
            this.enemyFleetGrid.Fleet = null;
            this.enemyFleetGrid.Lines = 0;
            this.enemyFleetGrid.Location = new System.Drawing.Point(725, 23);
            this.enemyFleetGrid.MinimumSize = new System.Drawing.Size(550, 550);
            this.enemyFleetGrid.Name = "enemyFleetGrid";
            this.enemyFleetGrid.playerGrid = DrawFleetGrid.PlayerGridType.ENEMY;
            this.enemyFleetGrid.SquareTerminator = new SquareTerminator(RulesSingleton.Instance.Rows, RulesSingleton.Instance.Columns);
            this.enemyFleetGrid.Shipwright = new Shipwright(RulesSingleton.Instance.Rows, RulesSingleton.Instance.Columns, this.playerFleetGrid.SquareTerminator);
            this.enemyFleetGrid.Size = new System.Drawing.Size(550, 550);
            this.enemyFleetGrid.TabIndex = 2;
            // 
            // BattleshipsGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1348, 697);
            this.Controls.Add(this.enemyFleetGrid);
            this.Controls.Add(this.newGameButton);
            this.Controls.Add(this.playerFleetGrid);
            this.DoubleBuffered = true;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1364, 736);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1364, 736);
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

