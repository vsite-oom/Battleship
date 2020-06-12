using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vsite.Oom.Battleship.Model;

namespace Vsite.Oom.Battleship.Gui
{
    public partial class GuiForm : Form
    {
        public GuiForm()
        {
            InitializeComponent();
        }

        public void InitGrid()
        {
            rules = RulesSingleton.Instance;
            Shipwright sw = new Shipwright(rules.Rows,rules.Columns);
            
            playerFleet = sw.CreateFleet(rules.ShipLengths);
            computerFleet = sw.CreateFleet(rules.ShipLengths);

            DrawPanels(ref playerFleet, ref computerFleet);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            playerPanel.deployed = false;
            computerPanel.deployed = false;
            InitGrid();
        }

        public void DrawPanels(ref Fleet playerFleet, ref Fleet computerFleet)
        {
            playerPanel.ClearAll();
            computerPanel.ClearAll();

            playerPanel.Location = new Point(50, 50);
            playerPanel.Size = new Size(400, 400);
            playerPanel.BorderStyle = BorderStyle.None;
            playerPanel.InitMembers(ref playerFleet, ref rules);
            this.Controls.Add(playerPanel);

            computerPanel.BorderStyle = BorderStyle.None;
            computerPanel.Location = new Point(500, 50);
            computerPanel.Size = new Size(400, 400);
            computerPanel.InitMembers(ref computerFleet, ref rules);
            this.Controls.Add(computerPanel);

            Refresh();
        }

        Fleet playerFleet = new Fleet();
        Fleet computerFleet = new Fleet();
        GuiPanel playerPanel = new GuiPanel(true);
        GuiPanel computerPanel = new GuiPanel(false);
        RulesSingleton rules;
    }
}
