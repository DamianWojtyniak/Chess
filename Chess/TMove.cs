namespace Chess
{
    public class TMove
    {
        public TPiece Piece;
        public TCell StartCell;
        public TCell StopCell;
        public TPiece Capture;

        public void Make()
        {
            if (Piece is TKing)
            {
                if (StopCell.X - StartCell.X == -2)
                {
                    var rook = StartCell.GetNeighbour(-4, 0).Piece;
                    rook.Cell = StopCell.GetNeighbour(1, 0);
                }
                if (StopCell.X - StartCell.X == 2)
                {
                    var rook = StartCell.GetNeighbour(3, 0).Piece;
                    rook.Cell = StopCell.GetNeighbour(-1, 0);
                }
            }
            Piece.Cell = StopCell;
            if (Capture != null)
            {
                Piece.Player.Enemy.Pieces.Remove(Capture);
            }
            Piece.MoveCount++;
            Piece.Player.Board.Moves.Add(this);
            Piece.Player.Board.ActivePlayer = Piece.Player.Board.ActivePlayer.Enemy;
        }

        public void UnMake()
        {
            if (Piece is TKing)
            {
                if (StopCell.X - StartCell.X == -2)
                {
                    var rook = StopCell.GetNeighbour(1, 0).Piece;
                    rook.Cell = StartCell.GetNeighbour(-4, 0);
                }
                if (StopCell.X - StartCell.X == 2)
                {
                    var rook = StopCell.GetNeighbour(-1, 0).Piece;
                    rook.Cell = StartCell.GetNeighbour(3, 0);
                }
            }
            Piece.MoveCount--;
            if (Capture != null)
                Piece.Player.Enemy.Pieces.Add(Capture);
            Piece.Cell = StartCell;
            Piece.Player.Board.Moves.Remove(this);
            Piece.Player.Board.ActivePlayer = Piece.Player.Board.ActivePlayer.Enemy;
        }
    }


}
