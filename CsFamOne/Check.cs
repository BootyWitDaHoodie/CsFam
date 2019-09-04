using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CsFamOne
{
    class Check
    {
        // Class to check board for various things

        // Check for any free field
        public bool AnyFree(Board board)
        {
            char[,] field = board.GetField(); // copy board

            for (int row = Program.NumRows - 1; row >= 0; row--)
            {
                for (int col = 0; col < Program.NumColumns; col++)
                {
                    if (field[row, col] == Program.Empty)
                    {
                        return true;
                    }
                }

            }

            return false;
        }

        // Check for free columns in given row height (ie 2 means at leas 2 free) Note height can't be higher than num rows
        public List<int> FreeColsWithHeight(Board board, int height)
        {
            List<int> cols = new List<int>();
            int row = Program.NumRows - height;
            for (int col = 0; col < Program.NumColumns; col++)
            {
                if (board.GetField(col, row) == Program.Empty)
                {
                    cols.Add(col);
                }
            }

            cols = cols.Distinct().ToList(); // remove duplicates
            return cols;
        }

        // List of legal moves
        public List<int> LegalMoves(Board board)
        {
            char[,] field = board.GetField(); // copy board
            List<int> legalMoves = new List<int>(); // list of moves

            for (int col = 0; col < Program.NumColumns; col++)
            {
                for (int row = Program.NumRows - 1; row >= 0; row--)
                {
                    if (field[row, col] == Program.Empty)
                    {
                        legalMoves.Add(col);
                    }
                }
            }

            legalMoves = legalMoves.Distinct().ToList(); // remove duplicates
            return legalMoves;
        }

        // Check for winner, tie or nothing (returns None if no winner/tie)
        public char Winner(Board board)
        {
            char[,] field = board.GetField(); // copy board
            char winner;
            // Horizontal
            for (int row = 0; row < Program.NumRows; row++)
            {
                for (int col = 0; col < Program.NumColumns - 3; col++)
                {
                    char temp = field[row, col];
                    if (temp == field[row, col + 1] && temp == field[row, col + 2] && temp == field[row, col + 3] &&
                        temp != Program.Empty)
                    {
                        winner = field[row, col];
                        return winner;
                    }
                }
            }
            // Vertical
            for (int row = 0; row < Program.NumRows - 3; row++)
            {
                for (int col = 0; col < Program.NumColumns; col++)
                {
                    char temp = field[row, col];
                    if (temp == field[row + 1, col] && temp == field[row + 2, col] && temp == field[row + 3, col] &&
                        temp != Program.Empty)
                    {
                        winner = field[row, col];
                        return winner;
                    }
                }
            }
            // Diagonal up
            for (int row = 0; row < Program.NumRows - 3; row++)
            {
                for (int col = 0; col < Program.NumColumns - 3; col++)
                {
                    char temp = field[row, col];
                    if (temp == field[row + 1, col + 1] && temp == field[row + 2, col + 2] && temp == field[row + 3, col + 3] &&
                        temp != Program.Empty)
                    {
                        winner = field[row, col];
                        return winner;
                    }
                }
            }
            // Diagonal down
            for (int row = 3; row < Program.NumRows; row++)
            {
                for (int col = 0; col < Program.NumColumns - 3; col++)
                {
                    char temp = field[row, col];
                    if (temp == field[row - 1, col + 1] && temp == field[row - 2, col + 2] && temp == field[row - 3, col + 3] &&
                        temp != Program.Empty)
                    {
                        winner = field[row, col];
                        return winner;
                    }
                }
            }
            // Tie
            if (AnyFree(board) == false)
            {
                return Program.Tie;
            }
            // Nothing to see here
            return Program.None;
        }
    }
}
