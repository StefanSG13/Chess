using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ChessFramework.StaticFields;

namespace ChessFramework
{
    public partial class PromotePawnForm : Form
    {
        private String PieceType;
        private Cell[] _buttons = new Cell[4];

        public PromotePawnForm(String team)
        {
            InitializeComponent();
            this.Size = new System.Drawing.Size(480,200);
            GenerateButtons();
            SetImages(team);
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

        }
    }
}
