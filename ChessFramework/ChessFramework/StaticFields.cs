using ChessFramework.Pieces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessFramework
{
    public static class StaticFields
    {
        public const String ImagesFolder = "../../PiecesJPGs/";

        #region PieceName

        public const String Bishop = "Bishop.png";
        public const String Pawn = "Pawn.png";
        public const String Rook = "Rook.png";
        public const String Knight = "Knight.png";
        public const String Queen = "Queen.png";
        public const String King = "King.png";

        #endregion PieceName

        #region Teams

        public static String White = "White";
        public static String Black = "Black";

        #endregion Teams

        #region Sounds

        public static class Sound
        {
            public const String Bishop = "../../Sounds/Bishop.wav";
            public const String Pawn = "../../Sounds/Pawn.wav";
            public const String Rook = "../../Sounds/Rook.wav";
            public const String Knight = "../../Sounds/Knight.wav";
            public const String Queen = "../../Sounds/Queen.wav";
            public const String King = "../../Sounds/King.wav";
        }

        #endregion Sounds
    }
}
