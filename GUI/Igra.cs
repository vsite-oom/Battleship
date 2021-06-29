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

namespace GUI
{
    public partial class Igra : Form
    {
        private readonly int Redci;
        private readonly int Stupci;
        private readonly Eliminator PostavljanjeFlote;
        private readonly Gunnery gunnery;
        private readonly Fleet mojaFlota;
        private readonly Fleet protivnickaFlota;
        private readonly ISquareEliminate eliminator;

        private readonly string mojaFlotaPrefix = "btnMojaFlota";
        private readonly string protivnickaFlotaPrefix = "btnProtivnickaFlota";

        public Igra(int redci, int stupci, string postavljenjeFlote, List<int> listaBrodova)
        {
            InitializeComponent();

            Properties.Settings.Default.Odigrano += 1;
            Properties.Settings.Default.Save();

            Redci = redci;
            Stupci = stupci;
            PostavljanjeFlote = (Eliminator)Enum.Parse(typeof(Eliminator), postavljenjeFlote);

            Size s = new Size(2 * Stupci * 40 + 100, Redci * 40 + 100);
            this.Size = this.MaximumSize = this.MinimumSize = s;

            int nextGridPositionX = kreirajGumbeZaGridGadjanja(20,10);

            kreirajGumbeZaGridMojihBrodova(nextGridPositionX + 45, 10);

            if (PostavljanjeFlote == Eliminator.SimpleSquareEliminator)
                eliminator = new SimpleSquareEliminator();
            else
                eliminator = new SurroundingSquareEliminator(redci,stupci);

            var shipwright = new Shipwright(redci, stupci, listaBrodova, eliminator);
            mojaFlota = shipwright.CreateFleet();
            protivnickaFlota = shipwright.CreateFleet();

            oznaciMojuFlotu();

            gunnery = new Gunnery(redci, stupci, listaBrodova, eliminator);
        }

        private int kreirajGumbeZaGridGadjanja(int xPoc, int yPoc)
        {
            return kreirajGumbeZaGrid(xPoc, yPoc, true);
        }


        private void kreirajGumbeZaGridMojihBrodova(int xPoc, int yPoc) 
        {
            kreirajGumbeZaGrid(xPoc, yPoc, false);
        }

        private int kreirajGumbeZaGrid(int xPoc, int yPoc, bool protivnickaFlota)
        {
            Point nextButtonPosition = new Point(xPoc, yPoc);
            Button newButton;
            for (int i=0; i< Redci; i++)
            {
                nextButtonPosition.X = xPoc;
                for (int j=0; j<Stupci; j++)
                {
                    newButton = new Button
                    {
                        Location = nextButtonPosition,
                        Text = ((char)(65 + j)).ToString() + (i + 1),
                        Size = new Size(35, 35)
                    };

                    if (protivnickaFlota)
                    {
                        newButton.Click += onClick;
                        newButton.Name = protivnickaFlotaPrefix + j + i;
                    }
                    else
                    {
                        newButton.Name = mojaFlotaPrefix + j + i;
                    }
                    
                    this.Controls.Add(newButton);

                    nextButtonPosition.X += 40;
                }
                nextButtonPosition.Y += 40;
            }
            return nextButtonPosition.X;
        }

        private void onClick(object sender, EventArgs e) 
        {
            var button = (Button)sender;
            button.Click -= onClick;

            var position = button.Text;
            int col = position[0] - 65;
            int row = position[1] - 49;

            Square mojaMeta = new Square(row,col);
            HitResult mojRezultat = protivnickaFlota.Hit(mojaMeta);

            obradiGadjanjeLogic(protivnickaFlotaPrefix, protivnickaFlota, mojaMeta, button, mojRezultat);

            Square sljedecaMeta = gunnery.NextTarget();
            HitResult protivnickiRezultat = mojaFlota.Hit(sljedecaMeta);
            gunnery.ProcessShootingResult(protivnickiRezultat);

            Button btnMojaFlota = nadjiGumbZaPolje(mojaFlotaPrefix, sljedecaMeta.Row, sljedecaMeta.Column);
            obradiGadjanjeLogic(mojaFlotaPrefix, mojaFlota ,sljedecaMeta, btnMojaFlota, protivnickiRezultat);
        }

        private void obradiGadjanjeLogic(string buttonPrefix, Fleet f, Square sq, Button btn, HitResult hit)
        {
            if (hit == HitResult.Sunken)
                obradiPotapanjeGUI(buttonPrefix, f, sq);
            else
                obradiGadjanjeGUI(btn, hit);
        }

        private Button nadjiGumbZaPolje(string buttonPrefix, int redak, int stupac) 
        {
            return (Button)this.Controls.Find(buttonPrefix + stupac + redak, true).First();
        }

        private void obradiPotapanjeGUI(string buttonPrefix, Fleet f, Square sq)
        {
            Ship s = f.shipOnSquare(sq);
            var squaresToEliminate = eliminator.ToEliminate(s.Squares);
            foreach(Square poljeZaEliminiaciju in squaresToEliminate)
            {
                var button = nadjiGumbZaPolje(buttonPrefix, poljeZaEliminiaciju.Row, poljeZaEliminiaciju.Column);
                button.BackColor = s.Squares.Contains(poljeZaEliminiaciju) ? Color.Red : Color.White;
                button.Click -= onClick;
            }
            f.RemainingShipNumber--;
            if(f.RemainingShipNumber == 0)
            {
                bool poraz = buttonPrefix.Equals(mojaFlotaPrefix);
                spremiRezultat(poraz);

                string dialogText = poraz ? "Igra je završila, nažalost ste izgubili. Želte li igrati ponovno?" : "Čestitam, pobjedili ste suparničku flotu. Želte li igrati ponovno?";
                
                DialogResult dialogResult = MessageBox.Show(dialogText, "Igra je završila.", MessageBoxButtons.YesNo);
                
                if (dialogResult == DialogResult.Yes)
                {
                    this.DialogResult = DialogResult.Yes;
                    this.Close();
                }
                else if (dialogResult == DialogResult.No)
                {
                    this.Close();
                }
            }
        }

        private void spremiRezultat(bool poraz)
        {
            
            if (poraz)
                Properties.Settings.Default.Porazi += 1;
            else
                Properties.Settings.Default.Pobjede += 1;

            Properties.Settings.Default.Save();

        }


        private void obradiGadjanjeGUI(Button btn, HitResult hit)
        {
            //sunken obrađen iznad
            if (hit == HitResult.Hit)
            {
                btn.BackColor = Color.Orange;
            }
            else
            {
                btn.BackColor = Color.White;
            }
        }

        private void oznaciMojuFlotu()
        {
            mojaFlota.Ships.ToList().ForEach(ship =>
            {
                ship.Squares.ToList().ForEach(square =>
                {
                    nadjiGumbZaPolje(mojaFlotaPrefix, square.Row, square.Column).BackColor = Color.Blue;
                });
            });
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Jeste li sigurni da želite napusitit igru?", "Izlaz", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}
