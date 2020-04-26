namespace Battleship.GameBoard
{
    partial class GameBoard
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
            this.textBoxColumn = new System.Windows.Forms.TextBox();
            this.textBoxRow = new System.Windows.Forms.TextBox();
            this.labelColumn = new System.Windows.Forms.Label();
            this.labelRow = new System.Windows.Forms.Label();
            this.CreateBattlefield = new System.Windows.Forms.Button();
            this.Battlefield = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.createFleet = new System.Windows.Forms.Button();
            this.Battlefield.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxColumn
            // 
            this.textBoxColumn.Location = new System.Drawing.Point(128, 51);
            this.textBoxColumn.Name = "textBoxColumn";
            this.textBoxColumn.Size = new System.Drawing.Size(100, 31);
            this.textBoxColumn.TabIndex = 0;
            // 
            // textBoxRow
            // 
            this.textBoxRow.Location = new System.Drawing.Point(128, 105);
            this.textBoxRow.Name = "textBoxRow";
            this.textBoxRow.Size = new System.Drawing.Size(100, 31);
            this.textBoxRow.TabIndex = 1;
            // 
            // labelColumn
            // 
            this.labelColumn.AutoSize = true;
            this.labelColumn.Location = new System.Drawing.Point(28, 51);
            this.labelColumn.Name = "labelColumn";
            this.labelColumn.Size = new System.Drawing.Size(91, 25);
            this.labelColumn.TabIndex = 2;
            this.labelColumn.Text = "Column:";
            // 
            // labelRow
            // 
            this.labelRow.AutoSize = true;
            this.labelRow.Location = new System.Drawing.Point(33, 110);
            this.labelRow.Name = "labelRow";
            this.labelRow.Size = new System.Drawing.Size(60, 25);
            this.labelRow.TabIndex = 3;
            this.labelRow.Text = "Row:";
            // 
            // CreateBattlefield
            // 
            this.CreateBattlefield.Location = new System.Drawing.Point(38, 168);
            this.CreateBattlefield.Name = "CreateBattlefield";
            this.CreateBattlefield.Size = new System.Drawing.Size(190, 53);
            this.CreateBattlefield.TabIndex = 4;
            this.CreateBattlefield.Text = "Create battlefield";
            this.CreateBattlefield.UseVisualStyleBackColor = true;
            this.CreateBattlefield.Click += new System.EventHandler(this.CreateBattlefield_Click);
            // 
            // Battlefield
            // 
            this.Battlefield.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Battlefield.ColumnCount = 2;
            this.Battlefield.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.Battlefield.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.Battlefield.Controls.Add(this.label1, 1, 0);
            this.Battlefield.Location = new System.Drawing.Point(312, 51);
            this.Battlefield.Name = "Battlefield";
            this.Battlefield.RowCount = 2;
            this.Battlefield.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.Battlefield.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.Battlefield.Size = new System.Drawing.Size(645, 653);
            this.Battlefield.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(325, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 25);
            this.label1.TabIndex = 0;
            // 
            // createFleet
            // 
            this.createFleet.Location = new System.Drawing.Point(12, 275);
            this.createFleet.Name = "createFleet";
            this.createFleet.Size = new System.Drawing.Size(279, 66);
            this.createFleet.TabIndex = 6;
            this.createFleet.Text = "Create fleet";
            this.createFleet.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1006, 743);
            this.Controls.Add(this.createFleet);
            this.Controls.Add(this.Battlefield);
            this.Controls.Add(this.CreateBattlefield);
            this.Controls.Add(this.labelRow);
            this.Controls.Add(this.labelColumn);
            this.Controls.Add(this.textBoxRow);
            this.Controls.Add(this.textBoxColumn);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Battlefield.ResumeLayout(false);
            this.Battlefield.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxColumn;
        private System.Windows.Forms.TextBox textBoxRow;
        private System.Windows.Forms.Label labelColumn;
        private System.Windows.Forms.Label labelRow;
        private System.Windows.Forms.Button CreateBattlefield;
        private System.Windows.Forms.TableLayoutPanel Battlefield;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button createFleet;
    }
}

