using System.Drawing;
using System.Windows.Forms;
using System;
using ChessFramework.Pieces;

namespace ChessFramework
{
    public class Cell : PictureBox
    {
        public Piece Piece;

        public void SetPiece(String team, String piece)
        {
            GenerateCorrectType(piece);
            Piece.Team = team;
            Piece.PieceType = piece;
            BackgroundImage = Image.FromFile(StaticFields.ImagesFolder+Piece.ToString());
        }

        public static void ChangePieces(Cell destination, Cell source)
        {
            source.Piece.DoACoolSound();
            destination.GenerateCorrectType(source.Piece.PieceType);
            destination.Piece.Team = source.Piece.Team;
            destination.Piece.PieceType = source.Piece.PieceType;
            destination.BackgroundImage = source.BackgroundImage;
            source.BackgroundImage = null;
            source.Piece = null;
        }

        private void GenerateCorrectType(string piece)
        {
            switch (piece)
            {
                case StaticFields.Rook:
                    Piece = new Rook();
                    break;

                case StaticFields.Queen:
                    Piece = new Queen();
                    break;
                case StaticFields.King:
                    Piece = new King();
                    break;
                case StaticFields.Knight:
                    Piece = new Knight();
                    break;
                case StaticFields.Bishop:
                    Piece = new Bishop();
                    break;
                default:
                    Piece = new Pawn();
                    break;
            }
        }

        public static void CopyBoard(Cell[,] source, Cell[,] destination)
        {
            for(int i = 0; i < 8; i++)
                for(int j = 0; j< 8; j++)
                {
                    if(source[i,j].Piece != null)
                    destination[i, j].Piece = source[i, j].Piece.Clone();
                }
        }

    }
}
