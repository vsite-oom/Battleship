namespace GUI
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnIndicator1 = new Button();
            btnIndicator2 = new Button();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            btnPlaceFleet = new Button();
            btnStartReset = new Button();
            SuspendLayout();
            // 
            // btnIndicator1
            // 
            btnIndicator1.Location = new Point(325, 25);
            btnIndicator1.Name = "btnIndicator1";
            btnIndicator1.Size = new Size(50, 50);
            btnIndicator1.TabIndex = 0;
            btnIndicator1.UseVisualStyleBackColor = true;
            // 
            // btnIndicator2
            // 
            btnIndicator2.Location = new Point(925, 25);
            btnIndicator2.Name = "btnIndicator2";
            btnIndicator2.Size = new Size(50, 50);
            btnIndicator2.TabIndex = 1;
            btnIndicator2.UseVisualStyleBackColor = true;
            // 
            // btnPlaceFleet
            // 
            btnPlaceFleet.Location = new Point(200, 625);
            btnPlaceFleet.Name = "btnPlaceFleet";
            btnPlaceFleet.Size = new Size(100, 50);
            btnPlaceFleet.TabIndex = 2;
            btnPlaceFleet.Text = "Place Fleet";
            btnPlaceFleet.UseVisualStyleBackColor = true;
            // 
            // btnStartReset
            // 
            btnStartReset.Location = new Point(350, 625);
            btnStartReset.Name = "btnStartReset";
            btnStartReset.Size = new Size(100, 50);
            btnStartReset.TabIndex = 3;
            btnStartReset.Text = "Start / Reset";
            btnStartReset.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1265, 736);
            Controls.Add(btnStartReset);
            Controls.Add(btnPlaceFleet);
            Controls.Add(btnIndicator2);
            Controls.Add(btnIndicator1);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion

        private Button btnIndicator1;
        private Button btnIndicator2;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;  
        private System.ComponentModel.BackgroundWorker backgroundWorker2;  
        private Button btnPlaceFleet;
        private Button btnStartReset;
    }
}
