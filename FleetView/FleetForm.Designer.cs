namespace FleetView
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
            this.buttonDraw = new System.Windows.Forms.Button();
            this.playButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonDraw
            // 
            this.buttonDraw.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.buttonDraw.Location = new System.Drawing.Point(12, 520);
            this.buttonDraw.Name = "buttonDraw";
            this.buttonDraw.Size = new System.Drawing.Size(141, 30);
            this.buttonDraw.TabIndex = 1;
            this.buttonDraw.Text = "&Draw";
            this.buttonDraw.UseVisualStyleBackColor = true;
            this.buttonDraw.Click += new System.EventHandler(this.DrawButton);
            // 
            // playButton
            // 
            this.playButton.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.playButton.Location = new System.Drawing.Point(850, 520);
            this.playButton.Name = "playButton";
            this.playButton.Size = new System.Drawing.Size(141, 30);
            this.playButton.TabIndex = 2;
            this.playButton.Text = "Play";
            this.playButton.UseVisualStyleBackColor = true;
            this.playButton.Click += new System.EventHandler(this.playButton_Click);
            // 
            // FleetForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlText;
            this.ClientSize = new System.Drawing.Size(1115, 562);
            this.Controls.Add(this.playButton);
            this.Controls.Add(this.buttonDraw);
            this.Name = "FleetForm";
            this.Text = "Fleet";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button buttonDraw;
        private System.Windows.Forms.Button playButton;
    }
}