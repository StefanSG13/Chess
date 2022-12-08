using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace ChessFramework.Pieces
{
    public class Knight : Piece
    {
        public override void DoACoolSound()
        {
            var player = new SoundPlayer(StaticFields.Sound.Knight);
            player.Play();
        }

        public override Piece Clone()
        {
            return new Knight()
            {
                Team = this.Team,
                PieceType = this.PieceType
            };
        }
    }
}
