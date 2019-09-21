using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.CompilerServices;
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
        //private static List<int> pointsOfMoves;
        private List<int> pointsOfMoves; // for pc vs pc
        private int _currentColumn;


        public Computer(char symbolIn, char playerSymbol)
        {
            this._symbol = symbolIn;
            this._opponent = playerSymbol;
        }

        private int BigBrainTime(Board board) // should return move
        {
            int move;
            char[,] field = board.GetField();
            List<int> legalMoves = _check.LegalMoves(board);
            //AdvancedMoves(board, legalMoves);
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

            List<int> donts = TwoSteps(board); // don't enable win opponent

            int advancedMove = AdvancedMoves(board, legalMoves);

            if (donts.Count != 0)
            {
                if (!donts.Contains(advancedMove)) return advancedMove;
                foreach (var col in _bestColumns)
                {
                    if (!donts.Contains(col) && legalMoves.Contains(col)) // make sure it's legal and proper move
                    {
                        return col;
                    }
                }
            }

            return advancedMove;

            //foreach (var col in _bestColumns) // last best thing
            //{
            //    if (legalMoves.Contains(col))
            //    {
            //        return col;
            //    }
            //}

            //return 0;
        }

        public int MakeMove(Board boardIn)
        {
            Board board = boardIn.CopyBoard();
            int move = BigBrainTime(board);
            DisplayMoveMessage(move);
            return move;
        }

        private void DisplayMoveMessage(int move)
        {
            Console.WriteLine($"I put mine in col nr {move + 1}");
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

        private int AdvancedMoves(Board board, List<int> freeColumns)
        {
            List<int> columnNumbers = new List<int>();
            List<int> winnerScores = new List<int>();
            Board copiedBoard = new Board();
            int score;
            foreach (var col in freeColumns)
            {
                board.CopyBoard(copiedBoard);
                // list stuff
                pointsOfMoves = new List<int>(); // make list/ refresh list
                columnNumbers.Add(col);
                _currentColumn = col;
                EndWinnerRecursion(copiedBoard, col, this._symbol, 0);
                score = pointsOfMoves.Sum() / pointsOfMoves.Count;
                winnerScores.Add(score);
            }
            //DisplayLists(winnerScores, columnNumbers);
            int winningScore = winnerScores.Max();
            int winningColumn = columnNumbers[winnerScores.IndexOf(winningScore)];
            Console.WriteLine($"Winning score: {winningScore}, winning column: {winningColumn + 1}");
            return winningColumn;
        }

        private void DisplayLists(List<int> scores, List<int> columns)
        {
            Console.WriteLine();
            foreach (var score in scores)
            {
                Console.WriteLine($"Column: {columns[scores.IndexOf(score)]} with {score} points");
            }
            Console.WriteLine();
        }

        private void EndWinnerRecursion(Board board, int col, char player, int count)
        { // make void and edit list in property's
            Board copiedBoard = new Board();
            board.CopyBoard(copiedBoard);
            copiedBoard.SetFieldGetRow(col, player);
            count++; // added count to try limit turn time

            // update list of legal moves
            List<int> updatedFreeColumns = new List<int>();
            updatedFreeColumns = _check.LegalMoves(copiedBoard);

            char endWinner = _check.Winner(copiedBoard);
            if (endWinner != Program.None || count > 12)
            {
                //return endWinner; // add to list
                if (endWinner == this._symbol) pointsOfMoves.Add(100);
                else if (endWinner == this._opponent) pointsOfMoves.Add(-100);
                else if (endWinner == Program.Tie) pointsOfMoves.Add(10);
                else pointsOfMoves.Add(1);
                if (endWinner != Program.None) Console.WriteLine($"Result of turn {count}, column {_currentColumn + 1} = {endWinner}");
            }
            else if (pointsOfMoves.Sum() > 1000 || pointsOfMoves.Sum() < -1000) // if doesnt do right alter these, maybe remove > 1000
            {
                Console.WriteLine($"Not added, points: {pointsOfMoves.Sum()}");
                return;
            }
            else
            {
                foreach (var mov in updatedFreeColumns)
                {
                    if (player == this._symbol)
                    {
                        EndWinnerRecursion(copiedBoard, mov, this._opponent, count);
                    }
                    else
                    {
                        EndWinnerRecursion(copiedBoard, mov, this._symbol, count);
                    }
                }
            }
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
