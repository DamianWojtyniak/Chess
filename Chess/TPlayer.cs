using System;
using System.Collections.Generic;

namespace Chess
{
    public class TPlayer
    {
        public List<TPiece> Pieces = new List<TPiece>();
        public TBoard Board;
        public bool IsComputer;
        public TPlayer Enemy
        {
            get
            {
                if (this == Board.WhitePlayer)
                    return Board.BlackPlayer;
                else
                    return Board.WhitePlayer;
            }
        }
        public TPiece PieceAtCell(TCell cell)
        {
            foreach (var piece in Pieces)
            {
                if (piece.Cell == cell)
                    return piece;
            }
            return null;
        }
        public List<TMove> GetAllMoves()
        {
            var moves = new List<TMove>();
            foreach (var piece in Pieces)
            {
                moves.AddRange(piece.GetMoves());
            }
            return moves;
        }

        public static int SearchDepth = 4;
        public TMove BestMove;
        public int MaxiMin(int depth)
        {
            if (depth == 0)
                return Board.Evaluate();

            var allMoves = GetAllMoves();
            var max = -int.MaxValue;

            foreach (var move in allMoves)
            {
                move.Make();
                var score = Enemy.MiniMax(depth - 1);
                if (score > max)
                {
                    if (depth == SearchDepth)
                    {
                        BestMove = move;
                    }
                    max = score;
                }
                move.UnMake();
            }
            return max;
        }
        public int MiniMax(int depth)
        {
            if (depth == 0)
                return Board.Evaluate();

            var allMoves = GetAllMoves();
            var min = int.MaxValue;

            foreach (var move in allMoves)
            {
                move.Make();
                var score = Enemy.MiniMax(depth - 1);
                if (score < min)
                {
                    if (depth == SearchDepth)
                    {
                        BestMove = move;
                    }
                    min = score;
                }
                move.UnMake();
            }
            return min;
        }

        public int AlphaBetaMax(int depth, int max, int min)
        {
            if (depth == 0)
                return Board.Evaluate();

            var allMoves = GetAllMoves();

            foreach (var move in allMoves)
            {
                move.Make();
                var score = Enemy.AlphaBetaMin(depth - 1, max, min);
                move.UnMake();

                if(score >= min)
                {
                    return min;
                }

                if (score > max)
                {
                    if (depth == SearchDepth)
                    {
                        BestMove = move;
                    }
                    max = score;
                }
            }
            return max;
        }

        public int AlphaBetaMin(int depth, int max, int min)
        {
            if (depth == 0)
                return Board.Evaluate();

            var allMoves = GetAllMoves();

            foreach (var move in allMoves)
            {
                move.Make();
                var score = Enemy.AlphaBetaMax(depth - 1, max, min);
                move.UnMake();

                if (score <= max)
                {
                    return max;
                }

                if (score < min)
                {
                    if (depth == SearchDepth)
                    {
                        BestMove = move;
                    }
                    min = score;
                }
            }
            return min;
        }

        internal void ComputerMove()
        {
            if (IsComputer)
            {
                if (this == Board.WhitePlayer)
                {
                    //MiniMax(TPlayer.SearchDepth);
                    //AlphaBetaMin(TPlayer.SearchDepth, -int.MaxValue, int.MaxValue);
                    AlphaBetaNegaMax(TPlayer.SearchDepth, int.MaxValue, -int.MaxValue);
                }
                else
                {
                    //MaxiMin(TPlayer.SearchDepth);
                    //AlphaBetaMax(TPlayer.SearchDepth, -int.MaxValue, int.MaxValue);
                    AlphaBetaNegaMax(TPlayer.SearchDepth, -int.MaxValue, int.MaxValue);
                }
                BestMove.Make();
            }
        }

        public int AlphaBetaNegaMax(int depth, int max, int min)
        {
            if (depth == 0)
                return Quiesce(max, min);

            var allMoves = GetAllMoves();

            foreach (var move in allMoves)
            {
                move.Make();
                var score = -Enemy.AlphaBetaNegaMax(depth - 1, -min, -max);
                move.UnMake();

                if (score >= min)
                {
                    return min;
                }

                if (score > max)
                {
                    if (depth == SearchDepth)
                    {
                        BestMove = move;
                    }
                    max = score;
                }
            }
            return max;
        }

        public int Quiesce(int max, int min)
        {
            var standPat = Board.Evaluate();
            if (standPat >= min)
                return min;
            if (standPat > max)
                max = standPat;

            var allMoves = GetAllMoves();

            foreach (var move in allMoves)
            {
                if(move.Capture == null)
                {
                    continue;
                }

                move.Make();
                var score = -Enemy.Quiesce(-min, -max);
                move.UnMake();

                if (score >= min)
                {
                    return min;
                }

                if (score > max)
                {
                    max = score;
                }
            }
            return max;
        }
    }
}