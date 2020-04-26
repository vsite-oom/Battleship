namespace Battleship
{
    partial class Main
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
            this.opponentPanel = new Battleship.Class.BattleshipPanel();
            this.btnNewGame = new System.Windows.Forms.Button();
            this.playerPanel = new Battleship.Class.BattleshipPanel();
            this.lbnOpponent = new System.Windows.Forms.Label();
            this.lbnPlayer = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // opponentPanel
            // 
            this.opponentPanel.Location = new System.Drawing.Point(12, 12);
            this.opponentPanel.Name = "opponentPanel";
            this.opponentPanel.Size = new System.Drawing.Size(551, 551);
            this.opponentPanel.TabIndex = 0;
            // 
            // btnNewGame
            // 
            this.btnNewGame.Location = new System.Drawing.Point(598, 540);
            this.btnNewGame.Name = "btnNewGame";
            this.btnNewGame.Size = new System.Drawing.Size(75, 23);
            this.btnNewGame.TabIndex = 1;
            this.btnNewGame.Text = "New game";
            this.btnNewGame.UseVisualStyleBackColor = true;
            this.btnNewGame.Click += new System.EventHandler(this.btnNewGame_Click);
            // 
            // playerPanel
            // 
            this.playerPanel.Location = new System.Drawing.Point(721, 12);
            this.playerPanel.Name = "playerPanel";
            this.playerPanel.Size = new System.Drawing.Size(551, 551);
            this.playerPanel.TabIndex = 2;
            // 
            // label1
            // 
            this.lbnOpponent.AutoSize = true;
            this.lbnOpponent.Location = new System.Drawing.Point(274, 570);
            this.lbnOpponent.Name = "lbnOpponent";
            this.lbnOpponent.Size = new System.Drawing.Size(35, 13);
            this.lbnOpponent.TabIndex = 3;
            this.lbnOpponent.Text = "Opponent";            
            // 
            // label2
            // 
            this.lbnPlayer.AutoSize = true;
            this.lbnPlayer.Location = new System.Drawing.Point(979, 566);
            this.lbnPlayer.Name = "lbnPlayer";
            this.lbnPlayer.Size = new System.Drawing.Size(35, 13);
            this.lbnPlayer.TabIndex = 4;
            this.lbnPlayer.Text = "Player";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1323, 614);
            this.Controls.Add(this.lbnPlayer);
            this.Controls.Add(this.lbnOpponent);
            this.Controls.Add(this.playerPanel);
            this.Controls.Add(this.btnNewGame);
            this.Controls.Add(this.opponentPanel);
            this.Name = "Main";
            this.Text = "Battleship";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Battleship.Class.BattleshipPanel opponentPanel;
        private System.Windows.Forms.Button btnNewGame;
        private Battleship.Class.BattleshipPanel playerPanel;
        private System.Windows.Forms.Label lbnOpponent;
        private System.Windows.Forms.Label lbnPlayer;
    }
}

