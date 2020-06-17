namespace BattleshipGUI
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.CreateFleet = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // CreateFleet
            // 
            this.CreateFleet.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.CreateFleet.Location = new System.Drawing.Point(15, 15);
            this.CreateFleet.Margin = new System.Windows.Forms.Padding(6);
            this.CreateFleet.Name = "CreateFleet";
            this.CreateFleet.Size = new System.Drawing.Size(150, 44);
            this.CreateFleet.TabIndex = 1;
            this.CreateFleet.Text = "Create Fleet";
            this.CreateFleet.UseVisualStyleBackColor = false;
            this.CreateFleet.Click += new System.EventHandler(this.CreateFleet_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(1968, 887);
            this.Controls.Add(this.CreateFleet);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Battleship";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button CreateFleet;
    }
}

