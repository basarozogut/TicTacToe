using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    class Game : IGame
    {
        private readonly IGameEventListener _gameEventListener;

        private Stack<GameState> _gameState;

        public Game(IGameEventListener gameEventListener)
        {
            _gameEventListener = gameEventListener;
        }

        public void MakeMove(int row, int column)
        {
            var state = _gameState.Peek();
            if (state.Finished)
            {
                _gameEventListener.PlayAlertSound();
                return;
            }

            var val = state.Boxes[row][column];

            if (val != BoxState.Empty)
            {
                _gameEventListener.PlayAlertSound();
                return;
            }

            var newState = GameState.CreateCopyFrom(state);

            if (state.Player == BoxState.X)
                newState.Player = BoxState.O;
            else
                newState.Player = BoxState.X;

            newState.Boxes[row][column] = newState.Player;

            _gameState.Push(newState);
            UpdateUserInterface();

            if (CheckForWin())
            {
                _gameEventListener.SetStateText($"Player {GetBoxValue(newState.Player)} won!");
                _gameEventListener.DisplayAlert("Game completed!");
                newState.Finished = true;
            }

            if (CheckForExhaustion())
            {
                _gameEventListener.SetStateText($"All moves exhausted!");
                _gameEventListener.DisplayAlert("Game completed!");
                newState.Finished = true;
            }
        }

        public void NewGame()
        {
            _gameState = new Stack<GameState>();
            _gameState.Push(new GameState());
            UpdateUserInterface();
        }

        public void UndoLastMove()
        {
            if (_gameState.Count > 1)
            {
                _gameState.Pop();
                UpdateUserInterface();
            }
            else
            {
                _gameEventListener.DisplayAlert("No more moves to undo!");
            }
        }

        private void UpdateUserInterface()
        {
            var state = _gameState.Peek();
            for (var i = 0; i < state.Boxes.Length; i++)
            {
                var row = state.Boxes[i];
                for (var j = 0; j < row.Length; j++)
                {
                    var boxValue = GetBoxValue(row[j]);
                    _gameEventListener.UpdateBox(i, j, boxValue);
                }
            }

            if (state.Player == BoxState.X)
                _gameEventListener.SetStateText($"Player O's turn.");
            else
                _gameEventListener.SetStateText($"Player X's turn.");
        }

        private string GetBoxValue(BoxState st)
        {
            if (st == BoxState.Empty)
                return string.Empty;

            if (st == BoxState.X)
                return "X";

            if (st == BoxState.O)
                return "O";

            throw new GameException("Unknown box state!");
        }

        /// <summary>
        /// At least 1 empty box means moves are remaining.
        /// </summary>
        /// <returns></returns>
        private bool CheckForExhaustion()
        {
            var state = _gameState.Peek();

            for (var i = 0; i < state.Boxes.Length; i++)
            {
                for (var j = 0; j < state.Boxes[i].Length; j++)
                {
                    if (state.Boxes[i][j] == BoxState.Empty)
                        return false;
                }
            }

            return true;
        }

        private bool CheckForWin()
        {
            var hMask1 = new int[3][]
            {
                new int[3] {1,1,1},
                new int[3] {0,0,0},
                new int[3] {0,0,0}
            };
            var hMask2 = new int[3][]
            {
                new int[3] {0,0,0},
                new int[3] {1,1,1},
                new int[3] {0,0,0}
            };
            var hMask3 = new int[3][]
            {
                new int[3] {0,0,0},
                new int[3] {0,0,0},
                new int[3] {1,1,1}
            };
            var vMask1 = new int[3][]
            {
                new int[3] {1,0,0},
                new int[3] {1,0,0},
                new int[3] {1,0,0}
            };
            var vMask2 = new int[3][]
            {
                new int[3] {0,1,0},
                new int[3] {0,1,0},
                new int[3] {0,1,0}
            };
            var vMask3 = new int[3][]
            {
                new int[3] {0,0,1},
                new int[3] {0,0,1},
                new int[3] {0,0,1}
            };
            var dMask1 = new int[3][]
            {
                new int[3] {1,0,0},
                new int[3] {0,1,0},
                new int[3] {0,0,1}
            };
            var dMask2 = new int[3][]
            {
                new int[3] {0,0,1},
                new int[3] {0,1,0},
                new int[3] {1,0,0}
            };

            var state = _gameState.Peek();

            return
                CheckMask(state, hMask1) ||
                CheckMask(state, hMask2) ||
                CheckMask(state, hMask3) ||
                CheckMask(state, vMask1) ||
                CheckMask(state, vMask2) ||
                CheckMask(state, vMask3) ||
                CheckMask(state, dMask1) ||
                CheckMask(state, dMask2);
        }

        /// <summary>
        /// Check if current game state conforms to mask.
        /// </summary>
        /// <param name="state"></param>
        /// <param name="mask"></param>
        /// <returns></returns>
        private bool CheckMask(GameState state, int[][] mask)
        {
            var player = state.Player;
            var boxes = state.Boxes;

            for (var i = 0; i < boxes.Length; i++)
            {
                for (int j = 0; j < boxes[i].Length; j++)
                {
                    var stateVal = boxes[i][j];
                    var maskVal = mask[i][j];

                    if (maskVal == 1 && stateVal == player)
                    {
                        // conforms, do nothing.
                    }
                    else if (maskVal == 0 && stateVal != player)
                    {
                        // conforms, do nothing.
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
