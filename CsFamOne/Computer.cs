using System;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using System.Text;

namespace CsFamOne
{
    class Computer
    {
        private char _symbol;
        private char _opponent;
        private static Check _check = new Check();
        private static readonly List<int> _bestColumns = new List<int>() {3, 2, 4, 1, 5, 0, 6};

        public Computer(char symbolIn, char playerSymbol)
        {
            this._symbol = symbolIn;
            this._opponent = playerSymbol;
        }

        public int BigBrainTime(Board board) // should return move
        {
            int move;
            char[,] field = board.GetField();
            List<int> legalMoves = _check.LegalMoves(board);
            move = WinMoves(board);
            if (move != 999)
            {
                return move;
            }

            move = StartingMoves(board);
            if (move != 999)
            {
                return move;
            }

            List<int> donts = TwoSteps(board);
            if (donts.Count != 0)
            {
                foreach (var col in _bestColumns)
                {
                    if (!donts.Contains(col) && legalMoves.Contains(col)) // make sure it's legal and proper move
                    {
                        return col;
                    }
                }
            }

            foreach (var col in _bestColumns) // last best thing
            {
                if (legalMoves.Contains(col))
                {
                    return col;
                }
            }

            return 0;
        }

        private List<int> TwoSteps(Board board) // returns list with moves not to make, because it enables a victory for the opponent
        {
            List<int> donts = new List<int>();
            int row1;
            int row2;
            List<int> freeColumns = _check.FreeColsWithHeight(board, 2);
            foreach (var col in freeColumns)
            {
                row1 = board.SetFieldGetRow(col, this._symbol);
                row2 = board.SetFieldGetRow(col, this._opponent);
                if (_check.Winner(board) == this._opponent)
                {
                    donts.Add(col);
                }

                board.SetField(col, row2, Program.Empty);
                board.SetField(col, row1, Program.Empty);
            }

            return donts;
        }

        private int StartingMoves(Board board)
        {
            int[] goodMoves = {3, 2, 4};
            foreach (var col in goodMoves)
            {
                if (board.GetField(col, 0) == Program.Empty)
                {
                    return col;
                }
            }

            return 999;
        }

        private int WinMoves(Board board) // predict winmove for self and opponent
        {
            int move = WinMove(board, this._symbol);
            if (move != 999)
            {
                return move;
            }
            else
            {
                return WinMove(board, this._opponent);
            }
        }
        private int WinMove(Board board, char symbol) // return col
        {
            List<int> legalMoves = _check.LegalMoves(board);
            int row;
            foreach (var col in legalMoves)
            {
                row = board.SetFieldGetRow(col, symbol);
                if (_check.Winner(board) == symbol)
                {
                    board.SetField(col, row, Program.Empty);
                    return col;
                }
                board.SetField(col, row, Program.Empty); // komt vanaf hier met col 6 (is out of range)
            }

            return 999;
        }
    }


}
