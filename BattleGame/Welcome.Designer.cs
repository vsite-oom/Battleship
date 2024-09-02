namespace Vsite.Oom.Battleship.Game
{
    partial class Welcome
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Welcome));
            label1 = new Label();
            toBattle = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.BackColor = Color.Transparent;
            label1.Dock = DockStyle.Top;
            label1.Font = new Font("Rockwell Condensed", 20F, FontStyle.Bold | FontStyle.Underline);
            label1.ForeColor = Color.Brown;
            label1.Location = new Point(0, 0);
            label1.Name = "label1";
            label1.Size = new Size(800, 40);
            label1.TabIndex = 0;
            label1.Text = "BATTLESHIPS OF WAR";
            label1.TextAlign = ContentAlignment.TopCenter;
            // 
            // toBattle
            // 
            toBattle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            toBattle.BackColor = Color.LightBlue;
            toBattle.Font = new Font("Rockwell Condensed", 20F);
            toBattle.ForeColor = Color.Black;
            toBattle.Location = new Point(306, 43);
            toBattle.Name = "toBattle";
            toBattle.Size = new Size(179, 91);
            toBattle.TabIndex = 1;
            toBattle.Text = "TO BATTLE";
            toBattle.UseVisualStyleBackColor = false;
            toBattle.Click += toBattle_Click;
            // 
            // Welcome
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(800, 450);
            Controls.Add(toBattle);
            Controls.Add(label1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "Welcome";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Welcome";
            Load += Welcome_Load;
            ResumeLayout(false);
        }

        #endregion

        private Label label1;
        private Button toBattle;
    }
}