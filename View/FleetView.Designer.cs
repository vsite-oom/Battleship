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
            this.RowsTextBox = new System.Windows.Forms.TextBox();
            this.ColumnsTextBox = new System.Windows.Forms.TextBox();
            this.ColumnsLabel = new System.Windows.Forms.Label();
            this.RowsLabel = new System.Windows.Forms.Label();
            this.shipLenghts3 = new System.Windows.Forms.RadioButton();
            this.shipLenghts4 = new System.Windows.Forms.RadioButton();
            this.shipLenghts5 = new System.Windows.Forms.RadioButton();
            this.shiplenghts6 = new System.Windows.Forms.RadioButton();
            this.shipLengthsLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // CreateFleet
            // 
            this.CreateFleet.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.CreateFleet.Location = new System.Drawing.Point(529, 13);
            this.CreateFleet.Name = "CreateFleet";
            this.CreateFleet.Size = new System.Drawing.Size(80, 28);
            this.CreateFleet.TabIndex = 0;
            this.CreateFleet.Text = "Create fleet";
            this.CreateFleet.UseVisualStyleBackColor = true;
            this.CreateFleet.Click += new System.EventHandler(this.CreateFleet_Click);
            // 
            // RowsTextBox
            // 
            this.RowsTextBox.Location = new System.Drawing.Point(65, 19);
            this.RowsTextBox.Name = "RowsTextBox";
            this.RowsTextBox.Size = new System.Drawing.Size(100, 20);
            this.RowsTextBox.TabIndex = 1;
            // 
            // ColumnsTextBox
            // 
            this.ColumnsTextBox.Location = new System.Drawing.Point(247, 19);
            this.ColumnsTextBox.Name = "ColumnsTextBox";
            this.ColumnsTextBox.Size = new System.Drawing.Size(100, 20);
            this.ColumnsTextBox.TabIndex = 2;
            // 
            // ColumnsLabel
            // 
            this.ColumnsLabel.AutoSize = true;
            this.ColumnsLabel.Location = new System.Drawing.Point(194, 22);
            this.ColumnsLabel.Name = "ColumnsLabel";
            this.ColumnsLabel.Size = new System.Drawing.Size(47, 13);
            this.ColumnsLabel.TabIndex = 3;
            this.ColumnsLabel.Text = "Columns";
            // 
            // RowsLabel
            // 
            this.RowsLabel.AutoSize = true;
            this.RowsLabel.Location = new System.Drawing.Point(25, 22);
            this.RowsLabel.Name = "RowsLabel";
            this.RowsLabel.Size = new System.Drawing.Size(34, 13);
            this.RowsLabel.TabIndex = 4;
            this.RowsLabel.Text = "Rows";
            // 
            // shipLenghts3
            // 
            this.shipLenghts3.AutoSize = true;
            this.shipLenghts3.Location = new System.Drawing.Point(28, 77);
            this.shipLenghts3.Name = "shipLenghts3";
            this.shipLenghts3.Size = new System.Drawing.Size(91, 17);
            this.shipLenghts3.TabIndex = 5;
            this.shipLenghts3.TabStop = true;
            this.shipLenghts3.Text = "3, 2, 2, 1, 1, 1";
            this.shipLenghts3.UseVisualStyleBackColor = true;
            // 
            // shipLenghts4
            // 
            this.shipLenghts4.AutoSize = true;
            this.shipLenghts4.Location = new System.Drawing.Point(148, 77);
            this.shipLenghts4.Name = "shipLenghts4";
            this.shipLenghts4.Size = new System.Drawing.Size(139, 17);
            this.shipLenghts4.TabIndex = 6;
            this.shipLenghts4.TabStop = true;
            this.shipLenghts4.Text = "4, 3, 3, 2, 2, 2, 1, 1, 1, 1";
            this.shipLenghts4.UseVisualStyleBackColor = true;
            // 
            // shipLenghts5
            // 
            this.shipLenghts5.AutoSize = true;
            this.shipLenghts5.Location = new System.Drawing.Point(28, 100);
            this.shipLenghts5.Name = "shipLenghts5";
            this.shipLenghts5.Size = new System.Drawing.Size(199, 17);
            this.shipLenghts5.TabIndex = 7;
            this.shipLenghts5.TabStop = true;
            this.shipLenghts5.Text = "5, 4, 4, 3, 3, 3, 2, 2, 2, 2, 1, 1, 1, 1, 1";
            this.shipLenghts5.UseVisualStyleBackColor = true;
            // 
            // shiplenghts6
            // 
            this.shiplenghts6.AutoSize = true;
            this.shiplenghts6.Location = new System.Drawing.Point(28, 123);
            this.shiplenghts6.Name = "shiplenghts6";
            this.shiplenghts6.Size = new System.Drawing.Size(271, 17);
            this.shiplenghts6.TabIndex = 8;
            this.shiplenghts6.TabStop = true;
            this.shiplenghts6.Text = "6, 5, 5, 4, 4, 4, 3, 3, 3, 3, 2, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1";
            this.shiplenghts6.UseVisualStyleBackColor = true;
            // 
            // shipLengthsLabel
            // 
            this.shipLengthsLabel.AutoSize = true;
            this.shipLengthsLabel.Location = new System.Drawing.Point(25, 52);
            this.shipLengthsLabel.Name = "shipLengthsLabel";
            this.shipLengthsLabel.Size = new System.Drawing.Size(105, 13);
            this.shipLengthsLabel.TabIndex = 9;
            this.shipLengthsLabel.Text = "Choose ship lengths:";
            // 
            // FleetView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(635, 624);
            this.Controls.Add(this.shipLengthsLabel);
            this.Controls.Add(this.shiplenghts6);
            this.Controls.Add(this.shipLenghts5);
            this.Controls.Add(this.shipLenghts4);
            this.Controls.Add(this.shipLenghts3);
            this.Controls.Add(this.RowsLabel);
            this.Controls.Add(this.ColumnsLabel);
            this.Controls.Add(this.ColumnsTextBox);
            this.Controls.Add(this.RowsTextBox);
            this.Controls.Add(this.CreateFleet);
            this.Name = "FleetView";
            this.Text = "FleetView";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button CreateFleet;
        private System.Windows.Forms.TextBox RowsTextBox;
        private System.Windows.Forms.TextBox ColumnsTextBox;
        private System.Windows.Forms.Label ColumnsLabel;
        private System.Windows.Forms.Label RowsLabel;
        private System.Windows.Forms.RadioButton shipLenghts3;
        private System.Windows.Forms.RadioButton shipLenghts4;
        private System.Windows.Forms.RadioButton shipLenghts5;
        private System.Windows.Forms.RadioButton shiplenghts6;
        private System.Windows.Forms.Label shipLengthsLabel;
    }
}