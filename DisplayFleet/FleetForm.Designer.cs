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
            this.fleetGrid1 = new DisplayFleet.FleetGrid(this.components);
            this.startGame = new System.Windows.Forms.Button();
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
            this.button1.Click += new System.EventHandler(this.quitGame);
            // 
            // fleetGrid1
            // 
            this.fleetGrid1.Location = new System.Drawing.Point(0, 0);
            this.fleetGrid1.Name = "fleetGrid1";
            this.fleetGrid1.Size = new System.Drawing.Size(0, 0);
            this.fleetGrid1.TabIndex = 0;
            // 
            // startGame
            // 
            this.startGame.Location = new System.Drawing.Point(234, 12);
            this.startGame.Name = "startGame";
            this.startGame.Size = new System.Drawing.Size(75, 23);
            this.startGame.TabIndex = 3;
            this.startGame.Text = "Start Game";
            this.startGame.UseVisualStyleBackColor = true;
            this.startGame.Click += new System.EventHandler(this.button2_Click);
            // 
            // FleetForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(862, 450);
            this.Controls.Add(this.startGame);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.align);
            this.Name = "FleetForm";
            this.Text = "Flota";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button align;
        private System.Windows.Forms.Button button1;
        private FleetGrid fleetGrid1;
        private System.Windows.Forms.Button startGame;
    }
}

