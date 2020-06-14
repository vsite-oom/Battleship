namespace GUI
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
            this.resetbutton = new System.Windows.Forms.Button();
            this.aiPanel = new System.Windows.Forms.Panel();
            this.igracPanel = new System.Windows.Forms.Panel();
            this.zadnjiPotezAi = new System.Windows.Forms.Label();
            this.brodoviIgrac = new System.Windows.Forms.Label();
            this.brodoviAi = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // resetbutton
            // 
            this.resetbutton.Location = new System.Drawing.Point(12, 578);
            this.resetbutton.Name = "resetbutton";
            this.resetbutton.Size = new System.Drawing.Size(75, 23);
            this.resetbutton.TabIndex = 1;
            this.resetbutton.Text = "Složi";
            this.resetbutton.UseVisualStyleBackColor = true;
            this.resetbutton.Click += new System.EventHandler(this.resetbutton_Click);
            // 
            // aiPanel
            // 
            this.aiPanel.Location = new System.Drawing.Point(52, 46);
            this.aiPanel.Name = "aiPanel";
            this.aiPanel.Size = new System.Drawing.Size(520, 514);
            this.aiPanel.TabIndex = 2;
            // 
            // igracPanel
            // 
            this.igracPanel.Location = new System.Drawing.Point(663, 46);
            this.igracPanel.Name = "igracPanel";
            this.igracPanel.Size = new System.Drawing.Size(572, 514);
            this.igracPanel.TabIndex = 3;
            // 
            // zadnjiPotezAi
            // 
            this.zadnjiPotezAi.AutoSize = true;
            this.zadnjiPotezAi.Location = new System.Drawing.Point(618, 217);
            this.zadnjiPotezAi.Name = "zadnjiPotezAi";
            this.zadnjiPotezAi.Size = new System.Drawing.Size(0, 13);
            this.zadnjiPotezAi.TabIndex = 4;
            // 
            // brodoviIgrac
            // 
            this.brodoviIgrac.AutoSize = true;
            this.brodoviIgrac.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.brodoviIgrac.Location = new System.Drawing.Point(671, 581);
            this.brodoviIgrac.Name = "brodoviIgrac";
            this.brodoviIgrac.Size = new System.Drawing.Size(0, 20);
            this.brodoviIgrac.TabIndex = 5;
            // 
            // brodoviAi
            // 
            this.brodoviAi.AutoSize = true;
            this.brodoviAi.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.brodoviAi.Location = new System.Drawing.Point(437, 581);
            this.brodoviAi.Name = "brodoviAi";
            this.brodoviAi.Size = new System.Drawing.Size(0, 20);
            this.brodoviAi.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1286, 619);
            this.Controls.Add(this.brodoviAi);
            this.Controls.Add(this.brodoviIgrac);
            this.Controls.Add(this.zadnjiPotezAi);
            this.Controls.Add(this.igracPanel);
            this.Controls.Add(this.aiPanel);
            this.Controls.Add(this.resetbutton);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button resetbutton;
        private System.Windows.Forms.Panel aiPanel;
        private System.Windows.Forms.Panel igracPanel;
        private System.Windows.Forms.Label zadnjiPotezAi;
        private System.Windows.Forms.Label brodoviIgrac;
        private System.Windows.Forms.Label brodoviAi;
    }
}

