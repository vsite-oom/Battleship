namespace ArrangeFleetGUI
{
    partial class Battleship
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
            this.button101 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button101
            // 
            this.button101.Location = new System.Drawing.Point(391, 470);
            this.button101.Name = "button101";
            this.button101.Size = new System.Drawing.Size(75, 23);
            this.button101.TabIndex = 100;
            this.button101.Text = "Arrange";
            this.button101.UseVisualStyleBackColor = true;
            this.button101.Click += new System.EventHandler(this.button101_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button101);
            this.panel1.Location = new System.Drawing.Point(8, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(476, 501);
            this.panel1.TabIndex = 0;
            // 
            // Battleship
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(486, 510);
            this.Controls.Add(this.panel1);
            this.MaximumSize = new System.Drawing.Size(504, 557);
            this.MinimumSize = new System.Drawing.Size(504, 557);
            this.Name = "Battleship";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Text = "Battleship";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button101;
        private System.Windows.Forms.Panel panel1;
    }
}

