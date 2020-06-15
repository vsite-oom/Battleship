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
			this.Arrange = new System.Windows.Forms.Button();
			this.Play = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// Arrange
			// 
			this.Arrange.Location = new System.Drawing.Point(42, 35);
			this.Arrange.Name = "Arrange";
			this.Arrange.Size = new System.Drawing.Size(75, 23);
			this.Arrange.TabIndex = 1;
			this.Arrange.Text = "Arrange";
			this.Arrange.UseVisualStyleBackColor = true;
			this.Arrange.Click += new System.EventHandler(this.Arrange_Click);
			// 
			// Play
			// 
			this.Play.Location = new System.Drawing.Point(204, 35);
			this.Play.Name = "Play";
			this.Play.Size = new System.Drawing.Size(75, 23);
			this.Play.TabIndex = 2;
			this.Play.Text = "Play Game";
			this.Play.UseVisualStyleBackColor = true;
			this.Play.Visible = false;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(817, 35);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 3;
			this.button1.Text = "Quit Game";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.Quit_Game);
			// 
			// FleetForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(918, 574);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.Play);
			this.Controls.Add(this.Arrange);
			this.Name = "FleetForm";
			this.Text = "Fleet";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button Arrange;
		private System.Windows.Forms.Button Play;
		private System.Windows.Forms.Button button1;
	}
}