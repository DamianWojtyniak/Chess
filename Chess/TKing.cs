using System.Collections.Generic;
using System.Linq;

namespace Chess
{
    public class TKing : TPiece
    {
        public bool IsChecking;
        public TKing(TPlayer player) : base(player)
        {
            ImageId = 2;
            Value = 1000;
        }

        public override List<TCell> GetFreeCells()
        {
            List<TMove> allMoves = null;
            if (!IsChecking)
            {
                IsChecking = true;
                allMoves = Player.Enemy.GetAllMoves();
                IsChecking = false;
            }

            var cells = new List<TCell>();
            cells.Add(Cell.GetNeighbour(-1, 0));
            cells.Add(Cell.GetNeighbour(-1, -1));
            cells.Add(Cell.GetNeighbour(0, -1));
            cells.Add(Cell.GetNeighbour(1, -1));
            cells.Add(Cell.GetNeighbour(1, 0));
            cells.Add(Cell.GetNeighbour(1, 1));
            cells.Add(Cell.GetNeighbour(0, 1));
            cells.Add(Cell.GetNeighbour(-1, 1));

            if (allMoves != null)
            {
                for (int i = cells.Count - 1; i >= 0; i--)
                {
                    if (allMoves.Any(p => p.StopCell == cells[i]))
                    {
                        cells.RemoveAt(i);
                    }
                }
            }
            
            if (MoveCount == 0 && allMoves != null)
            {
                var rook = Cell.GetNeighbour(-4, 0).Piece;
                if (rook != null && rook.MoveCount == 0)
                {
                    var isValid = true;
                    for (int i = 0; i < 4; i++)
                    {
                        var cell = Cell.GetNeighbour(-i, 0);
                        if (i > 0 && cell.Piece != null)
                        {
                            isValid = false;
                            break;
                        }
                        if (allMoves.Any(x => x.StopCell == cell))
                        {
                            isValid = false;
                            break;
                        }
                    }
                    if (isValid)
                        cells.Add(Cell.GetNeighbour(-2, 0));
                }
                rook = Cell.GetNeighbour(3, 0).Piece;
                if (rook != null && rook.MoveCount == 0)
                {
                    var isValid = true;
                    for (int i = 0; i < 3; i++)
                    {
                        var cell = Cell.GetNeighbour(i, 0);
                        if (i > 0 && cell.Piece != null)
                        {
                            isValid = false;
                            break;
                        }
                        if (allMoves.Any(x => x.StopCell == cell))
                        {
                            isValid = false;
                            break;
                        }
                    }
                    if (isValid)
                        cells.Add(Cell.GetNeighbour(2, 0));
                }
            }
            return cells;
        }
    }
}
