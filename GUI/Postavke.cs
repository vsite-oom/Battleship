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
    public partial class Postavke : Form
    {
        private int BrojStupaca;
        private int BrojRedaka;
        private Eliminator PostavljanjeFlote;
        private List<ShipCreatorControl> Brodovi = new List<ShipCreatorControl>();
        
        private string BrodoviZaSpremiti { get => string.Join(",", Brodovi);
    }

        private int BrojZauzetihPolja { get => Brodovi.Sum(br => br.CurrentShipLength); }

        public Postavke()
        {
            InitializeComponent();
            cBoxFlota.DisplayMember = "Description";
            cBoxFlota.ValueMember = "Value";
            cBoxFlota.DataSource = Enum.GetValues(typeof(Eliminator))
                .Cast<Enum>()
                .Select(value => new
                {
                    (Attribute.GetCustomAttribute(value.GetType().GetField(value.ToString()), typeof(DescriptionAttribute)) as DescriptionAttribute).Description,
                    value
                })
                .OrderBy(item => item.value)
                .ToList();

            int i = cBoxRedak.FindString(Properties.Settings.Default.BrojRedaka.ToString());
            cBoxRedak.SelectedIndex = i;

            i = cBoxStupac.FindString(Properties.Settings.Default.BrojStupaca.ToString());
            cBoxStupac.SelectedIndex = i;

            cBoxFlota.SelectedValue = (Eliminator)Enum.Parse(typeof(Eliminator), Properties.Settings.Default.PostavljanjeFlote.ToString());

            try
            {
                Properties.Settings.Default.ListaBrodova.Split(',').Select(int.Parse).ToList().ForEach(brod => dodajBrod(brod));
            }
            catch
            {
                new List<int>() { 4, 3, 2 }.ForEach(brod => dodajBrod(brod));
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Želite li spremiti postavke?", "Spremi", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                spremi();
            }
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            if(Properties.Settings.Default.BrojStupaca != BrojStupaca
                || Properties.Settings.Default.BrojRedaka != BrojRedaka
                || !Properties.Settings.Default.PostavljanjeFlote.Equals(PostavljanjeFlote.ToString())
                || !Properties.Settings.Default.ListaBrodova.Equals(BrodoviZaSpremiti))
            {
                DialogResult dialogResult = MessageBox.Show("Niste spremili postavke, želite li ih spremiti prije izlaska?", "Spremi", MessageBoxButtons.YesNoCancel);
                if (dialogResult == DialogResult.Yes)
                {
                    spremi();
                    Close();
                }
                else if (dialogResult == DialogResult.No)
                {
                    Close();
                }
            }
            else
            {
                Close();
            }
        }
        private void spremi()
        {
            Properties.Settings.Default.BrojStupaca = BrojStupaca;
            Properties.Settings.Default.BrojRedaka = BrojRedaka;
            Properties.Settings.Default.PostavljanjeFlote = PostavljanjeFlote.ToString();
            Properties.Settings.Default.ListaBrodova = BrodoviZaSpremiti;

            Properties.Settings.Default.Save();
        }

        private void micanjeViskaBrodova()
        {
            while (!imaLiMjestaNaGridu(false))
                makniBrod();
        }

        private void cBoxRedak_SelectedIndexChanged(object sender, EventArgs e)
        {
            BrojRedaka = Int32.Parse(cBoxRedak.Text);
            micanjeViskaBrodova();
        }

        private void cBoxStupac_SelectedIndexChanged(object sender, EventArgs e)
        {
            BrojStupaca = Int32.Parse(cBoxStupac.Text);
            micanjeViskaBrodova();
        }

        private void cBoxFlota_SelectedIndexChanged(object sender, EventArgs e)
        {
            PostavljanjeFlote = (Eliminator)cBoxFlota.SelectedValue;
            micanjeViskaBrodova();
        }

        public bool imaLiMjestaNaGridu(bool prikaziGresku)
        {
            if(PostavljanjeFlote == Eliminator.SimpleSquareEliminator && BrojZauzetihPolja * 2 > (BrojRedaka * BrojStupaca))
            {
                if(prikaziGresku)
                    MessageBox.Show("Nije moguće dodati još polja za brodove. Potrebno je imati barem pola polja slobodno.", "Nedovoljno mjesta na gridu");
                return false;
            }
            else if(PostavljanjeFlote == Eliminator.SurroundingSquareEliminator && BrojZauzetihPolja * 5 > (BrojRedaka * BrojStupaca))
            {
                if (prikaziGresku)
                    MessageBox.Show("Nije moguće dodati još polja za brodove. Kada se brodovi ne smiju dodirivati brodovi smiju zauzimati najviše petinu svih polja.", "Nedovoljno mjesta na gridu");
                return false;
            }
            return true;
        }

        private void changeFormHeight(int height)
        {
            Size newSize = new Size(this.Size.Width, height);

            this.MaximumSize = newSize;
            this.Size = newSize;
            this.MinimumSize = newSize;
        }

        private void btnMakniBrod_Click(object sender, EventArgs e)
        {
            makniBrod();
        }

        private void makniBrod()
        {
            if (Brodovi.Count > 1)
            {
                ShipCreatorControl shipForRemove = Brodovi[Brodovi.Count - 1];
                Brodovi.RemoveAt(Brodovi.Count - 1);

                this.Controls.Remove(shipForRemove);

                shipForRemove.Dispose();

                changeFormHeight(this.Size.Height - 43);
            }
            else
            {
                MessageBox.Show("Morate imati najmanje 1 brod.", "Flota");
            }
        }

        private void btnDodajBrod_Click(object sender, EventArgs e)
        {
            dodajBrod(1);
        }

        private void dodajBrod(int length)
        {
            changeFormHeight(this.Size.Height + 43);
            if (imaLiMjestaNaGridu(true))
            {
                ShipCreatorControl newShip = new ShipCreatorControl(this, length)
                {
                    Location = new Point(8, this.Height - 270)
                };

                this.Controls.Add(newShip);

                Brodovi.Add(newShip);
            }
        }
    }
}
