namespace Vsite.Oom.Battleship.Game
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

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
            label_Enemy = new Label();
            label_Host = new Label();
            panel_Info = new Panel();
            textBox_EnemyShoot = new TextBox();
            panel1 = new Panel();
            panel_ShootEnemy = new Panel();
            textBox_HostShoot = new TextBox();
            panel_Control = new Panel();
            button_StartStop = new Button();
            panel_Host = new Panel();
            panel_Split = new Panel();
            panel_Enemy = new Panel();
            panel_Title.SuspendLayout();
            panel_Info.SuspendLayout();
            panel_ShootEnemy.SuspendLayout();
            panel_Control.SuspendLayout();
            SuspendLayout();
            // 
            // panel_Title
            // 
            panel_Title.Controls.Add(label_Enemy);
            panel_Title.Controls.Add(label_Host);
            panel_Title.Dock = DockStyle.Top;
            panel_Title.Font = new Font("Calibri", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            panel_Title.Location = new Point(0, 0);
            panel_Title.Name = "panel_Title";
            panel_Title.Size = new Size(776, 38);
            panel_Title.TabIndex = 0;
            // 
            // label_Enemy
            // 
            label_Enemy.AutoSize = true;
            label_Enemy.Dock = DockStyle.Fill;
            label_Enemy.Font = new Font("Calibri", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            label_Enemy.Location = new Point(388, 0);
            label_Enemy.MinimumSize = new Size(388, 36);
            label_Enemy.Name = "label_Enemy";
            label_Enemy.Size = new Size(388, 36);
            label_Enemy.TabIndex = 1;
            label_Enemy.Text = "E N E M Y";
            label_Enemy.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label_Host
            // 
            label_Host.AutoSize = true;
            label_Host.Dock = DockStyle.Left;
            label_Host.Font = new Font("Calibri", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            label_Host.Location = new Point(0, 0);
            label_Host.MinimumSize = new Size(388, 36);
            label_Host.Name = "label_Host";
            label_Host.Size = new Size(388, 36);
            label_Host.TabIndex = 0;
            label_Host.Text = "H O S T";
            label_Host.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel_Info
            // 
            panel_Info.BackColor = Color.LightSkyBlue;
            panel_Info.Controls.Add(textBox_EnemyShoot);
            panel_Info.Controls.Add(panel1);
            panel_Info.Controls.Add(panel_ShootEnemy);
            panel_Info.Dock = DockStyle.Top;
            panel_Info.Font = new Font("Calibri", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            panel_Info.Location = new Point(0, 38);
            panel_Info.Name = "panel_Info";
            panel_Info.Size = new Size(776, 26);
            panel_Info.TabIndex = 1;
            // 
            // textBox_EnemyShoot
            // 
            textBox_EnemyShoot.BackColor = Color.Red;
            textBox_EnemyShoot.Dock = DockStyle.Left;
            textBox_EnemyShoot.Enabled = false;
            textBox_EnemyShoot.Font = new Font("Calibri", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            textBox_EnemyShoot.Location = new Point(393, 0);
            textBox_EnemyShoot.Margin = new Padding(0);
            textBox_EnemyShoot.Name = "textBox_EnemyShoot";
            textBox_EnemyShoot.ReadOnly = true;
            textBox_EnemyShoot.Size = new Size(71, 26);
            textBox_EnemyShoot.TabIndex = 6;
            textBox_EnemyShoot.TabStop = false;
            textBox_EnemyShoot.Text = "S H O O T";
            textBox_EnemyShoot.TextAlign = HorizontalAlignment.Center;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.InactiveCaptionText;
            panel1.Dock = DockStyle.Left;
            panel1.Font = new Font("Calibri", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            panel1.Location = new Point(383, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(10, 26);
            panel1.TabIndex = 5;
            // 
            // panel_ShootEnemy
            // 
            panel_ShootEnemy.BackColor = SystemColors.Info;
            panel_ShootEnemy.Controls.Add(textBox_HostShoot);
            panel_ShootEnemy.Dock = DockStyle.Left;
            panel_ShootEnemy.Location = new Point(0, 0);
            panel_ShootEnemy.Name = "panel_ShootEnemy";
            panel_ShootEnemy.Size = new Size(383, 26);
            panel_ShootEnemy.TabIndex = 0;
            // 
            // textBox_HostShoot
            // 
            textBox_HostShoot.BackColor = Color.Red;
            textBox_HostShoot.Dock = DockStyle.Left;
            textBox_HostShoot.Enabled = false;
            textBox_HostShoot.Font = new Font("Calibri", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            textBox_HostShoot.Location = new Point(0, 0);
            textBox_HostShoot.Margin = new Padding(0);
            textBox_HostShoot.Name = "textBox_HostShoot";
            textBox_HostShoot.ReadOnly = true;
            textBox_HostShoot.Size = new Size(71, 26);
            textBox_HostShoot.TabIndex = 0;
            textBox_HostShoot.TabStop = false;
            textBox_HostShoot.Text = "S H O O T";
            textBox_HostShoot.TextAlign = HorizontalAlignment.Center;
            // 
            // panel_Control
            // 
            panel_Control.Controls.Add(button_StartStop);
            panel_Control.Dock = DockStyle.Bottom;
            panel_Control.Font = new Font("Calibri", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            panel_Control.Location = new Point(0, 426);
            panel_Control.Name = "panel_Control";
            panel_Control.Size = new Size(776, 45);
            panel_Control.TabIndex = 2;
            // 
            // button_StartStop
            // 
            button_StartStop.Anchor = AnchorStyles.None;
            button_StartStop.BackColor = Color.White;
            button_StartStop.BackgroundImageLayout = ImageLayout.Stretch;
            button_StartStop.Font = new Font("Calibri", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            button_StartStop.ForeColor = Color.ForestGreen;
            button_StartStop.Location = new Point(328, 6);
            button_StartStop.Margin = new Padding(2);
            button_StartStop.Name = "button_StartStop";
            button_StartStop.Size = new Size(120, 32);
            button_StartStop.TabIndex = 56;
            button_StartStop.TabStop = false;
            button_StartStop.Tag = "0";
            button_StartStop.Text = "Start";
            button_StartStop.UseVisualStyleBackColor = false;
            button_StartStop.Click += new EventHandler(button_StartStop_Click);
            // 
            // panel_Host
            // 
            panel_Host.BackColor = Color.LightSkyBlue;
            panel_Host.Dock = DockStyle.Left;
            panel_Host.Font = new Font("Calibri", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            panel_Host.Location = new Point(0, 64);
            panel_Host.Name = "panel_Host";
            panel_Host.Size = new Size(383, 362);
            panel_Host.TabIndex = 3;
            panel_Host.SizeChanged += new EventHandler(panel_Host_SizeChanged);
            // 
            // panel_Split
            // 
            panel_Split.BackColor = SystemColors.InactiveCaptionText;
            panel_Split.Dock = DockStyle.Left;
            panel_Split.Font = new Font("Calibri", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            panel_Split.Location = new Point(383, 64);
            panel_Split.Name = "panel_Split";
            panel_Split.Size = new Size(10, 362);
            panel_Split.TabIndex = 4;
            // 
            // panel_Enemy
            // 
            panel_Enemy.BackColor = SystemColors.Info;
            panel_Enemy.Dock = DockStyle.Fill;
            panel_Enemy.Enabled = false;
            panel_Enemy.Font = new Font("Calibri", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            panel_Enemy.Location = new Point(393, 64);
            panel_Enemy.Name = "panel_Enemy";
            panel_Enemy.Size = new Size(383, 362);
            panel_Enemy.TabIndex = 5;
            panel_Enemy.SizeChanged += new EventHandler(panel_Enemy_SizeChanged);
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(776, 471);
            Controls.Add(panel_Enemy);
            Controls.Add(panel_Split);
            Controls.Add(panel_Host);
            Controls.Add(panel_Control);
            Controls.Add(panel_Info);
            Controls.Add(panel_Title);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "MainForm";
            Text = "Battleship Game";
            Resize += new EventHandler(MainForm_Resize);
            panel_Title.ResumeLayout(false);
            panel_Title.PerformLayout();
            panel_Info.ResumeLayout(false);
            panel_Info.PerformLayout();
            panel_ShootEnemy.ResumeLayout(false);
            panel_ShootEnemy.PerformLayout();
            panel_Control.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel_Title;
        private Label label_Enemy;
        private Label label_Host;
        private Panel panel_Info;
        private TextBox textBox_EnemyShoot;
        private Panel panel1;
        private Panel panel_ShootEnemy;
        private TextBox textBox_HostShoot;
        private Panel panel_Control;
        private Button button_StartStop;
        private Panel panel_Host;
        private Panel panel_Split;
        private Panel panel_Enemy;
    }
}
