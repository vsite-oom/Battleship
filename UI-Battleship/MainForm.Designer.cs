namespace UI_Battleship
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btNewGame = new Button();
            btExit = new Button();
            lbBattleship = new Label();
            SuspendLayout();
            // 
            // btNewGame
            // 
            btNewGame.Location = new Point(164, 97);
            btNewGame.Name = "btNewGame";
            btNewGame.Size = new Size(132, 43);
            btNewGame.TabIndex = 0;
            btNewGame.Text = "New game";
            btNewGame.UseVisualStyleBackColor = true;
            btNewGame.Click += btNewGame_Click;
            // 
            // btExit
            // 
            btExit.Location = new Point(164, 146);
            btExit.Name = "btExit";
            btExit.Size = new Size(132, 43);
            btExit.TabIndex = 1;
            btExit.Text = "Exit";
            btExit.UseVisualStyleBackColor = true;
            btExit.Click += btExit_Click;
            // 
            // lbBattleship
            // 
            lbBattleship.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lbBattleship.AutoSize = true;
            lbBattleship.Font = new Font("Segoe UI", 24F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbBattleship.Location = new Point(152, 21);
            lbBattleship.Name = "lbBattleship";
            lbBattleship.Size = new Size(160, 45);
            lbBattleship.TabIndex = 2;
            lbBattleship.Text = "Battleship";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(481, 260);
            Controls.Add(lbBattleship);
            Controls.Add(btExit);
            Controls.Add(btNewGame);
            Name = "MainForm";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btNewGame;
        private Button btExit;
        private Label lbBattleship;
    }
}
