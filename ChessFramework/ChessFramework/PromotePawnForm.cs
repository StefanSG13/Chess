using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ChessFramework.StaticFields;

namespace ChessFramework
{
    public partial class PromotePawnForm : Form
    {
        public String PieceType { get; private set; }
        private Cell[] _buttons = new Cell[4];
        private bool _pressedButton=false;
        private Board _board;

        public PromotePawnForm(String team, Board board)
        {
            this.Size = new System.Drawing.Size(480, 200);
            GenerateButtons();
            SetImages(team);
            _board = board;
        }

        private void PromotePawnForm_Load(object sender, EventArgs e)
        {

        }

        private void GenerateButtons()
        {

            for(int i = 0; i < 4; i++)
            {
                _buttons[i] = new Cell();
                _buttons[i].Location = new Point((this.Width - 20) / 4 * i, 25);
                _buttons[i].Width = (this.Width - 20) / 4;
                _buttons[i].Height = _buttons[i].Width;
                _buttons[i].Enabled = true;
                _buttons[i].Visible = true;
                _buttons[i].BackgroundImageLayout = ImageLayout.Center;
                _buttons[i].BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                _buttons[i].Click += (s, e) => { ClickedButton(s, (MouseEventArgs)e); };
                this.Controls.Add(_buttons[i]);
            }
        }

        public void SetImages(string team)
        {
            _buttons[0].SetPiece(team, Queen);
            _buttons[1].SetPiece(team, Rook);
            _buttons[2].SetPiece(team, Knight);
            _buttons[3].SetPiece(team, Bishop);
        }

        private void ClickedButton(object sender, MouseEventArgs e)
        {
            Cell btn = (Cell)sender;
            int i = Array.IndexOf(_buttons, btn);
            btn = _buttons[i];
            _pressedButton = true;
            _board.Piece = btn;
            this.Close();
        }
    }
}
