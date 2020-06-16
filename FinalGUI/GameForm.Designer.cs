namespace FinalGUI
{
    partial class GameForm
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
            this.groupBoxComputer = new System.Windows.Forms.GroupBox();
            this.gameGrid2 = new FinalGUI.GameGrid();
            this.groupBoxPlayer = new System.Windows.Forms.GroupBox();
            this.playersGrid = new FinalGUI.PlayersGrid();
            this.buttonDeploy = new System.Windows.Forms.Button();
            this.buttonStartGame = new System.Windows.Forms.Button();
            this.groupBoxComputer.SuspendLayout();
            this.groupBoxPlayer.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxComputer
            // 
            this.groupBoxComputer.Controls.Add(this.gameGrid2);
            this.groupBoxComputer.Location = new System.Drawing.Point(660, 14);
            this.groupBoxComputer.Margin = new System.Windows.Forms.Padding(5);
            this.groupBoxComputer.Name = "groupBoxComputer";
            this.groupBoxComputer.Padding = new System.Windows.Forms.Padding(0);
            this.groupBoxComputer.Size = new System.Drawing.Size(460, 475);
            this.groupBoxComputer.TabIndex = 0;
            this.groupBoxComputer.TabStop = false;
            this.groupBoxComputer.Text = "Computer";
            // 
            // gameGrid2
            // 
            this.gameGrid2.Location = new System.Drawing.Point(4, 17);
            this.gameGrid2.Name = "gameGrid2";
            this.gameGrid2.Size = new System.Drawing.Size(440, 440);
            this.gameGrid2.TabIndex = 0;
            // 
            // groupBoxPlayer
            // 
            this.groupBoxPlayer.Controls.Add(this.playersGrid);
            this.groupBoxPlayer.Location = new System.Drawing.Point(190, 14);
            this.groupBoxPlayer.Margin = new System.Windows.Forms.Padding(5);
            this.groupBoxPlayer.Name = "groupBoxPlayer";
            this.groupBoxPlayer.Padding = new System.Windows.Forms.Padding(0);
            this.groupBoxPlayer.Size = new System.Drawing.Size(460, 475);
            this.groupBoxPlayer.TabIndex = 0;
            this.groupBoxPlayer.TabStop = false;
            this.groupBoxPlayer.Text = "Player";
            // 
            // playersGrid
            // 
            this.playersGrid.Location = new System.Drawing.Point(4, 19);
            this.playersGrid.Name = "playersGrid";
            this.playersGrid.Size = new System.Drawing.Size(440, 440);
            this.playersGrid.TabIndex = 0;
            // 
            // buttonDeploy
            // 
            this.buttonDeploy.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonDeploy.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDeploy.Location = new System.Drawing.Point(27, 17);
            this.buttonDeploy.Name = "buttonDeploy";
            this.buttonDeploy.Size = new System.Drawing.Size(139, 41);
            this.buttonDeploy.TabIndex = 0;
            this.buttonDeploy.Text = "Deploy Fleets";
            this.buttonDeploy.UseVisualStyleBackColor = true;
            this.buttonDeploy.Click += new System.EventHandler(this.buttonDeploy_Click);
            // 
            // buttonStartGame
            // 
            this.buttonStartGame.Enabled = false;
            this.buttonStartGame.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonStartGame.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonStartGame.Location = new System.Drawing.Point(27, 79);
            this.buttonStartGame.Name = "buttonStartGame";
            this.buttonStartGame.Size = new System.Drawing.Size(139, 41);
            this.buttonStartGame.TabIndex = 1;
            this.buttonStartGame.Text = "Start Game";
            this.buttonStartGame.UseVisualStyleBackColor = true;
            this.buttonStartGame.Click += new System.EventHandler(this.buttonStartGame_Click);
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1134, 511);
            this.Controls.Add(this.buttonStartGame);
            this.Controls.Add(this.buttonDeploy);
            this.Controls.Add(this.groupBoxPlayer);
            this.Controls.Add(this.groupBoxComputer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "GameForm";
            this.Text = "Battleship";
            this.groupBoxComputer.ResumeLayout(false);
            this.groupBoxPlayer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxComputer;
        private System.Windows.Forms.GroupBox groupBoxPlayer;
        private GameGrid gameGrid2;
        private PlayersGrid playersGrid;
        private System.Windows.Forms.Button buttonDeploy;
        private System.Windows.Forms.Button buttonStartGame;
    }
}

