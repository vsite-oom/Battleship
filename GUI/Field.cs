using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vsite.Oom.Battleship.Model;

namespace GUI
{
	class Field : Button
	{

		 int row;
		 int column;

		public Field(int row, int column)
		{
			this.row = row;
			this.column = column;
		}

		public Square Position()
		{

			return new Square(row, column);

		}

	}
}
