using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTacToe
{
    public partial class GameForm : Form, IGameEventListener
    {
        private readonly IGame _game;

        public GameForm()
        {
            InitializeComponent();
            _game = new Game(this);
        }

        public void DisplayAlert(string message)
        {
            MessageBox.Show(message);
        }

        public void PlayAlertSound()
        {
            SystemSounds.Beep.Play();
        }

        public void SetStateText(string text)
        {
            lblGameState.Text = text;
        }

        public void UpdateBox(int row, int column, string value)
        {
            var matrix = new Button[3][];
            matrix[0] = new[] { btnRow1Col1, btnRow1Col2, btnRow1Col3 };
            matrix[1] = new[] { btnRow2Col1, btnRow2Col2, btnRow2Col3 };
            matrix[2] = new[] { btnRow3Col1, btnRow3Col2, btnRow3Col3 };

            var button = matrix[row][column];
            button.Text = value;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _game.NewGame();
        }

        private void btnRow1Col1_Click(object sender, EventArgs e)
        {
            _game.MakeMove(0, 0);
        }

        private void btnRow1Col2_Click(object sender, EventArgs e)
        {
            _game.MakeMove(0, 1);
        }

        private void btnRow1Col3_Click(object sender, EventArgs e)
        {
            _game.MakeMove(0, 2);
        }

        private void btnRow2Col1_Click(object sender, EventArgs e)
        {
            _game.MakeMove(1, 0);
        }

        private void btnRow2Col2_Click(object sender, EventArgs e)
        {
            _game.MakeMove(1, 1);
        }

        private void btnRow2Col3_Click(object sender, EventArgs e)
        {
            _game.MakeMove(1, 2);
        }

        private void btnRow3Col1_Click(object sender, EventArgs e)
        {
            _game.MakeMove(2, 0);
        }

        private void btnRow3Col2_Click(object sender, EventArgs e)
        {
            _game.MakeMove(2, 1);
        }

        private void btnRow3Col3_Click(object sender, EventArgs e)
        {
            _game.MakeMove(2, 2);
        }

        private void btnNewGame_Click(object sender, EventArgs e)
        {
            _game.NewGame();
        }

        private void btnUndoMove_Click(object sender, EventArgs e)
        {
            _game.UndoLastMove();
        }
    }
}
