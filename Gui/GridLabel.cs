namespace BattleshipGUI {
    internal class GridLabel : System.Windows.Forms.Label {
        public int Row, Column;

        public GridLabel(int row, int column) {
            Row = row;
            Column = column;
        }
    }
}