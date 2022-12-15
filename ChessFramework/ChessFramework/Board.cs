using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Runtime.Remoting.Channels;
using System;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.IO;
using System.Text;

namespace ChessFramework
{
    public class Board
    {
        #region Fields

        private Cell[,] _cell = new Cell[8, 8];
        private Form1 _form;
        private bool _pressedBefore = false;
        private Point _lastLocation;
        private List<Point> _points = new List<Point>();
        public Cell Piece = new Cell();

        public TcpClient client;
        public NetworkStream clientStream;
        public bool ascult;
        public Thread t;

        #endregion Fields

        #region GameStart
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
                    _cell[i, j] = new Cell
                    {
                        Location = new Point((_form.Width - 20) / 8 * j, (_form.Height - 80) / 8 * i),
                        Width = (_form.Width - 20) / 8,
                        Height = (_form.Height - 80) / 8,
                        BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
                    };
                    _cell[i, j].Click += (s, e) => { PieceMove(s, (MouseEventArgs)e); };
                    _form.Controls.Add(_cell[i, j]);
                    if ((i + j) % 2 == 1)
                        _cell[i, j].BackColor = Color.Brown;
                }
            }
        }

        public void PlacePieces()
        {
            for (int i = 0; i < 8; i++)
            {
                _cell[1, i].SetPiece(StaticFields.Black, StaticFields.Pawn);
            }

            for (int i = 0; i < 8; i++)
            {
                _cell[6, i].SetPiece(StaticFields.White, StaticFields.Pawn);
            }

            for (int i = 0; i < 8; i += 7)
            {
                _cell[0, i].SetPiece(StaticFields.Black, StaticFields.Rook);
                _cell[7, i].SetPiece(StaticFields.White, StaticFields.Rook);
            }

            for (int i = 1; i < 8; i += 5)
            {
                _cell[0, i].SetPiece(StaticFields.Black, StaticFields.Knight);
                _cell[7, i].SetPiece(StaticFields.White, StaticFields.Knight);
            }

            for (int i = 2; i < 6; i += 3)
            {
                _cell[0, i].SetPiece(StaticFields.Black, StaticFields.Bishop);
                _cell[7, i].SetPiece(StaticFields.White, StaticFields.Bishop);
            }

            _cell[0, 3].SetPiece(StaticFields.Black, StaticFields.Queen);
            _cell[0, 4].SetPiece(StaticFields.Black, StaticFields.King);
            _cell[7, 3].SetPiece(StaticFields.White, StaticFields.Queen);
            _cell[7, 4].SetPiece(StaticFields.White, StaticFields.King);
        }

        #endregion GameStart

        #region GameLogic

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
                    }
                }
                else
                {
                    if (new Point(x, y) != _lastLocation)
                    {
                        MovePieces(new Point(x, y), new Point(_lastLocation.X, _lastLocation.Y));
                        
                        if (_cell[x, y].Piece.PieceType.Equals("Pawn.png"))
                        {
                            if ((_cell[x, y].Piece.Team == StaticFields.White && x == 0) || (_cell[x, y].Piece.Team == StaticFields.Black && x == 7))
                                PromotePawn(x, y, _cell[x, y].Piece.Team);
                        }
                    }
                    _pressedBefore = false;
                }
            }
            _points.Add(_lastLocation);
        }

        public void cv(object sender, FormClosedEventArgs e, int x, int y)
        {
            if(Piece != null)
                Cell.ChangePieces(_cell[x, y], Piece);
            _form.Enabled = true;
        }

        public void MovePieces(Point source, Point destination)
        {
            Cell.ChangePieces(_cell[source.X, source.Y], _cell[destination.X, destination.Y]);
        }

        public void PromotePawn(int x, int y, string team)
        {
            var popUp = new PromotePawnForm(team,this);

            popUp.FormClosed += (s,e) => cv(s,e, x, y);
            popUp.Show();
            _form.Enabled = false;
        }

        public void SendOverLan(Point source, Point destination)
        {
            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
            string response = $"{source.X}{source.Y}{destination.X}{destination.Y}";

            if (ipAddress != null)
            {
                IPEndPoint serverEndPoint = new IPEndPoint(ipAddress, 3000);
                byte[] receiveBuffer = new byte[100];

                try
                {
                    using (TcpClient client = new TcpClient(serverEndPoint))
                    {
                        using (Socket socket = client.Client)
                        {
                            socket.Connect(serverEndPoint);

                            byte[] data = Encoding.ASCII.GetBytes(response);

                            socket.Send(data, data.Length, SocketFlags.None);

                            socket.Receive(receiveBuffer);

                            Console.WriteLine(Encoding.ASCII.GetString(receiveBuffer));
                        }
                    }
                }
                catch (SocketException socketException)
                {
                    throw;
                }
            }
        }

        #endregion GameLogic
    }
}
