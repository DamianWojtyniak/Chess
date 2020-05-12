using System.Collections.Generic;

namespace Chess
{
    class TQueen : TPiece
    {
        public TQueen(TPlayer player) : base(player)
        {
            ImageId = 8;
            Value = 10;
        }
        public override List<TCell> GetFreeCells()
        {
            var cells = new List<TCell>();
            cells.AddRange(GetFreeCellsFromDir(1, 0));
            cells.AddRange(GetFreeCellsFromDir(0, 1));
            cells.AddRange(GetFreeCellsFromDir(-1, 0));
            cells.AddRange(GetFreeCellsFromDir(0, -1));
            cells.AddRange(GetFreeCellsFromDir(1, 1));
            cells.AddRange(GetFreeCellsFromDir(-1, 1));
            cells.AddRange(GetFreeCellsFromDir(-1, -1));
            cells.AddRange(GetFreeCellsFromDir(1, -1));
            return cells;
        }
    }
}
