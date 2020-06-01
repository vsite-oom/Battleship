namespace GUI
{
	partial class Form1
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
			this.CreateFleetButton = new System.Windows.Forms.Button();
			this.PlayerGameBoard = new System.Windows.Forms.Panel();
			this.panel1 = new System.Windows.Forms.Panel();
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// CreateFleetButton
			// 
			this.CreateFleetButton.Location = new System.Drawing.Point(12, 456);
			this.CreateFleetButton.Name = "CreateFleetButton";
			this.CreateFleetButton.Size = new System.Drawing.Size(75, 23);
			this.CreateFleetButton.TabIndex = 0;
			this.CreateFleetButton.Text = "Slozi";
			this.CreateFleetButton.UseVisualStyleBackColor = true;
			this.CreateFleetButton.Click += new System.EventHandler(this.CreateFleetButton_Click);
			// 
			// PlayerGameBoard
			// 
			this.PlayerGameBoard.Location = new System.Drawing.Point(0, 10);
			this.PlayerGameBoard.Margin = new System.Windows.Forms.Padding(0);
			this.PlayerGameBoard.Name = "PlayerGameBoard";
			this.PlayerGameBoard.Size = new System.Drawing.Size(424, 413);
			this.PlayerGameBoard.TabIndex = 1;
			this.PlayerGameBoard.Click += new System.EventHandler(this.PlayerGameBoard_Click);
			// 
			// panel1
			// 
			this.panel1.Location = new System.Drawing.Point(439, 10);
			this.panel1.Margin = new System.Windows.Forms.Padding(0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(424, 413);
			this.panel1.TabIndex = 2;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(329, 502);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(172, 33);
			this.label1.TabIndex = 3;
			this.label1.Text = "label1";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(864, 561);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.PlayerGameBoard);
			this.Controls.Add(this.CreateFleetButton);
			this.MaximumSize = new System.Drawing.Size(880, 600);
			this.MinimumSize = new System.Drawing.Size(880, 600);
			this.Name = "Form1";
			this.Text = "Flota";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button CreateFleetButton;
		private System.Windows.Forms.Panel PlayerGameBoard;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label label1;
	}
}

