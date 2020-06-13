namespace Boards
{
    partial class FormMain
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
            this.groupBoxFleet = new System.Windows.Forms.GroupBox();
            this.buttonReset = new System.Windows.Forms.Button();
            this.groupBoxEvidence = new System.Windows.Forms.GroupBox();
            this.buttonStartGame = new System.Windows.Forms.Button();
            this.actionButton = new System.Windows.Forms.Button();
            this.ShootStatusPcLabel = new System.Windows.Forms.Label();
            this.ShootStatusPersonLabel = new System.Windows.Forms.Label();
            this.evidenceGrid = new Boards.EvidenceGrid();
            this.fleetDisplay = new Boards.FleetGrid();
            this.groupBoxFleet.SuspendLayout();
            this.groupBoxEvidence.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxFleet
            // 
            this.groupBoxFleet.Controls.Add(this.fleetDisplay);
            this.groupBoxFleet.Location = new System.Drawing.Point(12, 12);
            this.groupBoxFleet.Name = "groupBoxFleet";
            this.groupBoxFleet.Size = new System.Drawing.Size(479, 492);
            this.groupBoxFleet.TabIndex = 0;
            this.groupBoxFleet.TabStop = false;
            this.groupBoxFleet.Text = "My Fleet";
            // 
            // buttonReset
            // 
            this.buttonReset.Font = new System.Drawing.Font("Britannic Bold", 14.25F);
            this.buttonReset.ForeColor = System.Drawing.Color.MidnightBlue;
            this.buttonReset.Location = new System.Drawing.Point(849, 543);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(144, 32);
            this.buttonReset.TabIndex = 1;
            this.buttonReset.Text = "Arrange fleet";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.buttonResetClick);
            // 
            // groupBoxEvidence
            // 
            this.groupBoxEvidence.Controls.Add(this.evidenceGrid);
            this.groupBoxEvidence.Location = new System.Drawing.Point(514, 12);
            this.groupBoxEvidence.Name = "groupBoxEvidence";
            this.groupBoxEvidence.Size = new System.Drawing.Size(479, 492);
            this.groupBoxEvidence.TabIndex = 2;
            this.groupBoxEvidence.TabStop = false;
            this.groupBoxEvidence.Text = "Evidence Grid";
            // 
            // buttonStartGame
            // 
            this.buttonStartGame.Enabled = false;
            this.buttonStartGame.Font = new System.Drawing.Font("Britannic Bold", 14.25F);
            this.buttonStartGame.ForeColor = System.Drawing.Color.MidnightBlue;
            this.buttonStartGame.Location = new System.Drawing.Point(849, 505);
            this.buttonStartGame.Name = "buttonStartGame";
            this.buttonStartGame.Size = new System.Drawing.Size(144, 32);
            this.buttonStartGame.TabIndex = 3;
            this.buttonStartGame.Text = "Start Game";
            this.buttonStartGame.UseVisualStyleBackColor = true;
            this.buttonStartGame.Click += new System.EventHandler(this.buttonStartGame_Click);
            // 
            // actionButton
            // 
            this.actionButton.Enabled = false;
            this.actionButton.Font = new System.Drawing.Font("Britannic Bold", 14.25F);
            this.actionButton.ForeColor = System.Drawing.Color.MidnightBlue;
            this.actionButton.Location = new System.Drawing.Point(422, 513);
            this.actionButton.Name = "actionButton";
            this.actionButton.Size = new System.Drawing.Size(157, 52);
            this.actionButton.TabIndex = 4;
            this.actionButton.Text = "Select field to shoot!";
            this.actionButton.UseVisualStyleBackColor = true;
            this.actionButton.Visible = false;
            this.actionButton.Click += new System.EventHandler(this.actionButton_Click);
            // 
            // ShootStatusPcLabel
            // 
            this.ShootStatusPcLabel.AutoSize = true;
            this.ShootStatusPcLabel.Enabled = false;
            this.ShootStatusPcLabel.Font = new System.Drawing.Font("Britannic Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ShootStatusPcLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ShootStatusPcLabel.Location = new System.Drawing.Point(177, 515);
            this.ShootStatusPcLabel.Name = "ShootStatusPcLabel";
            this.ShootStatusPcLabel.Size = new System.Drawing.Size(30, 21);
            this.ShootStatusPcLabel.TabIndex = 5;
            this.ShootStatusPcLabel.Text = "lbl";
            this.ShootStatusPcLabel.Visible = false;
            // 
            // ShootStatusPersonLabel
            // 
            this.ShootStatusPersonLabel.AutoSize = true;
            this.ShootStatusPersonLabel.Enabled = false;
            this.ShootStatusPersonLabel.Font = new System.Drawing.Font("Britannic Bold", 14.25F);
            this.ShootStatusPersonLabel.Location = new System.Drawing.Point(659, 515);
            this.ShootStatusPersonLabel.Name = "ShootStatusPersonLabel";
            this.ShootStatusPersonLabel.Size = new System.Drawing.Size(30, 21);
            this.ShootStatusPersonLabel.TabIndex = 6;
            this.ShootStatusPersonLabel.Text = "lbl";
            this.ShootStatusPersonLabel.Visible = false;
            // 
            // evidenceGrid
            // 
            this.evidenceGrid.Enabled = false;
            this.evidenceGrid.Location = new System.Drawing.Point(6, 19);
            this.evidenceGrid.Name = "evidenceGrid";
            this.evidenceGrid.Size = new System.Drawing.Size(458, 458);
            this.evidenceGrid.TabIndex = 0;
            // 
            // fleetDisplay
            // 
            this.fleetDisplay.Location = new System.Drawing.Point(6, 19);
            this.fleetDisplay.Name = "fleetDisplay";
            this.fleetDisplay.Size = new System.Drawing.Size(458, 458);
            this.fleetDisplay.TabIndex = 0;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1011, 577);
            this.Controls.Add(this.ShootStatusPersonLabel);
            this.Controls.Add(this.ShootStatusPcLabel);
            this.Controls.Add(this.actionButton);
            this.Controls.Add(this.buttonStartGame);
            this.Controls.Add(this.groupBoxEvidence);
            this.Controls.Add(this.buttonReset);
            this.Controls.Add(this.groupBoxFleet);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FormMain";
            this.Text = "Battleship";
            this.groupBoxFleet.ResumeLayout(false);
            this.groupBoxEvidence.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxFleet;
        private FleetGrid fleetDisplay;
        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.GroupBox groupBoxEvidence;
        private EvidenceGrid evidenceGrid;
        private System.Windows.Forms.Button buttonStartGame;
        private System.Windows.Forms.Button actionButton;
        private System.Windows.Forms.Label ShootStatusPcLabel;
        private System.Windows.Forms.Label ShootStatusPersonLabel;
    }
}