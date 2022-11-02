using System.Drawing;
using System.Windows.Forms;
using static ChessFramework.StaticFields;
using System.Collections.Generic;

namespace ChessFramework
{
    public class Board
    {
        private Cell[,] _cell = new Cell[8, 8];
        private Form1 _form;
        private bool _pressedBefore = false;
        private Point _lastLocation;
        private List<Point> _points = new List<Point>();
        public Cell Piece = new Cell();
        private Button _undo;

        public Board(Form1 form)
        {
            _form = form;
            GenerateTable();
            PlacePieces();
        }

        public void GenerateTable()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    _cell[i, j] = new Cell();
                    //_cell[i, j].Location = new Point((_form.Width - 20) / 8 * j, (_form.Height - 80) / 8 * i);
                    _cell[i, j].Width = (_form.Width - 20) / 8;
                    _cell[i, j].Height = (_form.Height - 80) / 8;
                    _cell[i, j].Enabled = true;
                    _cell[i, j].Visible = false;
                    _cell[i, j].Click += (s, e) => { PieceMove(s, (MouseEventArgs)e); };
                    _form.Controls.Add(_cell[i, j]);
                    _cell[i, j].BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                    //_cell[i, j].
                    _cell[i, j].BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
                    if ((i + j) % 2 == 1)
                        _cell[i, j].BackColor = Color.Brown;
                }
            }

            _undo = new Button();
            _undo.Width = 40;
            _undo.Height = 20;
            _undo.Location = new Point(_form.Width/2,_form.Height/2);
            _undo.Enabled = true;
            _undo.Text = "Undo";
            _undo.BackColor = Color.Black;
            _undo.Visible = true;
        }

        public void PlacePieces()
        {
            for (int i = 0; i < 8; i++)
            {
                _cell[1, i].SetPiece(Black, Pawn);
            }

            for (int i = 0; i < 8; i++)
            {
                _cell[6, i].SetPiece(White, Pawn);
            }

            for (int i = 0; i < 8; i += 7)
            {
                _cell[0, i].SetPiece(Black, Rook);
                _cell[7, i].SetPiece(White, Rook);
            }

            for (int i = 1; i < 8; i += 5)
            {
                _cell[0, i].SetPiece(Black, Knight);
                _cell[7, i].SetPiece(White, Knight);
            }

            for (int i = 2; i < 6; i += 3)
            {
                _cell[0, i].SetPiece(Black, Bishop);
                _cell[7, i].SetPiece(White, Bishop);
            }

            _cell[0, 3].SetPiece(Black, Queen);
            _cell[0, 4].SetPiece(Black, King);
            _cell[7, 3].SetPiece(White, Queen);
            _cell[7, 4].SetPiece(White, King);
        }

        public void PieceMove(object sender, MouseEventArgs cellEvent)
        {
            Cell pressedCell = (Cell)sender;
            int y = (int)pressedCell.Location.X / ((_form.Width - 20) / 8);
            int x = (int)pressedCell.Location.Y / ((_form.Height - 80) / 8);

            if (cellEvent.Button == MouseButtons.Left)
            {
                if (_pressedBefore == false)
                {
                    if (_cell[x, y].BackgroundImage != null)
                    {
                        _pressedBefore = true;
                        _lastLocation = new Point(x, y);
                        //_cell[x, y].
                    }
                }
                else
                {
                    if (new Point(x, y) != _lastLocation)
                    {
                        Cell.ChangePieces(_cell[x, y], _cell[_lastLocation.X, _lastLocation.Y]);
                        if (_cell[x, y].Piece.Equals("Pawn.png"))
                        {
                            if ((_cell[x, y].Team == White && x == 0) || (_cell[x, y].Team == Black && x == 7))
                                PromotePawn(x, y, _cell[x, y].Team);
                        }
                    }
                    _pressedBefore = false;
                }
            }
            _points.Add(_lastLocation);
        }

        public void cv(object sender, FormClosedEventArgs e, int x, int y)
        {
            Cell.ChangePieces(_cell[x, y], Piece);
        }

        public void PromotePawn(int x, int y, string team)
        {
            var popUp = new PromotePawnForm(team,this);
            popUp.FormClosed += (s,e) => cv(s,e, x, y);
            popUp.Show();
        }
    }
}
