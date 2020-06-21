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
            this.computersGrid = new FinalGUI.ComputersGrid();
            this.groupBoxPlayer = new System.Windows.Forms.GroupBox();
            this.playersGrid = new FinalGUI.PlayersGrid();
            this.buttonDeploy = new System.Windows.Forms.Button();
            this.buttonStartGame = new System.Windows.Forms.Button();
            this.labelStatus = new System.Windows.Forms.Label();
            this.groupBoxComputer.SuspendLayout();
            this.groupBoxPlayer.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxComputer
            // 
            this.groupBoxComputer.Controls.Add(this.computersGrid);
            this.groupBoxComputer.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxComputer.Location = new System.Drawing.Point(880, 17);
            this.groupBoxComputer.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.groupBoxComputer.Name = "groupBoxComputer";
            this.groupBoxComputer.Padding = new System.Windows.Forms.Padding(0);
            this.groupBoxComputer.Size = new System.Drawing.Size(613, 585);
            this.groupBoxComputer.TabIndex = 0;
            this.groupBoxComputer.TabStop = false;
            this.groupBoxComputer.Text = "Computer";
            // 
            // computersGrid
            // 
            this.computersGrid.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.computersGrid.ForeColor = System.Drawing.SystemColors.ControlText;
            this.computersGrid.Location = new System.Drawing.Point(5, 26);
            this.computersGrid.Margin = new System.Windows.Forms.Padding(5);
            this.computersGrid.Name = "computersGrid";
            this.computersGrid.Size = new System.Drawing.Size(587, 542);
            this.computersGrid.TabIndex = 0;
            // 
            // groupBoxPlayer
            // 
            this.groupBoxPlayer.Controls.Add(this.playersGrid);
            this.groupBoxPlayer.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxPlayer.Location = new System.Drawing.Point(253, 17);
            this.groupBoxPlayer.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.groupBoxPlayer.Name = "groupBoxPlayer";
            this.groupBoxPlayer.Padding = new System.Windows.Forms.Padding(0);
            this.groupBoxPlayer.Size = new System.Drawing.Size(613, 585);
            this.groupBoxPlayer.TabIndex = 0;
            this.groupBoxPlayer.TabStop = false;
            this.groupBoxPlayer.Text = "You";
            // 
            // playersGrid
            // 
            this.playersGrid.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.playersGrid.ForeColor = System.Drawing.SystemColors.ControlText;
            this.playersGrid.Location = new System.Drawing.Point(5, 23);
            this.playersGrid.Margin = new System.Windows.Forms.Padding(5);
            this.playersGrid.Name = "playersGrid";
            this.playersGrid.Size = new System.Drawing.Size(587, 542);
            this.playersGrid.TabIndex = 0;
            // 
            // buttonDeploy
            // 
            this.buttonDeploy.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonDeploy.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDeploy.Location = new System.Drawing.Point(36, 21);
            this.buttonDeploy.Margin = new System.Windows.Forms.Padding(4);
            this.buttonDeploy.Name = "buttonDeploy";
            this.buttonDeploy.Size = new System.Drawing.Size(185, 50);
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
            this.buttonStartGame.Location = new System.Drawing.Point(36, 97);
            this.buttonStartGame.Margin = new System.Windows.Forms.Padding(4);
            this.buttonStartGame.Name = "buttonStartGame";
            this.buttonStartGame.Size = new System.Drawing.Size(185, 50);
            this.buttonStartGame.TabIndex = 1;
            this.buttonStartGame.Text = "Start Game";
            this.buttonStartGame.UseVisualStyleBackColor = true;
            this.buttonStartGame.Click += new System.EventHandler(this.buttonStartGame_Click);
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelStatus.ForeColor = System.Drawing.Color.Red;
            this.labelStatus.Location = new System.Drawing.Point(36, 183);
            this.labelStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(0, 24);
            this.labelStatus.TabIndex = 2;
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1512, 629);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.buttonStartGame);
            this.Controls.Add(this.buttonDeploy);
            this.Controls.Add(this.groupBoxPlayer);
            this.Controls.Add(this.groupBoxComputer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "GameForm";
            this.Text = "Battleship";
            this.groupBoxComputer.ResumeLayout(false);
            this.groupBoxPlayer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxComputer;
        private System.Windows.Forms.GroupBox groupBoxPlayer;
        private PlayersGrid playersGrid;
        private System.Windows.Forms.Button buttonDeploy;
        private System.Windows.Forms.Button buttonStartGame;
        private System.Windows.Forms.Label labelStatus;
        private ComputersGrid computersGrid;
    }
}

