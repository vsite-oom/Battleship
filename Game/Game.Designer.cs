using static System.Net.Mime.MediaTypeNames;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Vsite.Oom.Battleship.Game
{
    partial class Game
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        System.ComponentModel.IContainer components = null;

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
        void InitializeComponent()
        {
            strateskiGrid = new DataGridView();
            taktickiGrid = new DataGridView();
            pocetakButton = new Button();
            predajaButton = new Button();
            statusLabel = new TextBox();
            rezultatLabel = new TextBox();
            textStatus = new Label();
            textRezultat = new Label();
            ((System.ComponentModel.ISupportInitialize)(strateskiGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(taktickiGrid)).BeginInit();
            SuspendLayout();
            // 
            // strateskiGrid
            // 
            strateskiGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            strateskiGrid.GridColor = System.Drawing.Color.Black;
            strateskiGrid.Location = new System.Drawing.Point(24, 88);
            strateskiGrid.Name = "strateskiGrid";
            strateskiGrid.RowHeadersWidth = 62;
            strateskiGrid.Size = new System.Drawing.Size(357, 300);
            strateskiGrid.TabIndex = 0;
            // 
            // taktickiGrid
            // 
            taktickiGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            taktickiGrid.GridColor = System.Drawing.Color.Black;
            taktickiGrid.Location = new System.Drawing.Point(410, 88);
            taktickiGrid.Name = "taktickiGrid";
            taktickiGrid.RowHeadersWidth = 62;
            taktickiGrid.Size = new System.Drawing.Size(355, 300);
            taktickiGrid.TabIndex = 1;
            // 
            // pocetakButton
            // 
            pocetakButton.Location = new System.Drawing.Point(24, 41);
            pocetakButton.Name = "pocetakButton";
            pocetakButton.Size = new System.Drawing.Size(112, 34);
            pocetakButton.TabIndex = 2;
            pocetakButton.Text = "pocni igru";
            pocetakButton.UseVisualStyleBackColor = true;
            pocetakButton.Click += new System.EventHandler(pocetakButton_Click);
            // 
            // predajaButton
            // 
            predajaButton.Location = new System.Drawing.Point(653, 41);
            predajaButton.Name = "predajaButton";
            predajaButton.Size = new System.Drawing.Size(112, 34);
            predajaButton.TabIndex = 3;
            predajaButton.Text = "predaj igru";
            predajaButton.UseVisualStyleBackColor = true;
            // 
            // statusLabel
            // 
            statusLabel.Location = new System.Drawing.Point(237, 44);
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new System.Drawing.Size(144, 31);
            statusLabel.TabIndex = 4;
            // 
            // rezultatLabel
            // 
            rezultatLabel.Location = new System.Drawing.Point(525, 44);
            rezultatLabel.Name = "rezultatLabel";
            rezultatLabel.Size = new System.Drawing.Size(122, 31);
            rezultatLabel.TabIndex = 5;
            // 
            // textStatus
            // 
            textStatus.AutoSize = true;
            textStatus.Location = new System.Drawing.Point(142, 47);
            textStatus.Name = "textStatus";
            textStatus.Size = new System.Drawing.Size(98, 25);
            textStatus.TabIndex = 6;
            textStatus.Text = "status igre:";
            // 
            // textRezultat
            // 
            textRezultat.AutoSize = true;
            textRezultat.Location = new System.Drawing.Point(410, 46);
            textRezultat.Name = "textRezultat";
            textRezultat.Size = new System.Drawing.Size(109, 25);
            textRezultat.TabIndex = 8;
            textRezultat.Text = "rezultat igre:";
            // 
            // Game
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(800, 450);
            Controls.Add(textRezultat);
            Controls.Add(textStatus);
            Controls.Add(rezultatLabel);
            Controls.Add(statusLabel);
            Controls.Add(predajaButton);
            Controls.Add(pocetakButton);
            Controls.Add(taktickiGrid);
            Controls.Add(strateskiGrid);
            Name = "Game";
            Text = "Borba brodova";
            ((System.ComponentModel.ISupportInitialize)(strateskiGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(taktickiGrid)).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        DataGridView strateskiGrid;
        DataGridView taktickiGrid;
        Button pocetakButton;
        Button predajaButton;
        TextBox statusLabel;
        TextBox rezultatLabel;
        Label textStatus;
        Label textRezultat;
    }
}
