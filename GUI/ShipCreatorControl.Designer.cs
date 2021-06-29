
namespace GUI
{
    partial class ShipCreatorControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnMinus = new System.Windows.Forms.Button();
            this.btnPlus = new System.Windows.Forms.Button();
            this.btnFirstField = new System.Windows.Forms.Button();
            this.btnFifthField = new System.Windows.Forms.Button();
            this.btnFourthField = new System.Windows.Forms.Button();
            this.btnThirdField = new System.Windows.Forms.Button();
            this.btnSecondField = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnMinus
            // 
            this.btnMinus.Location = new System.Drawing.Point(4, 4);
            this.btnMinus.Name = "btnMinus";
            this.btnMinus.Size = new System.Drawing.Size(35, 35);
            this.btnMinus.TabIndex = 0;
            this.btnMinus.Text = "-";
            this.btnMinus.UseVisualStyleBackColor = true;
            this.btnMinus.Click += new System.EventHandler(this.btnMinus_Click);
            // 
            // btnPlus
            // 
            this.btnPlus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPlus.Location = new System.Drawing.Point(250, 4);
            this.btnPlus.Name = "btnPlus";
            this.btnPlus.Size = new System.Drawing.Size(35, 35);
            this.btnPlus.TabIndex = 2;
            this.btnPlus.Text = "+";
            this.btnPlus.UseVisualStyleBackColor = true;
            this.btnPlus.Click += new System.EventHandler(this.btnPlus_Click);
            // 
            // btnFirstField
            // 
            this.btnFirstField.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnFirstField.Location = new System.Drawing.Point(45, 4);
            this.btnFirstField.Name = "btnFirstField";
            this.btnFirstField.Size = new System.Drawing.Size(35, 35);
            this.btnFirstField.TabIndex = 3;
            this.btnFirstField.TabStop = false;
            this.btnFirstField.UseVisualStyleBackColor = false;
            this.btnFirstField.Visible = false;
            // 
            // btnFifthField
            // 
            this.btnFifthField.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnFifthField.Location = new System.Drawing.Point(209, 4);
            this.btnFifthField.Name = "btnFifthField";
            this.btnFifthField.Size = new System.Drawing.Size(35, 35);
            this.btnFifthField.TabIndex = 4;
            this.btnFifthField.TabStop = false;
            this.btnFifthField.UseVisualStyleBackColor = false;
            this.btnFifthField.Visible = false;
            // 
            // btnFourthField
            // 
            this.btnFourthField.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnFourthField.Location = new System.Drawing.Point(168, 4);
            this.btnFourthField.Name = "btnFourthField";
            this.btnFourthField.Size = new System.Drawing.Size(35, 35);
            this.btnFourthField.TabIndex = 5;
            this.btnFourthField.TabStop = false;
            this.btnFourthField.UseVisualStyleBackColor = false;
            this.btnFourthField.Visible = false;
            // 
            // btnThirdField
            // 
            this.btnThirdField.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnThirdField.Location = new System.Drawing.Point(127, 4);
            this.btnThirdField.Name = "btnThirdField";
            this.btnThirdField.Size = new System.Drawing.Size(35, 35);
            this.btnThirdField.TabIndex = 6;
            this.btnThirdField.TabStop = false;
            this.btnThirdField.UseVisualStyleBackColor = false;
            this.btnThirdField.Visible = false;
            // 
            // btnSecondField
            // 
            this.btnSecondField.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnSecondField.Location = new System.Drawing.Point(86, 4);
            this.btnSecondField.Name = "btnSecondField";
            this.btnSecondField.Size = new System.Drawing.Size(35, 35);
            this.btnSecondField.TabIndex = 7;
            this.btnSecondField.TabStop = false;
            this.btnSecondField.UseVisualStyleBackColor = false;
            this.btnSecondField.Visible = false;
            // 
            // ShipCreatorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnSecondField);
            this.Controls.Add(this.btnThirdField);
            this.Controls.Add(this.btnFourthField);
            this.Controls.Add(this.btnFifthField);
            this.Controls.Add(this.btnFirstField);
            this.Controls.Add(this.btnPlus);
            this.Controls.Add(this.btnMinus);
            this.MaximumSize = new System.Drawing.Size(289, 43);
            this.MinimumSize = new System.Drawing.Size(289, 43);
            this.Name = "ShipCreatorControl";
            this.Size = new System.Drawing.Size(289, 43);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnMinus;
        private System.Windows.Forms.Button btnPlus;
        private System.Windows.Forms.Button btnFirstField;
        private System.Windows.Forms.Button btnFifthField;
        private System.Windows.Forms.Button btnFourthField;
        private System.Windows.Forms.Button btnThirdField;
        private System.Windows.Forms.Button btnSecondField;
    }
}
