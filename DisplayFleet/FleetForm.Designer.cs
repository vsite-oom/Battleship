namespace DisplayFleet
{
    partial class FleetForm
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
            this.components = new System.ComponentModel.Container();
            this.align = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.startGame = new System.Windows.Forms.Button();
            this.playerName = new System.Windows.Forms.Label();
            this.enemyName = new System.Windows.Forms.Label();
            this.nameLabel = new System.Windows.Forms.Label();
            this.enemyLabel = new System.Windows.Forms.Label();
            this.textNameUser = new System.Windows.Forms.TextBox();
            this.textNameEnemy = new System.Windows.Forms.TextBox();
            this.nameButton = new System.Windows.Forms.Button();
            this.fleetGrid1 = new DisplayFleet.FleetGrid(this.components);
            this.userPoint = new System.Windows.Forms.Label();
            this.enemyPoint = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // align
            // 
            this.align.Location = new System.Drawing.Point(330, 12);
            this.align.Name = "align";
            this.align.Size = new System.Drawing.Size(127, 23);
            this.align.TabIndex = 1;
            this.align.Text = "&Arrange Fleet";
            this.align.UseVisualStyleBackColor = true;
            this.align.Visible = false;
            this.align.Click += new System.EventHandler(this.displayFleet);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(475, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Quit Game";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.quitGame);
            // 
            // startGame
            // 
            this.startGame.Location = new System.Drawing.Point(234, 12);
            this.startGame.Name = "startGame";
            this.startGame.Size = new System.Drawing.Size(75, 23);
            this.startGame.TabIndex = 3;
            this.startGame.Text = "Start Game";
            this.startGame.UseVisualStyleBackColor = true;
            this.startGame.Visible = false;
            this.startGame.Click += new System.EventHandler(this.button2_Click);
            // 
            // playerName
            // 
            this.playerName.Location = new System.Drawing.Point(73, 419);
            this.playerName.Name = "playerName";
            this.playerName.Size = new System.Drawing.Size(170, 22);
            this.playerName.TabIndex = 4;
            // 
            // enemyName
            // 
            this.enemyName.Location = new System.Drawing.Point(483, 419);
            this.enemyName.Name = "enemyName";
            this.enemyName.Size = new System.Drawing.Size(170, 23);
            this.enemyName.TabIndex = 5;
            // 
            // nameLabel
            // 
            this.nameLabel.Location = new System.Drawing.Point(143, 417);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(100, 23);
            this.nameLabel.TabIndex = 6;
            this.nameLabel.Text = "What\'s your name?";
            // 
            // enemyLabel
            // 
            this.enemyLabel.Location = new System.Drawing.Point(601, 417);
            this.enemyLabel.Name = "enemyLabel";
            this.enemyLabel.Size = new System.Drawing.Size(100, 23);
            this.enemyLabel.TabIndex = 7;
            this.enemyLabel.Text = "Opponent name";
            // 
            // textNameUser
            // 
            this.textNameUser.Location = new System.Drawing.Point(12, 414);
            this.textNameUser.Name = "textNameUser";
            this.textNameUser.Size = new System.Drawing.Size(100, 20);
            this.textNameUser.TabIndex = 8;
            // 
            // textNameEnemy
            // 
            this.textNameEnemy.Location = new System.Drawing.Point(750, 414);
            this.textNameEnemy.Name = "textNameEnemy";
            this.textNameEnemy.Size = new System.Drawing.Size(100, 20);
            this.textNameEnemy.TabIndex = 9;
            // 
            // nameButton
            // 
            this.nameButton.Location = new System.Drawing.Point(382, 415);
            this.nameButton.Name = "nameButton";
            this.nameButton.Size = new System.Drawing.Size(75, 23);
            this.nameButton.TabIndex = 10;
            this.nameButton.Text = "Lock on name";
            this.nameButton.UseVisualStyleBackColor = true;
            this.nameButton.Click += new System.EventHandler(this.nameButton_Click);
            // 
            // fleetGrid1
            // 
            this.fleetGrid1.Location = new System.Drawing.Point(0, 0);
            this.fleetGrid1.Name = "fleetGrid1";
            this.fleetGrid1.Size = new System.Drawing.Size(0, 0);
            this.fleetGrid1.TabIndex = 0;
            // 
            // userPoint
            // 
            this.userPoint.AutoSize = true;
            this.userPoint.Location = new System.Drawing.Point(9, 17);
            this.userPoint.Name = "userPoint";
            this.userPoint.Size = new System.Drawing.Size(35, 13);
            this.userPoint.TabIndex = 11;
            this.userPoint.Text = "label1";
            // 
            // enemyPoint
            // 
            this.enemyPoint.AutoSize = true;
            this.enemyPoint.Location = new System.Drawing.Point(815, 22);
            this.enemyPoint.Name = "enemyPoint";
            this.enemyPoint.Size = new System.Drawing.Size(35, 13);
            this.enemyPoint.TabIndex = 12;
            this.enemyPoint.Text = "label1";
            // 
            // FleetForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(862, 450);
            this.Controls.Add(this.enemyPoint);
            this.Controls.Add(this.userPoint);
            this.Controls.Add(this.nameButton);
            this.Controls.Add(this.textNameEnemy);
            this.Controls.Add(this.textNameUser);
            this.Controls.Add(this.enemyLabel);
            this.Controls.Add(this.nameLabel);
            this.Controls.Add(this.enemyName);
            this.Controls.Add(this.playerName);
            this.Controls.Add(this.startGame);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.align);
            this.Name = "FleetForm";
            this.Text = "Flota";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button align;
        private System.Windows.Forms.Button button1;
        private FleetGrid fleetGrid1;
        private System.Windows.Forms.Button startGame;
        private System.Windows.Forms.Label playerName;
        private System.Windows.Forms.Label enemyName;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.Label enemyLabel;
        private System.Windows.Forms.TextBox textNameUser;
        private System.Windows.Forms.TextBox textNameEnemy;
        private System.Windows.Forms.Button nameButton;
        private System.Windows.Forms.Label userPoint;
        private System.Windows.Forms.Label enemyPoint;
    }
}

