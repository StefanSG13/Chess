using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace ChessFramework.Pieces
{
    public abstract class Piece
    {
        public String Team { get; set; }
        public String PieceType { get; set; }

        public Piece()
        {
        }

        public void SetPiece(string team, string piece)
        {
            Team = team;
            PieceType = piece;
        }

        public virtual void DoACoolSound(){}

        public override String ToString()
        {
            return Team + PieceType;
        }

        public virtual Piece Clone()
        {
            return this.Clone();
        }
    }
}
