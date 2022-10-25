using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessFramework
{
    public static class StaticFields
    {
        public static String ImagesFolder = "../../PiecesJPGs/";
        #region PieceName

        public static String Bishop = "Bishop.png";
        public static String Pawn = "Pawn.png";
        public static String Rook = "Rook.png";
        public static String Knight = "Knight.png";
        public static String Queen = "Queen.png";
        public static String King = "King.png";

        #endregion PieceName
        #region Teams

        public static String White = "White";
        public static String Black = "Black";

        #endregion Teams
    }
}
