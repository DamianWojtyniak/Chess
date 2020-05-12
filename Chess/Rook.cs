using System.Collections.Generic;

namespace Chess
{
    public class Rook : TPiece
    {
        public Rook(TPlayer player) : base(player)
        {
            ImageId = 10;
            Value = 5;
        }
        public override List<TCell> GetFreeCells()
        {
            var cells = new List<TCell>();
            cells.AddRange(GetFreeCellsFromDir(1, 0));
            cells.AddRange(GetFreeCellsFromDir(0, 1));
            cells.AddRange(GetFreeCellsFromDir(-1, 0));
            cells.AddRange(GetFreeCellsFromDir(0, -1));
            return cells;
        }
    }
}
