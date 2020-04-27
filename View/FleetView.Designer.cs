namespace Vsite.Oom.Battleship.Model.View
{
    partial class FleetView
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
            this.CreateFleet = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // CreateFleet
            // 
            this.CreateFleet.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.CreateFleet.Location = new System.Drawing.Point(34, 12);
            this.CreateFleet.Name = "CreateFleet";
            this.CreateFleet.Size = new System.Drawing.Size(80, 28);
            this.CreateFleet.TabIndex = 0;
            this.CreateFleet.Text = "Create fleet";
            this.CreateFleet.UseVisualStyleBackColor = true;
            this.CreateFleet.Click += new System.EventHandler(this.CreateFleet_Click);
            // 
            // FleetView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(635, 624);
            this.Controls.Add(this.CreateFleet);
            this.Name = "FleetView";
            this.Text = "FleetView";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button CreateFleet;
    }
}