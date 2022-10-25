using System.Drawing;
using System.Windows.Forms;
using System;
using static ChessFramework.StaticFields;

namespace ChessFramework
{
    public class Cell : PictureBox
    {
        public String Team { get; set; }
        public String Piece { get; set; }

        public void SetPiece(String Team, String Piece)
        {
            this.Team = Team;
            this.Piece = Piece;
            BackgroundImage = Image.FromFile(ImagesFolder+Team+Piece);
        }

        public static void ChangePieces(Cell destination, Cell source)
        {
            destination.Team = source.Team;
            destination.Piece = source.Piece;
            destination.BackgroundImage = source.BackgroundImage;
            source.BackgroundImage = null;
            source.Piece = null;
            source.Team = null;
        }
    }
}
