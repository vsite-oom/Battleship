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
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            btnPlaceFleet = new Button();
            btnStartReset = new Button();
            SuspendLayout();
            // 
            // btnPlaceFleet
            // 
            btnPlaceFleet.Location = new Point(250, 650);
            btnPlaceFleet.Name = "btnPlaceFleet";
            btnPlaceFleet.Size = new Size(100, 50);
            btnPlaceFleet.TabIndex = 2;
            btnPlaceFleet.Text = "Place Fleet";
            btnPlaceFleet.UseVisualStyleBackColor = true;
            btnPlaceFleet.Click += btnPlaceFleet_Click;
            // 
            // btnStartReset
            // 
            btnStartReset.Location = new Point(450, 650);
            btnStartReset.Name = "btnStartReset";
            btnStartReset.Size = new Size(100, 50);
            btnStartReset.TabIndex = 3;
            btnStartReset.Text = "Start / Reset";
            btnStartReset.UseVisualStyleBackColor = true;
            btnStartReset.Click += btnStartReset_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1450, 750);
            Controls.Add(btnStartReset);
            Controls.Add(btnPlaceFleet);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion
        private System.ComponentModel.BackgroundWorker backgroundWorker1;  
        private System.ComponentModel.BackgroundWorker backgroundWorker2;  
        private Button btnPlaceFleet;
        private Button btnStartReset;
    }
}
