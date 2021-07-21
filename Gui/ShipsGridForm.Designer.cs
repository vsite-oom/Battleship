
namespace BattleshipGUI {
    partial class ShipsGridForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }



        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.Start_Button = new System.Windows.Forms.Button();
            this.stop_watch_Label = new System.Windows.Forms.Label();
            this.TimerLabel = new System.Windows.Forms.Timer(this.components);
            this.time_TextLabel = new System.Windows.Forms.Label();
            this.my_ships_GroupBox = new System.Windows.Forms.GroupBox();
            this.enemy_ships_GroupBox = new System.Windows.Forms.GroupBox();
            this.SuspendLayout();

            this.Start_Button.Font = new System.Drawing.Font("Georgia", 7.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Start_Button.Location = new System.Drawing.Point(648, 278);
            this.Start_Button.Margin = new System.Windows.Forms.Padding(4);
            this.Start_Button.Name = "Start_Button";
            this.Start_Button.Size = new System.Drawing.Size(200, 137);
            this.Start_Button.TabIndex = 0;
            this.Start_Button.Text = "Start the game";
            this.Start_Button.UseVisualStyleBackColor = true;
            this.Start_Button.Click += new System.EventHandler(this.PlaceFleetButton_Click);

            this.stop_watch_Label.AutoSize = true;
            this.stop_watch_Label.Font = new System.Drawing.Font("Georgia", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stop_watch_Label.Location = new System.Drawing.Point(701, 26);
            this.stop_watch_Label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.stop_watch_Label.Name = "stop_watch_Label";
            this.stop_watch_Label.Size = new System.Drawing.Size(79, 20);
            this.stop_watch_Label.TabIndex = 9;
            this.stop_watch_Label.Text = "00:00:00";

            this.TimerLabel.Enabled = true;
            this.TimerLabel.Tick += new System.EventHandler(this.GameTimer_Tick);

            this.time_TextLabel.AutoSize = true;
            this.time_TextLabel.Font = new System.Drawing.Font("Georgia", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.time_TextLabel.Location = new System.Drawing.Point(712, 1);
            this.time_TextLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.time_TextLabel.Name = "time_TextLabel";
            this.time_TextLabel.Size = new System.Drawing.Size(68, 25);
            this.time_TextLabel.TabIndex = 10;
            this.time_TextLabel.Text = "Time";
            this.time_TextLabel.Click += new System.EventHandler(this.time_TextLabel_Click);
            this.my_ships_GroupBox.Font = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.my_ships_GroupBox.Location = new System.Drawing.Point(16, 21);
            this.my_ships_GroupBox.Margin = new System.Windows.Forms.Padding(4);
            this.my_ships_GroupBox.Name = "my_fleet_GroupBox";
            this.my_ships_GroupBox.Padding = new System.Windows.Forms.Padding(4);
            this.my_ships_GroupBox.Size = new System.Drawing.Size(624, 562);
            this.my_ships_GroupBox.TabIndex = 11;
            this.my_ships_GroupBox.TabStop = false;
            this.my_ships_GroupBox.Text = "My Ships";
            this.my_ships_GroupBox.Enter += new System.EventHandler(this.my_fleet_GroupBox_Enter);

            this.enemy_ships_GroupBox.Font = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.enemy_ships_GroupBox.Location = new System.Drawing.Point(856, 21);
            this.enemy_ships_GroupBox.Margin = new System.Windows.Forms.Padding(4);
            this.enemy_ships_GroupBox.Name = "enemy_fleet_GroupBox";
            this.enemy_ships_GroupBox.Padding = new System.Windows.Forms.Padding(4);
            this.enemy_ships_GroupBox.Size = new System.Drawing.Size(635, 562);
            this.enemy_ships_GroupBox.TabIndex = 12;
            this.enemy_ships_GroupBox.TabStop = false;
            this.enemy_ships_GroupBox.Text = "Enemy Ships";
            this.enemy_ships_GroupBox.Enter += new System.EventHandler(this.enemyFleetGroupBox_Enter);

            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1513, 623);
            this.Controls.Add(this.enemy_ships_GroupBox);
            this.Controls.Add(this.my_ships_GroupBox);
            this.Controls.Add(this.time_TextLabel);
            this.Controls.Add(this.stop_watch_Label);
            this.Controls.Add(this.Start_Button);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FleetGridForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Battleship";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Start_Button;
        private System.Windows.Forms.Label stop_watch_Label;
        private System.Windows.Forms.Timer TimerLabel;
        private System.Windows.Forms.Label time_TextLabel;
        private System.Windows.Forms.GroupBox my_ships_GroupBox;
        private System.Windows.Forms.GroupBox enemy_ships_GroupBox;
    }
}

