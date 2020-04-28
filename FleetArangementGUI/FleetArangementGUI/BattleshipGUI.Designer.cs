namespace FleetArangementGUI
{
    partial class BattleshipGUI
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
            this.fleetsGrid = new FleetArangementGUI.FleetsGrid();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // align
            // 
            this.align.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.align.Location = new System.Drawing.Point(39, 48);
            this.align.Name = "align";
            this.align.Size = new System.Drawing.Size(75, 23);
            this.align.TabIndex = 1;
            this.align.Text = "&Arrange!";
            this.align.UseVisualStyleBackColor = true;
            this.align.Click += new System.EventHandler(this.ArrangeFleet);
            // 
            // fleetGrid
            // 
            this.fleetsGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fleetsGrid.BackColor = System.Drawing.Color.Aqua;
            this.fleetsGrid.Location = new System.Drawing.Point(13, 95);
            this.fleetsGrid.Name = "fleetsGrid";
            this.fleetsGrid.Size = new System.Drawing.Size(381, 343);
            this.fleetsGrid.TabIndex = 0;
            this.fleetsGrid.Text = "fleetsGrid";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(294, 48);
            this.button1.Name = "button";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Quit";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Quit);
            // 
            // FleetForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(406, 450);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.align);
            this.Controls.Add(this.fleetsGrid);
            this.Name = "FleetForm";
            this.Text = "Flota";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button align;
        private FleetsGrid fleetsGrid;
    }
}
