using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    class GameState
    {
        public readonly BoxState[][] Boxes;
        public BoxState Player;
        public bool Finished;

        public GameState()
        {
            Boxes = new BoxState[3][];
            Player = BoxState.O;
            for (int i = 0; i < Boxes.Length; i++)
            {
                Boxes[i] = new BoxState[3];
                for (var j = 0; j < Boxes[i].Length; j++)
                {
                    Boxes[i][j] = BoxState.Empty;
                }
            }
            Finished = false;
        }

        public static GameState CreateCopyFrom(GameState other)
        {
            var state = new GameState();
            for (var i = 0; i < other.Boxes.Length; i++)
            {
                for (var j = 0; j < other.Boxes[i].Length; j++)
                {
                    state.Boxes[i][j] = other.Boxes[i][j];
                }
            }

            state.Player = other.Player;
            state.Finished = other.Finished;

            return state;
        }
    }

    public enum BoxState
    {
        Empty, X, O
    }
}
