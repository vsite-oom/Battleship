namespace BattleshipGUI
{
    partial class startingForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.button_WOC2 = new ePOSOne.btnProduct.Button_WOC();
            this.button_WOC1 = new ePOSOne.btnProduct.Button_WOC();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Ink Free", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(17, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(281, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Please choose one of the following:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // button_WOC2
            // 
            this.button_WOC2.BackColor = System.Drawing.Color.Transparent;
            this.button_WOC2.BorderColor = System.Drawing.Color.DarkRed;
            this.button_WOC2.ButtonColor = System.Drawing.Color.DarkRed;
            this.button_WOC2.FlatAppearance.BorderSize = 0;
            this.button_WOC2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.button_WOC2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.button_WOC2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_WOC2.Font = new System.Drawing.Font("Ink Free", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_WOC2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button_WOC2.Location = new System.Drawing.Point(12, 63);
            this.button_WOC2.Name = "button_WOC2";
            this.button_WOC2.OnHoverBorderColor = System.Drawing.Color.Black;
            this.button_WOC2.OnHoverButtonColor = System.Drawing.Color.Yellow;
            this.button_WOC2.OnHoverTextColor = System.Drawing.Color.Purple;
            this.button_WOC2.Size = new System.Drawing.Size(134, 23);
            this.button_WOC2.TabIndex = 6;
            this.button_WOC2.Text = "Player vs Computer";
            this.button_WOC2.TextColor = System.Drawing.Color.White;
            this.button_WOC2.UseVisualStyleBackColor = false;
            this.button_WOC2.Click += new System.EventHandler(this.button_WOC2_Click);
            // 
            // button_WOC1
            // 
            this.button_WOC1.BackColor = System.Drawing.Color.Transparent;
            this.button_WOC1.BorderColor = System.Drawing.Color.DarkRed;
            this.button_WOC1.ButtonColor = System.Drawing.Color.DarkRed;
            this.button_WOC1.FlatAppearance.BorderSize = 0;
            this.button_WOC1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.button_WOC1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.button_WOC1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_WOC1.Font = new System.Drawing.Font("Ink Free", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_WOC1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button_WOC1.Location = new System.Drawing.Point(152, 63);
            this.button_WOC1.Name = "button_WOC1";
            this.button_WOC1.OnHoverBorderColor = System.Drawing.Color.Black;
            this.button_WOC1.OnHoverButtonColor = System.Drawing.Color.Yellow;
            this.button_WOC1.OnHoverTextColor = System.Drawing.Color.Purple;
            this.button_WOC1.Size = new System.Drawing.Size(160, 23);
            this.button_WOC1.TabIndex = 7;
            this.button_WOC1.Text = "Random fleet generator";
            this.button_WOC1.TextColor = System.Drawing.Color.White;
            this.button_WOC1.UseVisualStyleBackColor = false;
            this.button_WOC1.Click += new System.EventHandler(this.button_WOC1_Click);
            // 
            // startingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(324, 111);
            this.Controls.Add(this.button_WOC1);
            this.Controls.Add(this.button_WOC2);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(340, 150);
            this.MinimumSize = new System.Drawing.Size(315, 150);
            this.Name = "startingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Choose GUI";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private ePOSOne.btnProduct.Button_WOC button_WOC2;
        private ePOSOne.btnProduct.Button_WOC button_WOC1;
    }
}