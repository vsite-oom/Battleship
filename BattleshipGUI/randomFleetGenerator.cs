using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using modelNmspc = Vsite.Oom.Battleship.Model;

namespace BattleshipGUI
{   
    public partial class randomFleetGenerator : Form
    {
        private modelNmspc.Grid gr = new modelNmspc.Grid(10, 10);
        private modelNmspc.fleet fl = new modelNmspc.fleet();
        private Random random = new Random();
        public randomFleetGenerator()
        {
            InitializeComponent();
        }
        private void Grid_Paint(object sender, PaintEventArgs e)
        {
            while (CreateGrid() == 404) {
                gr = new modelNmspc.Grid(10, 10);                             //petlja se vrti dok createGrid uspješno ne izgenerira valjanu flotu
                fl = new modelNmspc.fleet();
                Console.WriteLine("!fail!");
            }
            DrawGrid();
        }
        private bool CheckIfSquareIsInFleet(int row, int column)
        {
            var shipsList = fl.Ships;
            using (var sequenceEnum1 = shipsList.GetEnumerator())
            {
                while (sequenceEnum1.MoveNext())
                {
                    var ship = sequenceEnum1.Current;
                    var squares = ship.squares;
                    using (var sequenceEnum2 = squares.GetEnumerator())
                    {
                        while (sequenceEnum2.MoveNext())
                        {
                            var square = sequenceEnum2.Current;
                            if(square.column == column && square.row == row) {
                                return true; }
                        }
                    }
                }
            }
            return false;
        }
        //private void PrintOutFleetPositions()
        //{
        //    var shipsList = fl.Ships;
        //    using (var sequenceEnum1 = shipsList.GetEnumerator())
        //    {
        //        while (sequenceEnum1.MoveNext())                                               //debugging purpose only
        //        {
        //            var ship = sequenceEnum1.Current;
        //            var squares = ship.squares;
        //            using (var sequenceEnum2 = squares.GetEnumerator())
        //            {
        //                while (sequenceEnum2.MoveNext())
        //                {
        //                    var square = sequenceEnum2.Current;
        //                    Console.WriteLine("col = " + square.column + " row=" + square.row);

        //                }
        //                Console.WriteLine("ship over---!");
        //            }
        //        }
        //    }
        //}
        private void DrawGrid()
        {  
            Graphics graphobject = grid.CreateGraphics();                      

            Brush blueBrush = new SolidBrush(Color.Blue);
            Pen bluePen = new Pen(blueBrush, 8);
            Brush grayBrush = new SolidBrush(Color.Gray);
            Pen grayPen = new Pen(grayBrush, 8);
            int column = 0;
            int row = 0;
            /*PrintOutFleetPositions(); */                                               //debugging purpose only
            for (int i = 0; i != 500; i += 50)
            {
                for (int j = 0; j != 500; j += 50)
                {
                    if (CheckIfSquareIsInFleet(row,column))
                    {
                        graphobject.FillRectangle(blueBrush, j, i, 49, 49);           //crta plava ili siva polja ovisno da li se nalaze u fleet-u
                    }
                    else
                    {
                        graphobject.FillRectangle(grayBrush, j, i, 49, 49);
                    }
                    ++column;
                }
                column = 0;
                ++row;
            }
                      
          
        }

        private void Generate_Click(object sender, EventArgs e)
        {
            gr = new modelNmspc.Grid(10, 10);
            fl = new modelNmspc.fleet();
            while (CreateGrid() == 404)
            {                                                 //petlja se vrti dok createGrid uspješno ne izgenerira valjanu flotu
                gr = new modelNmspc.Grid(10, 10);
                fl = new modelNmspc.fleet();
                Console.WriteLine("!fail!");
            }
            DrawGrid();
        }

        private int CreateGrid()
        {
            int trigger = 1;
            int shipLength = 5;
            var availableSquares = gr.GetAvailablePlacements(shipLength);                                   //dohvaćanje mogućih pozicija na gridu za ship duljine 5
            if (ChooseRandomShipPositions_AddToFleet_Eliminate(availableSquares) == 404) { return 404; };  //uzima random poziciju od mogućih pozicija, dodaje u flotu, te eliminira iz grida
            for (int i = 0; i < 9; ++i)
            {
                if (trigger < 3 ) { shipLength = 4; }
                if (trigger <6  && trigger >= 3 ){ shipLength = 3; }
                if (trigger <= 10 && trigger >=6 ) { shipLength = 2; }


                availableSquares = gr.GetAvailablePlacements(shipLength);
                if (ChooseRandomShipPositions_AddToFleet_Eliminate(availableSquares) == 404) { return 404; };
                ++trigger;
            }
            return 1;
        }

        private int ChooseRandomShipPositions_AddToFleet_Eliminate(IEnumerable<IEnumerable<modelNmspc.Square>> availablePositions)
        {
            
            int result = availablePositions.Count();
            int randomPosition = random.Next(0, result);
            int counter = 0;
            List<modelNmspc.Square> squaresToAddAndElim = null;
            IEnumerable<modelNmspc.Square> ship = null;
            using (var sequenceEnum = availablePositions.GetEnumerator())
            {
                while (sequenceEnum.MoveNext())                             //prolazak kroz validne pozicije,
                {                                                          // te random odabir jedne od njih
                    if (counter == randomPosition)
                    {
                        ship = sequenceEnum.Current;
                    }
                    ++counter;
                }
            }
            int initCounter = 0;
            if (ship == null)
            {
               return 404;
            }
            
                using (var sequenceEnum = ship.GetEnumerator())
                {
                    while (sequenceEnum.MoveNext())
                    {
                        if (initCounter == 0)                                  //prolazak kroz svaki square  u izabranoj validnoj poziciji
                        {
                            var square = sequenceEnum.Current;
                            squaresToAddAndElim = new List<modelNmspc.Square> { new modelNmspc.Square(square.row, square.column) };
                            initCounter = 1;
                        }
                        else
                        {
                            var square = sequenceEnum.Current;
                            squaresToAddAndElim.Add(square);
                        }

                    }
                }
                //using (var sequenceEnum = squaresToAddAndElim.GetEnumerator())
                //{
                //    while (sequenceEnum.MoveNext())
                //    {                                                                         //used for debugging only

                //            var square = sequenceEnum.Current;
                //        var c = square.column;
                //        var r = square.row;
                //    }
                //}
                fl.addShip(squaresToAddAndElim);                                                     //dodaje u flotu sve shipove koji su random odabrani
                modelNmspc.squareTerminator terminator = new modelNmspc.squareTerminator(gr.Rw,gr.Cl);       //eliminira iz grida sve okolne pozicije odabranih shipova
                var toElim = terminator.ToEliminate(squaresToAddAndElim);
                gr.eliminateSquares(toElim);                                                         //eliminira iz grida sve square-ove odabranih shipova
                //Console.WriteLine("eliminirano "+toElim.Count());                                 //used for debugging
                return 1;
            }
        }

}
