using System;
using System.Windows.Forms;

namespace ChessFramework
{
    public partial class Form1 : Form
    {
        private Board _board;

        public Form1()
        {
            InitializeComponent();
            this.Size = new System.Drawing.Size(800, 800);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _board = new Board(this);
        }
    }
}
