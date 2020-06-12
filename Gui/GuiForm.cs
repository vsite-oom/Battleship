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

        public void DrawPanels(ref Fleet playerFleet, ref Fleet computerFleet)
        {
            playerPanel.ClearAll();
            computerPanel.ClearAll();

            playerPanel.Location = new Point(50, 50);
            playerPanel.Size = new Size(400, 400);
            playerPanel.BorderStyle = BorderStyle.None;
            playerPanel.InitMembers(ref computerFleet, ref rules, ref computerPanel, ref label1);
            this.Controls.Add(playerPanel);

            computerPanel.BorderStyle = BorderStyle.None;
            computerPanel.Location = new Point(500, 50);
            computerPanel.Size = new Size(400, 400);
            computerPanel.InitMembers(ref playerFleet, ref rules);
            this.Controls.Add(computerPanel);

            Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label1.Text = "";
            playerPanel.deployed = false;
            computerPanel.deployed = false;
            InitGrid();
        }

        private Fleet playerFleet = new Fleet();
        private Fleet computerFleet = new Fleet();
        private PanelPlayer playerPanel = new PanelPlayer();
        private PanelComputer computerPanel = new PanelComputer();
        private RulesSingleton rules;
    }
}
