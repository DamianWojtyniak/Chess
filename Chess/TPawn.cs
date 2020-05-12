using System.Collections.Generic;
using System.Linq;
namespace Chess
{

    public class TPawn : TPiece
    {
        public TPawn(TPlayer player) : base(player)
        {
            ImageId = 6;
            Value = 1;
        }
        public override List<TCell> GetFreeCells()
        {
            var cells = new List<TCell>();
            var dir = Player == Player.Board.WhitePlayer ? -1 : 1;
            var cell = Cell.GetNeighbour(1, dir);

            if (cell != null && cell.Piece != null)
                cells.Add(cell);
            cell = Cell.GetNeighbour(-1, dir);
            if (cell != null && cell.Piece != null)
                cells.Add(cell);
            cell = Cell.GetNeighbour(0, dir);

            if (cell?.Piece == null)
            {
                cells.Add(cell);
                if (MoveCount == 0)
                {
                    cell = cell.GetNeighbour(0, dir);
                    if (cell.Piece == null)
                        cells.Add(cell);
                }
            }
            if (MoveCount > 0)
            {
                var lastMove = Player.Board.Moves.Last();
                if (lastMove.Piece is TPawn && lastMove.Piece.MoveCount == 1)
                {
                    if (lastMove.StopCell == Cell.GetNeighbour(-1, 0))
                        cells.Add(Cell.GetNeighbour(-1, dir));
                    if (lastMove.StopCell == Cell.GetNeighbour(1, 0))
                        cells.Add(Cell.GetNeighbour(1, dir));
                }
            }
            return cells;
        }
    }
}
