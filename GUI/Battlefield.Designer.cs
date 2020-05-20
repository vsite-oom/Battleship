namespace GUI
{
    partial class Battlefield
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
            this.buttonArrange = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonArrange
            // 
            this.buttonArrange.Location = new System.Drawing.Point(555, 497);
            this.buttonArrange.Name = "buttonArrange";
            this.buttonArrange.Size = new System.Drawing.Size(75, 23);
            this.buttonArrange.TabIndex = 1;
            this.buttonArrange.Text = "Složi";
            this.buttonArrange.UseVisualStyleBackColor = true;
            this.buttonArrange.Click += new System.EventHandler(this.arrange_Click);
            // 
            // Battlefield
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(643, 561);
            this.Controls.Add(this.buttonArrange);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Battlefield";
            this.Text = "Potapanje brodova";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button buttonArrange;
    }
}

