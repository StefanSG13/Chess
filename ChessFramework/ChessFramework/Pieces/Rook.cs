using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;

namespace ChessFramework.Pieces
{
    public class Rook : Piece
    {
        public override void DoACoolSound()
        {
            var player = new SoundPlayer(StaticFields.Sound.Rook);
            player.Play();
        }
        public override Piece Clone()
        {
            return new Rook() { Team = this.Team, PieceType = this.PieceType };
        }
    }
}
