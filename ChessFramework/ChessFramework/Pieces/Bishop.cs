using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace ChessFramework.Pieces
{
    public class Bishop : Piece
    {
        public override void DoACoolSound()
        {
            var player = new SoundPlayer(StaticFields.Sound.Bishop);
            player.Play();
        }

        public override Piece Clone()
        {
            return new Bishop()
            {
                Team = this.Team,
                PieceType = this.PieceType
            };
        }
    }
}
