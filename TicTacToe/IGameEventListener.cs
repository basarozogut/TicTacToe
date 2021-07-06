using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    interface IGameEventListener
    {
        void UpdateBox(int row, int column, string value);
        void SetStateText(string text);
        void DisplayAlert(string message);
        void PlayAlertSound();
    }
}
