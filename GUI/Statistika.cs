using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class Statistika : Form
    {
        private int odigrano;
        private int pobjede;
        private int porazi;
        private int prekinuto;

        public Statistika()
        {
            InitializeComponent();

            odigrano = Properties.Settings.Default.Odigrano;
            pobjede = Properties.Settings.Default.Pobjede;
            porazi = Properties.Settings.Default.Porazi;
            prekinuto = odigrano - pobjede - porazi;

            lblOdigranoBroj.Text = odigrano.ToString();
            lblPobjedeBroj.Text = pobjede.ToString();
            lblPoraziBroj.Text = porazi.ToString();
            lblPrekinutoBroj.Text = prekinuto.ToString();
        }

        private void btnPovratak_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
