namespace Vsite.Oom.Battleship.Game
{
    partial class BattleGame
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BattleGame));
            panel_Player = new Panel();
            panel_Opponent = new Panel();
            panel1 = new Panel();
            label1 = new Label();
            panel_Info = new Panel();
            opponentHitsLabel = new Label();
            playerHitsLabel = new Label();
            button_StartStop = new Button();
            label2 = new Label();
            label3 = new Label();
            timer1 = new System.Windows.Forms.Timer(components);
            panel1.SuspendLayout();
            panel_Info.SuspendLayout();
            SuspendLayout();
            // 
            // panel_Player
            // 
            panel_Player.BackColor = SystemColors.AppWorkspace;
            panel_Player.BackgroundImage = (Image)resources.GetObject("panel_Player.BackgroundImage");
            panel_Player.BackgroundImageLayout = ImageLayout.Stretch;
            panel_Player.Location = new Point(0, 62);
            panel_Player.Name = "panel_Player";
            panel_Player.Size = new Size(500, 400);
            panel_Player.TabIndex = 0;
            // 
            // panel_Opponent
            // 
            panel_Opponent.BackColor = SystemColors.AppWorkspace;
            panel_Opponent.BackgroundImage = (Image)resources.GetObject("panel_Opponent.BackgroundImage");
            panel_Opponent.BackgroundImageLayout = ImageLayout.Stretch;
            panel_Opponent.Location = new Point(824, 62);
            panel_Opponent.Name = "panel_Opponent";
            panel_Opponent.Size = new Size(502, 400);
            panel_Opponent.TabIndex = 1;
            // 
            // panel1
            // 
            panel1.BackgroundImage = (Image)resources.GetObject("panel1.BackgroundImage");
            panel1.BackgroundImageLayout = ImageLayout.Stretch;
            panel1.Controls.Add(label1);
            panel1.Location = new Point(496, 62);
            panel1.Name = "panel1";
            panel1.Size = new Size(333, 400);
            panel1.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = SystemColors.ActiveCaption;
            label1.Font = new Font("Segoe UI", 15F, FontStyle.Bold | FontStyle.Italic);
            label1.Location = new Point(139, 171);
            label1.Name = "label1";
            label1.Size = new Size(29, 35);
            label1.TabIndex = 0;
            label1.Text = "0";
            // 
            // panel_Info
            // 
            panel_Info.BackColor = SystemColors.ActiveCaption;
            panel_Info.Controls.Add(opponentHitsLabel);
            panel_Info.Controls.Add(playerHitsLabel);
            panel_Info.Location = new Point(0, 468);
            panel_Info.Name = "panel_Info";
            panel_Info.Size = new Size(1312, 78);
            panel_Info.TabIndex = 3;
            // 
            // opponentHitsLabel
            // 
            opponentHitsLabel.AutoSize = true;
            opponentHitsLabel.Location = new Point(1070, 25);
            opponentHitsLabel.Name = "opponentHitsLabel";
            opponentHitsLabel.Size = new Size(82, 20);
            opponentHitsLabel.TabIndex = 1;
            opponentHitsLabel.Text = "AI BODOVI";
            // 
            // playerHitsLabel
            // 
            playerHitsLabel.AutoSize = true;
            playerHitsLabel.Location = new Point(198, 25);
            playerHitsLabel.Name = "playerHitsLabel";
            playerHitsLabel.Size = new Size(117, 20);
            playerHitsLabel.TabIndex = 0;
            playerHitsLabel.Text = "PLAYER BODOVI";
            // 
            // button_StartStop
            // 
            button_StartStop.Font = new Font("Segoe UI", 15F);
            button_StartStop.Location = new Point(564, 9);
            button_StartStop.Name = "button_StartStop";
            button_StartStop.Size = new Size(183, 44);
            button_StartStop.TabIndex = 4;
            button_StartStop.Text = "START";
            button_StartStop.UseVisualStyleBackColor = true;
            button_StartStop.Click += button_StartStop_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            label2.ForeColor = SystemColors.HotTrack;
            label2.Location = new Point(198, 14);
            label2.Name = "label2";
            label2.Size = new Size(104, 35);
            label2.TabIndex = 5;
            label2.Text = "PLAYER";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            label3.ForeColor = Color.DarkRed;
            label3.Location = new Point(1070, 14);
            label3.Name = "label3";
            label3.Size = new Size(100, 35);
            label3.TabIndex = 6;
            label3.Text = "ENEMY";
            // 
            // timer1
            // 
            timer1.Interval = 1000;
            timer1.Tick += timer1_Tick;
            // 
            // BattleGame
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1312, 552);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(button_StartStop);
            Controls.Add(panel_Info);
            Controls.Add(panel1);
            Controls.Add(panel_Opponent);
            Controls.Add(panel_Player);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "BattleGame";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "BattleGame";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel_Info.ResumeLayout(false);
            panel_Info.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel_Player;
        private Panel panel_Opponent;
        private Panel panel1;
        private Label label1;
        private Panel panel_Info;
        private Label opponentHitsLabel;
        private Label playerHitsLabel;
        private Button button_StartStop;
        private Label label2;
        private Label label3;
        private System.Windows.Forms.Timer timer1;
    }
}