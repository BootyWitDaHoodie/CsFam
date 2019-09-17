using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;

namespace CsFamOne
{
    class Board
    {
        private char[,] _field;
        public Board()
        {
            // new board
            this._field = new char[Program.NumRows, Program.NumColumns];
            for (int row = 0; row < Program.NumRows; row++)
            {
                for (int col = 0; col < Program.NumColumns; col++)
                {
                    this._field[row, col] = Program.Empty;
                }
            }
        }

        public void DisplayBoard()
        {
            string output = "\n";

            for (int row = Program.NumRows - 1; row >= 0; row--)
            {
                for (int col = 0; col < Program.NumColumns; col++)
                {
                    output += " | " + this._field[row, col];
                }

                output += " |\n  ---------------------------\n";
            }

            output += " | 1 | 2 | 3 | 4 | 5 | 6 | 7 |\n";
            Console.WriteLine(output);
        }

        public char[,] GetField()
        {
            return this._field;
        }

        public char GetField(int col, int row)
        {
            return this._field[row, col];
        }

        public int SetFieldGetRow(int col, char symbol) // use if already checked for free space, returns row number
        {
            int getRow = 0;
            for (int row = 0; row < Program.NumRows; row++)
            {
                getRow = row;
                if (this._field[row, col] == Program.Empty)
                {
                    this._field[row, col] = symbol;
                    return getRow;
                }
            }

            return getRow;
        }

        /// <summary>
        /// Copy this board to multiple other boards
        /// </summary>
        /// <param name="boards">Boards to be copied to</param>
        public void CopyBoard(params Board[] boards)
        {
            foreach (var board in boards)
            {
                for (int row = 0; row < Program.NumRows; row++)
                {
                    for (int col = 0; col < Program.NumColumns; col++)
                    {
                        board._field[row, col] = this._field[row, col];
                    }
                }
            }
        }

        public void SetField(int col, int row, char symbol)
        {
            this._field[row, col] = symbol;
        }

    }

}
