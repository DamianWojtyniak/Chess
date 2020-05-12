using System.Collections.Generic;

namespace Chess
{
    class Bishop : TPiece
    {
        public Bishop(TPlayer player) : base(player)
        {
            ImageId = 0;
            Value = 3;
        }

        public override List<TCell> GetFreeCells()
        {
            var cells = new List<TCell>();
            cells.AddRange(GetFreeCellsFromDir(1, 1));
            cells.AddRange(GetFreeCellsFromDir(-1, 1));
            cells.AddRange(GetFreeCellsFromDir(-1, -1));
            cells.AddRange(GetFreeCellsFromDir(1, -1));
            return cells;
        }
    }
}
