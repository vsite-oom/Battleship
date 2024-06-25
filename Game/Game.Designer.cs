namespace Vsite.Oom.Battleship.Game
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label playerHitsLabel;
        private Label opponentHitsLabel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            panel_Title = new Panel();
            button_StartStop = new Button();
            label_Enemy = new Label();
            label_Host = new Label();
            panel_Info = new Panel();
            opponentHitsLabel = new Label();
            panel1 = new Panel();
            panel_ShootEnemy = new Panel();
            playerHitsLabel = new Label();
            panel_Control = new Panel();
            panel_Host = new Panel();
            panel_Split = new Panel();
            panel_Enemy = new Panel();
            panel_Title.SuspendLayout();
            panel_Info.SuspendLayout();
            panel_ShootEnemy.SuspendLayout();
            SuspendLayout();
            // 
            // panel_Title
            // 
            panel_Title.Controls.Add(button_StartStop);
            panel_Title.Controls.Add(label_Enemy);
            panel_Title.Controls.Add(label_Host);
            panel_Title.Dock = DockStyle.Top;
            panel_Title.Font = new Font("Calibri", 9.75F);
            panel_Title.Location = new Point(0, 0);
            panel_Title.Margin = new Padding(4, 5, 4, 5);
            panel_Title.Name = "panel_Title";
            panel_Title.Size = new Size(1109, 64);
            panel_Title.TabIndex = 0;
            // 
            // button_StartStop
            // 
            button_StartStop.Anchor = AnchorStyles.None;
            button_StartStop.BackColor = Color.White;
            button_StartStop.BackgroundImageLayout = ImageLayout.Stretch;
            button_StartStop.Font = new Font("Calibri", 14.25F, FontStyle.Bold);
            button_StartStop.ForeColor = Color.ForestGreen;
            button_StartStop.Location = new Point(453, 6);
            button_StartStop.Margin = new Padding(2, 4, 2, 4);
            button_StartStop.Name = "button_StartStop";
            button_StartStop.Size = new Size(200, 54);
            button_StartStop.TabIndex = 56;
            button_StartStop.TabStop = false;
            button_StartStop.Tag = "0";
            button_StartStop.Text = "Start";
            button_StartStop.UseVisualStyleBackColor = false;
            button_StartStop.Click += button_StartStop_Click;
            // 
            // label_Enemy
            // 
            label_Enemy.AutoSize = true;
            label_Enemy.Dock = DockStyle.Fill;
            label_Enemy.Font = new Font("Calibri", 14.25F, FontStyle.Bold);
            label_Enemy.Location = new Point(554, 0);
            label_Enemy.Margin = new Padding(4, 0, 4, 0);
            label_Enemy.MinimumSize = new Size(554, 60);
            label_Enemy.Name = "label_Enemy";
            label_Enemy.Size = new Size(554, 60);
            label_Enemy.TabIndex = 1;
            label_Enemy.Text = "Napad";
            label_Enemy.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label_Host
            // 
            label_Host.AutoSize = true;
            label_Host.Dock = DockStyle.Left;
            label_Host.Font = new Font("Calibri", 14.25F, FontStyle.Bold);
            label_Host.Location = new Point(0, 0);
            label_Host.Margin = new Padding(4, 0, 4, 0);
            label_Host.MinimumSize = new Size(554, 60);
            label_Host.Name = "label_Host";
            label_Host.Size = new Size(554, 60);
            label_Host.TabIndex = 0;
            label_Host.Text = "Baza";
            label_Host.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel_Info
            // 
            panel_Info.BackColor = SystemColors.GradientActiveCaption;
            panel_Info.Controls.Add(opponentHitsLabel);
            panel_Info.Controls.Add(panel1);
            panel_Info.Controls.Add(panel_ShootEnemy);
            panel_Info.Dock = DockStyle.Top;
            panel_Info.Font = new Font("Calibri", 9.75F);
            panel_Info.Location = new Point(0, 64);
            panel_Info.Margin = new Padding(4, 5, 4, 5);
            panel_Info.Name = "panel_Info";
            panel_Info.Size = new Size(1109, 44);
            panel_Info.TabIndex = 1;
            // 
            // opponentHitsLabel
            // 
            opponentHitsLabel.AutoSize = true;
            opponentHitsLabel.Font = new Font("Calibri", 12F, FontStyle.Bold);
            opponentHitsLabel.Location = new Point(698, 5);
            opponentHitsLabel.Margin = new Padding(4, 0, 4, 0);
            opponentHitsLabel.Name = "opponentHitsLabel";
            opponentHitsLabel.Size = new Size(0, 29);
            opponentHitsLabel.TabIndex = 3;
            opponentHitsLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ControlLightLight;
            panel1.Dock = DockStyle.Left;
            panel1.Font = new Font("Calibri", 9.75F);
            panel1.Location = new Point(548, 0);
            panel1.Margin = new Padding(4, 5, 4, 5);
            panel1.Name = "panel1";
            panel1.Size = new Size(14, 44);
            panel1.TabIndex = 5;
            // 
            // panel_ShootEnemy
            // 
            panel_ShootEnemy.BackColor = SystemColors.GradientActiveCaption;
            panel_ShootEnemy.Controls.Add(playerHitsLabel);
            panel_ShootEnemy.Dock = DockStyle.Left;
            panel_ShootEnemy.Location = new Point(0, 0);
            panel_ShootEnemy.Margin = new Padding(4, 5, 4, 5);
            panel_ShootEnemy.Name = "panel_ShootEnemy";
            panel_ShootEnemy.Size = new Size(548, 44);
            panel_ShootEnemy.TabIndex = 0;
            // 
            // playerHitsLabel
            // 
            playerHitsLabel.AutoSize = true;
            playerHitsLabel.Font = new Font("Calibri", 12F, FontStyle.Bold);
            playerHitsLabel.Location = new Point(153, 5);
            playerHitsLabel.Margin = new Padding(4, 0, 4, 0);
            playerHitsLabel.Name = "playerHitsLabel";
            playerHitsLabel.Size = new Size(0, 29);
            playerHitsLabel.TabIndex = 2;
            playerHitsLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel_Control
            // 
            panel_Control.Dock = DockStyle.Bottom;
            panel_Control.Font = new Font("Calibri", 9.75F);
            panel_Control.Location = new Point(0, 710);
            panel_Control.Margin = new Padding(4, 5, 4, 5);
            panel_Control.Name = "panel_Control";
            panel_Control.Size = new Size(1109, 75);
            panel_Control.TabIndex = 2;
            // 
            // panel_Host
            // 
            panel_Host.BackColor = Color.Blue;
            panel_Host.Dock = DockStyle.Left;
            panel_Host.Font = new Font("Calibri", 9.75F);
            panel_Host.Location = new Point(0, 108);
            panel_Host.Margin = new Padding(4, 5, 4, 5);
            panel_Host.Name = "panel_Host";
            panel_Host.Size = new Size(548, 602);
            panel_Host.TabIndex = 3;
            // 
            // panel_Split
            // 
            panel_Split.BackColor = SystemColors.ControlLightLight;
            panel_Split.Dock = DockStyle.Left;
            panel_Split.Font = new Font("Calibri", 9.75F);
            panel_Split.Location = new Point(548, 108);
            panel_Split.Margin = new Padding(4, 5, 4, 5);
            panel_Split.Name = "panel_Split";
            panel_Split.Size = new Size(14, 602);
            panel_Split.TabIndex = 4;
            // 
            // panel_Enemy
            // 
            panel_Enemy.BackColor = Color.Crimson;
            panel_Enemy.Dock = DockStyle.Fill;
            panel_Enemy.Enabled = false;
            panel_Enemy.Font = new Font("Calibri", 9.75F);
            panel_Enemy.Location = new Point(562, 108);
            panel_Enemy.Margin = new Padding(4, 5, 4, 5);
            panel_Enemy.Name = "panel_Enemy";
            panel_Enemy.Size = new Size(547, 602);
            panel_Enemy.TabIndex = 5;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1109, 785);
            Controls.Add(panel_Enemy);
            Controls.Add(panel_Split);
            Controls.Add(panel_Host);
            Controls.Add(panel_Control);
            Controls.Add(panel_Info);
            Controls.Add(panel_Title);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(4, 5, 4, 5);
            Name = "MainForm";
            Text = "Battleship Game";
            panel_Title.ResumeLayout(false);
            panel_Title.PerformLayout();
            panel_Info.ResumeLayout(false);
            panel_Info.PerformLayout();
            panel_ShootEnemy.ResumeLayout(false);
            panel_ShootEnemy.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel_Title;
        private Label label_Enemy;
        private Label label_Host;
        private Panel panel_Info;
        private Panel panel1;
        private Panel panel_ShootEnemy;
        private Panel panel_Control;
        private Button button_StartStop;
        private Panel panel_Host;
        private Panel panel_Split;
        private Panel panel_Enemy;
    }
}
