using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Chess
{
    public partial class TBoard : UserControl
    {
        public static int N = 8;
        public TCell[,] Cells = new TCell[N, N];
        public TPlayer WhitePlayer = new TPlayer();
        public TPlayer BlackPlayer = new TPlayer();
        public TPlayer ActivePlayer;
        public List<TMove> Moves = new List<TMove>();

        private TPiece _ActivePiece;
        public TPiece ActivePiece
        {
            get
            {
                return _ActivePiece;
            }
            set
            {
                foreach (var cell in Cells)
                {
                    cell.IsHighlighted = false;
                }

                _ActivePiece = value;
                if (_ActivePiece != null)
                {
                    var moves = _ActivePiece.GetMoves();
                    foreach (var move in moves)
                    {
                        move.StopCell.IsHighlighted = true;
                    }
                }
                Invalidate();
            }
        }

        public TBoard()
        {
            InitializeComponent();
            DoubleBuffered = true;
            for (int y = 0; y < N; y++)
            {
                for (int x = 0; x < N; x++)
                {
                    var cell = new TCell();
                    cell.Board = this;
                    cell.X = x;
                    cell.Y = y;
                    Cells[y, x] = cell;
                }
            }
            Reset();
        }

        private void TBoard_Load(object sender, EventArgs e)
        {

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.ScaleTransform(CellWidth, CellHeight);
            var brush = new SolidBrush(ForeColor);

            for (int y = 0; y < N; y++)
            {
                for (int x = 0; x < N; x++)
                {
                    var cell = Cells[y, x];
                    brush.Color = ForeColor;
                    if ((x + y) % 2 == 0)
                        brush.Color = BackColor;
                    if (cell == ActivePiece?.Cell)
                        brush.Color = Color.Red;
                    if (cell.IsHighlighted)
                        brush.Color = Color.Magenta;
                    var rc = new RectangleF(x, y, 1, 1);
                    e.Graphics.FillRectangle(brush, rc);
                }

                var player = WhitePlayer;
                for (int i = 0; i < 2; i++)
                {

                    foreach (var piece in player.Pieces)
                    {
                        var rc = new RectangleF(piece.Cell.X, piece.Cell.Y, 1, 1);
                        var image = pieceImages.Images[piece.ImageId + i];
                        e.Graphics.DrawImage(image, rc);
                    }
                    player = BlackPlayer;
                }
            }
        }
        public void Reset()
        {
            Moves.Clear();
            ActivePlayer = WhitePlayer;
            var player = WhitePlayer;
            for (int i = 0; i < 2; i++)
            {
                player.Board = this;
                new Rook(player);
                new Knight(player);
                new Bishop(player);
                new TQueen(player);
                new TKing(player);
                new Bishop(player);
                new Knight(player);
                new Rook(player);
                for (int j = 0; j < N; j++)
                {
                    new TPawn(player);
                }
                for (int j = 0; j < player.Pieces.Count; j++)
                {
                    var x = j % N;
                    var y = N - (j / N) - 1; ;
                    if (player == BlackPlayer)
                    {
                        y = j / N;
                    }
                    player.Pieces[j].Cell = Cells[y, x];
                }
                player = BlackPlayer;
            }
        }

        public float CellWidth
        {
            get { return (float)Width / N; }
        }
        public float CellHeight
        {
            get { return (float)Height / N; }
        }

        public TCell CellAt(float x, float y)
        {
            y /= CellHeight;
            x /= CellWidth;
            if (x < 0) x = 0;
            if (x > N - 1) x = N - 1;
            if (y < 0) y = 0;
            if (y > N - 1) y = N - 1;
            return Cells[(int)y, (int)x];
        }
        TCell StartCell;
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            StartCell = CellAt(e.X, e.Y);
            ActivePiece = StartCell.Piece;

            if (ActivePiece?.Player != ActivePlayer)
                ActivePiece = null;
            Invalidate();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (e.Button == MouseButtons.Left && ActivePiece != null)
            {
                ActivePiece.Cell = CellAt(e.X, e.Y);
                Invalidate();
            }
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (ActivePiece != null)
            {
                var stopCell = CellAt(e.X, e.Y);
                ActivePiece.Cell = StartCell;
                if (stopCell.IsHighlighted)
                {
                    var moves = ActivePiece.GetMoves();
                    var move = moves.Find(x => x.StopCell == stopCell);
                    move.Make();
                    ActivePiece = null;
                    ActivePlayer.ComputerMove();
                }
                Invalidate();
            }
        }

        public int Evaluate()
        {
            var score = 0;
            foreach (var piece in BlackPlayer.Pieces)
            {
                score += piece.Value;
            }
            foreach (var piece in WhitePlayer.Pieces)
            {
                score -= piece.Value;
            }
            return score;
        }
    }
}
