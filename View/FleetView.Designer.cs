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
            this.play = new System.Windows.Forms.Button();
            this.endGame = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.newGame = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // CreateFleet
            // 
            this.CreateFleet.BackColor = System.Drawing.Color.LemonChiffon;
            this.CreateFleet.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.CreateFleet.Location = new System.Drawing.Point(34, 12);
            this.CreateFleet.Name = "CreateFleet";
            this.CreateFleet.Size = new System.Drawing.Size(80, 28);
            this.CreateFleet.TabIndex = 0;
            this.CreateFleet.Text = "Create fleet";
            this.CreateFleet.UseVisualStyleBackColor = false;
            this.CreateFleet.Click += new System.EventHandler(this.CreateFleet_Click);
            // 
            // play
            // 
            this.play.BackColor = System.Drawing.Color.LemonChiffon;
            this.play.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.play.Location = new System.Drawing.Point(147, 12);
            this.play.Name = "play";
            this.play.Size = new System.Drawing.Size(80, 28);
            this.play.TabIndex = 1;
            this.play.Text = "Play";
            this.play.UseVisualStyleBackColor = false;
            this.play.Visible = false;
            this.play.Click += new System.EventHandler(this.play_ClickAsync);
            // 
            // endGame
            // 
            this.endGame.BackColor = System.Drawing.Color.LemonChiffon;
            this.endGame.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.endGame.Location = new System.Drawing.Point(259, 12);
            this.endGame.Name = "endGame";
            this.endGame.Size = new System.Drawing.Size(80, 28);
            this.endGame.TabIndex = 2;
            this.endGame.Text = "End game";
            this.endGame.UseVisualStyleBackColor = false;
            this.endGame.Visible = false;
            this.endGame.Click += new System.EventHandler(this.endGame_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(452, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 17);
            this.label1.TabIndex = 3;
            this.label1.Visible = false;
            // 
            // newGame
            // 
            this.newGame.BackColor = System.Drawing.Color.LemonChiffon;
            this.newGame.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.newGame.Location = new System.Drawing.Point(34, 12);
            this.newGame.Name = "newGame";
            this.newGame.Size = new System.Drawing.Size(80, 28);
            this.newGame.TabIndex = 4;
            this.newGame.Text = "New game";
            this.newGame.UseVisualStyleBackColor = false;
            this.newGame.Visible = false;
            this.newGame.Click += new System.EventHandler(this.newGame_Click);
            // 
            // FleetView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Pink;
            this.ClientSize = new System.Drawing.Size(1087, 539);
            this.Controls.Add(this.newGame);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.endGame);
            this.Controls.Add(this.play);
            this.Controls.Add(this.CreateFleet);
            this.Name = "FleetView";
            this.Text = "FleetView";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button CreateFleet;
        private System.Windows.Forms.Button play;
        private System.Windows.Forms.Button endGame;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button newGame;
    }
}