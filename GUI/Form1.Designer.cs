using System.Windows.Forms;

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
			this.components = new System.ComponentModel.Container();
			this.CreateFleetButton = new System.Windows.Forms.Button();
			this.PlayerGameBoard = new System.Windows.Forms.Panel();
			this.EnemyGameBoard = new System.Windows.Forms.Panel();
			this.TargetFieldLabel = new System.Windows.Forms.Label();
			this.AcceptFleetButton = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.label14 = new System.Windows.Forms.Label();
			this.label15 = new System.Windows.Forms.Label();
			this.label16 = new System.Windows.Forms.Label();
			this.label17 = new System.Windows.Forms.Label();
			this.label18 = new System.Windows.Forms.Label();
			this.label19 = new System.Windows.Forms.Label();
			this.label20 = new System.Windows.Forms.Label();
			this.label21 = new System.Windows.Forms.Label();
			this.label22 = new System.Windows.Forms.Label();
			this.label23 = new System.Windows.Forms.Label();
			this.label24 = new System.Windows.Forms.Label();
			this.label25 = new System.Windows.Forms.Label();
			this.label26 = new System.Windows.Forms.Label();
			this.label27 = new System.Windows.Forms.Label();
			this.label28 = new System.Windows.Forms.Label();
			this.label29 = new System.Windows.Forms.Label();
			this.label30 = new System.Windows.Forms.Label();
			this.label31 = new System.Windows.Forms.Label();
			this.label32 = new System.Windows.Forms.Label();
			this.label33 = new System.Windows.Forms.Label();
			this.label34 = new System.Windows.Forms.Label();
			this.label35 = new System.Windows.Forms.Label();
			this.label36 = new System.Windows.Forms.Label();
			this.label37 = new System.Windows.Forms.Label();
			this.label38 = new System.Windows.Forms.Label();
			this.label39 = new System.Windows.Forms.Label();
			this.label40 = new System.Windows.Forms.Label();
			this.label41 = new System.Windows.Forms.Label();
			this.InfoBoardTextBox = new System.Windows.Forms.TextBox();
			this.EnemyAI = new System.Windows.Forms.Timer(this.components);
			this.SuspendLayout();
			// 
			// CreateFleetButton
			// 
			this.CreateFleetButton.Location = new System.Drawing.Point(59, 471);
			this.CreateFleetButton.Name = "CreateFleetButton";
			this.CreateFleetButton.Size = new System.Drawing.Size(90, 30);
			this.CreateFleetButton.TabIndex = 0;
			this.CreateFleetButton.Text = "Slozi";
			this.CreateFleetButton.UseVisualStyleBackColor = true;
			this.CreateFleetButton.Click += new System.EventHandler(this.CreateFleetButton_Click);
			// 
			// PlayerGameBoard
			// 
			this.PlayerGameBoard.Location = new System.Drawing.Point(59, 33);
			this.PlayerGameBoard.Margin = new System.Windows.Forms.Padding(0);
			this.PlayerGameBoard.Name = "PlayerGameBoard";
			this.PlayerGameBoard.Size = new System.Drawing.Size(424, 413);
			this.PlayerGameBoard.TabIndex = 1;
			// 
			// EnemyGameBoard
			// 
			this.EnemyGameBoard.Location = new System.Drawing.Point(677, 33);
			this.EnemyGameBoard.Margin = new System.Windows.Forms.Padding(0);
			this.EnemyGameBoard.Name = "EnemyGameBoard";
			this.EnemyGameBoard.Size = new System.Drawing.Size(424, 413);
			this.EnemyGameBoard.TabIndex = 2;
			// 
			// TargetFieldLabel
			// 
			this.TargetFieldLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.TargetFieldLabel.Location = new System.Drawing.Point(511, 56);
			this.TargetFieldLabel.Name = "TargetFieldLabel";
			this.TargetFieldLabel.Size = new System.Drawing.Size(136, 19);
			this.TargetFieldLabel.TabIndex = 3;
			this.TargetFieldLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// AcceptFleetButton
			// 
			this.AcceptFleetButton.Enabled = false;
			this.AcceptFleetButton.Location = new System.Drawing.Point(59, 516);
			this.AcceptFleetButton.Name = "AcceptFleetButton";
			this.AcceptFleetButton.Size = new System.Drawing.Size(90, 30);
			this.AcceptFleetButton.TabIndex = 4;
			this.AcceptFleetButton.Text = "Potvrdi";
			this.AcceptFleetButton.UseVisualStyleBackColor = true;
			this.AcceptFleetButton.Click += new System.EventHandler(this.AcceptFleetButton_Click);
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(511, 33);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(136, 26);
			this.label1.TabIndex = 5;
			this.label1.Text = "Targeted field";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(78, 7);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(23, 24);
			this.label2.TabIndex = 6;
			this.label2.Text = "A";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(118, 7);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(23, 24);
			this.label3.TabIndex = 7;
			this.label3.Text = "B";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label4
			// 
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label4.Location = new System.Drawing.Point(438, 7);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(23, 24);
			this.label4.TabIndex = 8;
			this.label4.Text = "J";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label5
			// 
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label5.Location = new System.Drawing.Point(159, 7);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(23, 24);
			this.label5.TabIndex = 9;
			this.label5.Text = "C";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label6
			// 
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label6.Location = new System.Drawing.Point(198, 7);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(23, 24);
			this.label6.TabIndex = 10;
			this.label6.Text = "D";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label7
			// 
			this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label7.Location = new System.Drawing.Point(239, 7);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(23, 24);
			this.label7.TabIndex = 11;
			this.label7.Text = "E";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label8
			// 
			this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label8.Location = new System.Drawing.Point(277, 7);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(23, 24);
			this.label8.TabIndex = 12;
			this.label8.Text = "F";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label9
			// 
			this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label9.Location = new System.Drawing.Point(318, 7);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(23, 24);
			this.label9.TabIndex = 13;
			this.label9.Text = "G";
			this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label10
			// 
			this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label10.Location = new System.Drawing.Point(358, 7);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(23, 24);
			this.label10.TabIndex = 14;
			this.label10.Text = "H";
			this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label11
			// 
			this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label11.Location = new System.Drawing.Point(398, 7);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(23, 24);
			this.label11.TabIndex = 15;
			this.label11.Text = "I";
			this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label12
			// 
			this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label12.Location = new System.Drawing.Point(16, 35);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(40, 40);
			this.label12.TabIndex = 16;
			this.label12.Text = "1";
			this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label13
			// 
			this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label13.Location = new System.Drawing.Point(16, 75);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(40, 40);
			this.label13.TabIndex = 17;
			this.label13.Text = "2";
			this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label14
			// 
			this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label14.Location = new System.Drawing.Point(16, 115);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(40, 40);
			this.label14.TabIndex = 18;
			this.label14.Text = "3";
			this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label15
			// 
			this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label15.Location = new System.Drawing.Point(16, 155);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(40, 40);
			this.label15.TabIndex = 19;
			this.label15.Text = "4";
			this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label16
			// 
			this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label16.Location = new System.Drawing.Point(16, 195);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(40, 40);
			this.label16.TabIndex = 20;
			this.label16.Text = "5";
			this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label17
			// 
			this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label17.Location = new System.Drawing.Point(16, 240);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(40, 40);
			this.label17.TabIndex = 21;
			this.label17.Text = "6";
			this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label18
			// 
			this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label18.Location = new System.Drawing.Point(16, 280);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(40, 40);
			this.label18.TabIndex = 22;
			this.label18.Text = "7";
			this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label19
			// 
			this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label19.Location = new System.Drawing.Point(16, 320);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(40, 40);
			this.label19.TabIndex = 23;
			this.label19.Text = "8";
			this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label20
			// 
			this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label20.Location = new System.Drawing.Point(16, 360);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(40, 40);
			this.label20.TabIndex = 24;
			this.label20.Text = "9";
			this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label21
			// 
			this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label21.Location = new System.Drawing.Point(16, 400);
			this.label21.Name = "label21";
			this.label21.Size = new System.Drawing.Size(40, 40);
			this.label21.TabIndex = 25;
			this.label21.Text = "10";
			this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label22
			// 
			this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label22.Location = new System.Drawing.Point(1015, 7);
			this.label22.Name = "label22";
			this.label22.Size = new System.Drawing.Size(23, 24);
			this.label22.TabIndex = 35;
			this.label22.Text = "I";
			this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label23
			// 
			this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label23.Location = new System.Drawing.Point(975, 7);
			this.label23.Name = "label23";
			this.label23.Size = new System.Drawing.Size(23, 24);
			this.label23.TabIndex = 34;
			this.label23.Text = "H";
			this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label24
			// 
			this.label24.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label24.Location = new System.Drawing.Point(935, 7);
			this.label24.Name = "label24";
			this.label24.Size = new System.Drawing.Size(23, 24);
			this.label24.TabIndex = 33;
			this.label24.Text = "G";
			this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label25
			// 
			this.label25.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label25.Location = new System.Drawing.Point(894, 7);
			this.label25.Name = "label25";
			this.label25.Size = new System.Drawing.Size(23, 24);
			this.label25.TabIndex = 32;
			this.label25.Text = "F";
			this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label26
			// 
			this.label26.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label26.Location = new System.Drawing.Point(856, 7);
			this.label26.Name = "label26";
			this.label26.Size = new System.Drawing.Size(23, 24);
			this.label26.TabIndex = 31;
			this.label26.Text = "E";
			this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label27
			// 
			this.label27.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label27.Location = new System.Drawing.Point(815, 7);
			this.label27.Name = "label27";
			this.label27.Size = new System.Drawing.Size(23, 24);
			this.label27.TabIndex = 30;
			this.label27.Text = "D";
			this.label27.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label28
			// 
			this.label28.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label28.Location = new System.Drawing.Point(776, 7);
			this.label28.Name = "label28";
			this.label28.Size = new System.Drawing.Size(23, 24);
			this.label28.TabIndex = 29;
			this.label28.Text = "C";
			this.label28.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label29
			// 
			this.label29.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label29.Location = new System.Drawing.Point(1055, 7);
			this.label29.Name = "label29";
			this.label29.Size = new System.Drawing.Size(23, 24);
			this.label29.TabIndex = 28;
			this.label29.Text = "J";
			this.label29.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label30
			// 
			this.label30.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label30.Location = new System.Drawing.Point(735, 7);
			this.label30.Name = "label30";
			this.label30.Size = new System.Drawing.Size(23, 24);
			this.label30.TabIndex = 27;
			this.label30.Text = "B";
			this.label30.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label31
			// 
			this.label31.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label31.Location = new System.Drawing.Point(695, 7);
			this.label31.Name = "label31";
			this.label31.Size = new System.Drawing.Size(23, 24);
			this.label31.TabIndex = 26;
			this.label31.Text = "A";
			this.label31.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label32
			// 
			this.label32.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label32.Location = new System.Drawing.Point(1104, 400);
			this.label32.Name = "label32";
			this.label32.Size = new System.Drawing.Size(40, 40);
			this.label32.TabIndex = 45;
			this.label32.Text = "10";
			this.label32.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label33
			// 
			this.label33.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label33.Location = new System.Drawing.Point(1104, 360);
			this.label33.Name = "label33";
			this.label33.Size = new System.Drawing.Size(40, 40);
			this.label33.TabIndex = 44;
			this.label33.Text = "9";
			this.label33.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label34
			// 
			this.label34.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label34.Location = new System.Drawing.Point(1104, 320);
			this.label34.Name = "label34";
			this.label34.Size = new System.Drawing.Size(40, 40);
			this.label34.TabIndex = 43;
			this.label34.Text = "8";
			this.label34.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label35
			// 
			this.label35.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label35.Location = new System.Drawing.Point(1104, 280);
			this.label35.Name = "label35";
			this.label35.Size = new System.Drawing.Size(40, 40);
			this.label35.TabIndex = 42;
			this.label35.Text = "7";
			this.label35.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label36
			// 
			this.label36.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label36.Location = new System.Drawing.Point(1104, 240);
			this.label36.Name = "label36";
			this.label36.Size = new System.Drawing.Size(40, 40);
			this.label36.TabIndex = 41;
			this.label36.Text = "6";
			this.label36.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label37
			// 
			this.label37.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label37.Location = new System.Drawing.Point(1104, 195);
			this.label37.Name = "label37";
			this.label37.Size = new System.Drawing.Size(40, 40);
			this.label37.TabIndex = 40;
			this.label37.Text = "5";
			this.label37.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label38
			// 
			this.label38.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label38.Location = new System.Drawing.Point(1104, 155);
			this.label38.Name = "label38";
			this.label38.Size = new System.Drawing.Size(40, 40);
			this.label38.TabIndex = 39;
			this.label38.Text = "4";
			this.label38.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label39
			// 
			this.label39.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label39.Location = new System.Drawing.Point(1104, 115);
			this.label39.Name = "label39";
			this.label39.Size = new System.Drawing.Size(40, 40);
			this.label39.TabIndex = 38;
			this.label39.Text = "3";
			this.label39.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label40
			// 
			this.label40.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label40.Location = new System.Drawing.Point(1104, 75);
			this.label40.Name = "label40";
			this.label40.Size = new System.Drawing.Size(40, 40);
			this.label40.TabIndex = 37;
			this.label40.Text = "2";
			this.label40.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label41
			// 
			this.label41.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label41.Location = new System.Drawing.Point(1104, 35);
			this.label41.Name = "label41";
			this.label41.Size = new System.Drawing.Size(40, 40);
			this.label41.TabIndex = 36;
			this.label41.Text = "1";
			this.label41.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// InfoBoardTextBox
			// 
			this.InfoBoardTextBox.BackColor = System.Drawing.Color.Black;
			this.InfoBoardTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.InfoBoardTextBox.ForeColor = System.Drawing.Color.White;
			this.InfoBoardTextBox.Location = new System.Drawing.Point(401, 477);
			this.InfoBoardTextBox.Multiline = true;
			this.InfoBoardTextBox.Name = "InfoBoardTextBox";
			this.InfoBoardTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.InfoBoardTextBox.Size = new System.Drawing.Size(357, 150);
			this.InfoBoardTextBox.TabIndex = 46;
			// 
			// EnemyAI
			// 
			this.EnemyAI.Interval = 2000;
			this.EnemyAI.Tick += new System.EventHandler(this.EnemyAI_Tick);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1184, 761);
			this.Controls.Add(this.InfoBoardTextBox);
			this.Controls.Add(this.label32);
			this.Controls.Add(this.label33);
			this.Controls.Add(this.label34);
			this.Controls.Add(this.label35);
			this.Controls.Add(this.label36);
			this.Controls.Add(this.label37);
			this.Controls.Add(this.label38);
			this.Controls.Add(this.label39);
			this.Controls.Add(this.label40);
			this.Controls.Add(this.label41);
			this.Controls.Add(this.label22);
			this.Controls.Add(this.label23);
			this.Controls.Add(this.label24);
			this.Controls.Add(this.label25);
			this.Controls.Add(this.label26);
			this.Controls.Add(this.label27);
			this.Controls.Add(this.label28);
			this.Controls.Add(this.label29);
			this.Controls.Add(this.label30);
			this.Controls.Add(this.label31);
			this.Controls.Add(this.label21);
			this.Controls.Add(this.label20);
			this.Controls.Add(this.label19);
			this.Controls.Add(this.label18);
			this.Controls.Add(this.label17);
			this.Controls.Add(this.label16);
			this.Controls.Add(this.label15);
			this.Controls.Add(this.label14);
			this.Controls.Add(this.label13);
			this.Controls.Add(this.label12);
			this.Controls.Add(this.label11);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.AcceptFleetButton);
			this.Controls.Add(this.TargetFieldLabel);
			this.Controls.Add(this.EnemyGameBoard);
			this.Controls.Add(this.PlayerGameBoard);
			this.Controls.Add(this.CreateFleetButton);
			this.MaximumSize = new System.Drawing.Size(1200, 800);
			this.MinimumSize = new System.Drawing.Size(1200, 800);
			this.Name = "Form1";
			this.Text = "Flota";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button CreateFleetButton;
		private System.Windows.Forms.Panel PlayerGameBoard;
		private System.Windows.Forms.Panel EnemyGameBoard;
		private System.Windows.Forms.Label TargetFieldLabel;
		private System.Windows.Forms.Button AcceptFleetButton;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.Label label20;
		private System.Windows.Forms.Label label21;
		private System.Windows.Forms.Label label22;
		private System.Windows.Forms.Label label23;
		private System.Windows.Forms.Label label24;
		private System.Windows.Forms.Label label25;
		private System.Windows.Forms.Label label26;
		private System.Windows.Forms.Label label27;
		private System.Windows.Forms.Label label28;
		private System.Windows.Forms.Label label29;
		private System.Windows.Forms.Label label30;
		private System.Windows.Forms.Label label31;
		private System.Windows.Forms.Label label32;
		private System.Windows.Forms.Label label33;
		private System.Windows.Forms.Label label34;
		private System.Windows.Forms.Label label35;
		private System.Windows.Forms.Label label36;
		private System.Windows.Forms.Label label37;
		private System.Windows.Forms.Label label38;
		private System.Windows.Forms.Label label39;
		private System.Windows.Forms.Label label40;
		private System.Windows.Forms.Label label41;
		private System.Windows.Forms.TextBox InfoBoardTextBox;
		private Timer EnemyAI;
	}
}

