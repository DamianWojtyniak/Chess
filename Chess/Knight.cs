using System.Collections.Generic;

namespace Chess
{
    public class Knight : TPiece
    {
        public Knight(TPlayer player): base (player)
        {
            ImageId = 4;
            Value = 3;
        }
        public override List<TCell> GetFreeCells()
        {
            var cells = new List<TCell>();
            cells.Add(Cell.GetNeighbour(2, -1));
            cells.Add(Cell.GetNeighbour(2, 1));
            cells.Add(Cell.GetNeighbour(-2, -1));
            cells.Add(Cell.GetNeighbour(-2, -1));
            cells.Add(Cell.GetNeighbour(-1, 2));
            cells.Add(Cell.GetNeighbour(1, 2));
            cells.Add(Cell.GetNeighbour(-1, -2));
            cells.Add(Cell.GetNeighbour(1, -2));
            return cells;
        }
    }
}
