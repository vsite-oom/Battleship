namespace BattleshipGUI
{
    partial class randomFleetGenerator
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
            this.grid = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // grid
            // 
            this.grid.Location = new System.Drawing.Point(21, 12);
            this.grid.Name = "grid";
            this.grid.Size = new System.Drawing.Size(500, 500);
            this.grid.TabIndex = 1;
            this.grid.Paint += new System.Windows.Forms.PaintEventHandler(this.Grid_Paint);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(225, 524);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Generate";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Generate_Click);
            // 
            // randomFleetGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(543, 549);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.grid);
            this.Name = "randomFleetGenerator";
            this.Text = "Random fleet generator";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel grid;
        private System.Windows.Forms.Button button2;
    }
}

