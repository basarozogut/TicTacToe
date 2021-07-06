using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    interface IGame
    {
        void MakeMove(int row, int column);
        void UndoLastMove();
        void NewGame();
    }
}
