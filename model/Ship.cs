﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace model;

public class Ship
{
    public Ship(IEnumerable<Square> squares)
    {
        Squares = squares;
    }

    public readonly IEnumerable<Square> Squares;

    public bool Contains(int row, int column)
    {
        return Squares.FirstOrDefault(sq => sq.Row == row && sq.Column == column) != null;
    }
}