namespace Chess
{
    partial class TBoard
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TBoard));
            this.pieceImages = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // pieceImages
            // 
            this.pieceImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("pieceImages.ImageStream")));
            this.pieceImages.TransparentColor = System.Drawing.Color.Red;
            this.pieceImages.Images.SetKeyName(0, "Bishop.bmp");
            this.pieceImages.Images.SetKeyName(1, "BishopBlack.bmp");
            this.pieceImages.Images.SetKeyName(2, "King.bmp");
            this.pieceImages.Images.SetKeyName(3, "KingBlack.bmp");
            this.pieceImages.Images.SetKeyName(4, "Knight.bmp");
            this.pieceImages.Images.SetKeyName(5, "KnightBlack.bmp");
            this.pieceImages.Images.SetKeyName(6, "Pawn.bmp");
            this.pieceImages.Images.SetKeyName(7, "PawnBlack.bmp");
            this.pieceImages.Images.SetKeyName(8, "Queen.bmp");
            this.pieceImages.Images.SetKeyName(9, "QueenBlack.bmp");
            this.pieceImages.Images.SetKeyName(10, "Rook.bmp");
            this.pieceImages.Images.SetKeyName(11, "RookBlack.bmp");
            // 
            // TBoard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "TBoard";
            this.Load += new System.EventHandler(this.TBoard_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList pieceImages;
    }
}
