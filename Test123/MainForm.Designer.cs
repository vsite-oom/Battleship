
namespace BattleshipGUI
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.PlaceFleetButton = new System.Windows.Forms.Button();
            this.myFleetColorDialog = new System.Windows.Forms.ColorDialog();
            this.enemyShipsAliveLabel = new System.Windows.Forms.Label();
            this.enemyShipsAliveText = new System.Windows.Forms.Label();
            this.enemySquaresLeftLabel = new System.Windows.Forms.Label();
            this.enemySquaresLeftText = new System.Windows.Forms.Label();
            this.myShipsAliveText = new System.Windows.Forms.Label();
            this.mySquaresLeftText = new System.Windows.Forms.Label();
            this.myShipsAliveLabel = new System.Windows.Forms.Label();
            this.mySquaresLeftLabel = new System.Windows.Forms.Label();
            this.stopWatchLabel = new System.Windows.Forms.Label();
            this.gameTimer = new System.Windows.Forms.Timer(this.components);
            this.timeTextLabel = new System.Windows.Forms.Label();
            this.myFleetGroupBox = new System.Windows.Forms.GroupBox();
            this.enemyFleetGroupBox = new System.Windows.Forms.GroupBox();
            this.SuspendLayout();
            // 
            // PlaceFleetButton
            // 
            this.PlaceFleetButton.Location = new System.Drawing.Point(528, 459);
            this.PlaceFleetButton.Name = "PlaceFleetButton";
            this.PlaceFleetButton.Size = new System.Drawing.Size(69, 23);
            this.PlaceFleetButton.TabIndex = 0;
            this.PlaceFleetButton.Text = "Place fleet";
            this.PlaceFleetButton.UseVisualStyleBackColor = true;
            this.PlaceFleetButton.Click += new System.EventHandler(this.PlaceFleetButton_Click);
            // 
            // enemyShipsAliveLabel
            // 
            this.enemyShipsAliveLabel.AutoSize = true;
            this.enemyShipsAliveLabel.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.enemyShipsAliveLabel.Location = new System.Drawing.Point(820, -2);
            this.enemyShipsAliveLabel.Name = "enemyShipsAliveLabel";
            this.enemyShipsAliveLabel.Size = new System.Drawing.Size(0, 15);
            this.enemyShipsAliveLabel.TabIndex = 1;
            // 
            // enemyShipsAliveText
            // 
            this.enemyShipsAliveText.AutoSize = true;
            this.enemyShipsAliveText.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.enemyShipsAliveText.Location = new System.Drawing.Point(755, -3);
            this.enemyShipsAliveText.Name = "enemyShipsAliveText";
            this.enemyShipsAliveText.Size = new System.Drawing.Size(68, 15);
            this.enemyShipsAliveText.TabIndex = 2;
            this.enemyShipsAliveText.Text = "Ships alive:";
            // 
            // enemySquaresLeftLabel
            // 
            this.enemySquaresLeftLabel.AutoSize = true;
            this.enemySquaresLeftLabel.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.enemySquaresLeftLabel.Location = new System.Drawing.Point(1025, -3);
            this.enemySquaresLeftLabel.Name = "enemySquaresLeftLabel";
            this.enemySquaresLeftLabel.Size = new System.Drawing.Size(0, 15);
            this.enemySquaresLeftLabel.TabIndex = 3;
            // 
            // enemySquaresLeftText
            // 
            this.enemySquaresLeftText.AutoSize = true;
            this.enemySquaresLeftText.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.enemySquaresLeftText.Location = new System.Drawing.Point(955, -3);
            this.enemySquaresLeftText.Name = "enemySquaresLeftText";
            this.enemySquaresLeftText.Size = new System.Drawing.Size(73, 15);
            this.enemySquaresLeftText.TabIndex = 4;
            this.enemySquaresLeftText.Text = "Squares left:";
            // 
            // myShipsAliveText
            // 
            this.myShipsAliveText.AutoSize = true;
            this.myShipsAliveText.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.myShipsAliveText.Location = new System.Drawing.Point(106, -3);
            this.myShipsAliveText.Name = "myShipsAliveText";
            this.myShipsAliveText.Size = new System.Drawing.Size(68, 15);
            this.myShipsAliveText.TabIndex = 5;
            this.myShipsAliveText.Text = "Ships alive:";
            // 
            // mySquaresLeftText
            // 
            this.mySquaresLeftText.AutoSize = true;
            this.mySquaresLeftText.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mySquaresLeftText.Location = new System.Drawing.Point(305, -3);
            this.mySquaresLeftText.Name = "mySquaresLeftText";
            this.mySquaresLeftText.Size = new System.Drawing.Size(73, 15);
            this.mySquaresLeftText.TabIndex = 6;
            this.mySquaresLeftText.Text = "Squares left:";
            // 
            // myShipsAliveLabel
            // 
            this.myShipsAliveLabel.AutoSize = true;
            this.myShipsAliveLabel.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.myShipsAliveLabel.Location = new System.Drawing.Point(170, -3);
            this.myShipsAliveLabel.Name = "myShipsAliveLabel";
            this.myShipsAliveLabel.Size = new System.Drawing.Size(0, 15);
            this.myShipsAliveLabel.TabIndex = 7;
            // 
            // mySquaresLeftLabel
            // 
            this.mySquaresLeftLabel.AutoSize = true;
            this.mySquaresLeftLabel.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mySquaresLeftLabel.Location = new System.Drawing.Point(374, -3);
            this.mySquaresLeftLabel.Name = "mySquaresLeftLabel";
            this.mySquaresLeftLabel.Size = new System.Drawing.Size(0, 15);
            this.mySquaresLeftLabel.TabIndex = 8;
            // 
            // stopWatchLabel
            // 
            this.stopWatchLabel.AutoSize = true;
            this.stopWatchLabel.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stopWatchLabel.Location = new System.Drawing.Point(539, 17);
            this.stopWatchLabel.Name = "stopWatchLabel";
            this.stopWatchLabel.Size = new System.Drawing.Size(49, 15);
            this.stopWatchLabel.TabIndex = 9;
            this.stopWatchLabel.Text = "00:00:00";
            // 
            // gameTimer
            // 
            this.gameTimer.Enabled = true;
            this.gameTimer.Tick += new System.EventHandler(this.GameTimer_Tick);
            // 
            // timeTextLabel
            // 
            this.timeTextLabel.AutoSize = true;
            this.timeTextLabel.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timeTextLabel.Location = new System.Drawing.Point(545, -2);
            this.timeTextLabel.Name = "timeTextLabel";
            this.timeTextLabel.Size = new System.Drawing.Size(33, 15);
            this.timeTextLabel.TabIndex = 10;
            this.timeTextLabel.Text = "Time";
            // 
            // myFleetGroupBox
            // 
            this.myFleetGroupBox.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.myFleetGroupBox.Location = new System.Drawing.Point(12, 17);
            this.myFleetGroupBox.Name = "myFleetGroupBox";
            this.myFleetGroupBox.Size = new System.Drawing.Size(456, 457);
            this.myFleetGroupBox.TabIndex = 11;
            this.myFleetGroupBox.TabStop = false;
            this.myFleetGroupBox.Text = "My Fleet";
            // 
            // enemyFleetGroupBox
            // 
            this.enemyFleetGroupBox.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.enemyFleetGroupBox.Location = new System.Drawing.Point(661, 17);
            this.enemyFleetGroupBox.Name = "enemyFleetGroupBox";
            this.enemyFleetGroupBox.Size = new System.Drawing.Size(457, 457);
            this.enemyFleetGroupBox.TabIndex = 12;
            this.enemyFleetGroupBox.TabStop = false;
            this.enemyFleetGroupBox.Text = "Enemy Fleet";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1135, 506);
            this.Controls.Add(this.enemyFleetGroupBox);
            this.Controls.Add(this.myFleetGroupBox);
            this.Controls.Add(this.timeTextLabel);
            this.Controls.Add(this.stopWatchLabel);
            this.Controls.Add(this.mySquaresLeftLabel);
            this.Controls.Add(this.myShipsAliveLabel);
            this.Controls.Add(this.mySquaresLeftText);
            this.Controls.Add(this.myShipsAliveText);
            this.Controls.Add(this.enemySquaresLeftText);
            this.Controls.Add(this.enemySquaresLeftLabel);
            this.Controls.Add(this.enemyShipsAliveText);
            this.Controls.Add(this.enemyShipsAliveLabel);
            this.Controls.Add(this.PlaceFleetButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Battleship";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button PlaceFleetButton;
        private System.Windows.Forms.ColorDialog myFleetColorDialog;
        private System.Windows.Forms.Label enemyShipsAliveLabel;
        private System.Windows.Forms.Label enemyShipsAliveText;
        private System.Windows.Forms.Label enemySquaresLeftLabel;
        private System.Windows.Forms.Label enemySquaresLeftText;
        private System.Windows.Forms.Label myShipsAliveText;
        private System.Windows.Forms.Label mySquaresLeftText;
        private System.Windows.Forms.Label myShipsAliveLabel;
        private System.Windows.Forms.Label mySquaresLeftLabel;
        private System.Windows.Forms.Label stopWatchLabel;
        private System.Windows.Forms.Timer gameTimer;
        private System.Windows.Forms.Label timeTextLabel;
        private System.Windows.Forms.GroupBox myFleetGroupBox;
        private System.Windows.Forms.GroupBox enemyFleetGroupBox;
    }
}

