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
    public partial class ShipCreatorControl : UserControl
    {
        public int CurrentShipLength { get => CurrentBtnIndex + 1; }
        private Postavke ParentPostavke;
        private int CurrentBtnIndex = -1;
        private Button[] buttonArray;

        public ShipCreatorControl(Postavke parentPostavke) : this(parentPostavke, 1)
        {

        }

        public ShipCreatorControl(Postavke parentPostavke, int length)
        {
            InitializeComponent();

            ParentPostavke = parentPostavke;

            buttonArray = new[]
            {
                btnFirstField,
                btnSecondField,
                btnThirdField,
                btnFourthField,
                btnFifthField
            };
            for(int i = 0; i < length && i < 5;i++)
            {
                dodajPoljeBroda();
            }
        }

        private void dodajPoljeBroda()
        {
            if (CurrentShipLength < 5)
            {
                if (ParentPostavke.imaLiMjestaNaGridu(true))
                {
                    CurrentBtnIndex++;
                    buttonArray[CurrentBtnIndex].Visible = true;
                }
            }
            else
            {
                MessageBox.Show("Brod ne može imati više od 5 polja.", "Neuspjelo dodavanje polja broda");
            }
        }

        private void makniPoljeBroda()
        {
            if (CurrentShipLength > 1)
            {
                buttonArray[CurrentBtnIndex].Visible = false;
                CurrentBtnIndex--;
            }
            else
            {
                MessageBox.Show("Brod ne može imati manje od 1 polja.", "Neuspjelo micanje polja broda");
            }
        }

        private void btnPlus_Click(object sender, EventArgs e)
        {
            dodajPoljeBroda();
        }

        private void btnMinus_Click(object sender, EventArgs e)
        {
            makniPoljeBroda();
        }

        public override string ToString()
        {
            return CurrentShipLength.ToString();
        }
    }
}
