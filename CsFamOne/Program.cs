using System;

namespace CsFamOne
{
    class Program
    {
        // Constant globals
        public const int NumRows = 6;
        public const int NumColumns = 7;
        public const char X = 'X';
        public const char O = 'O';
        public const char Empty = ' ';
        public const char Tie = 'T';
        public const char None = 'N';

        static void Main(string[] args)
        {
            Console.WriteLine("Welkom");
            Board board = new Board();
            board.DisplayBoard();
            Check check = new Check();
            PlayerHandler playerHandler = new PlayerHandler();
            // players
            Player player1 = new Player(X);
            Player player2 = new Player(O);
            Computer computer1 = new Computer(O, X);
            char turn = X;
            int move;
            while (check.Winner(board) == None)
            {
                if (turn == X)
                {
                    move = playerHandler.Move(board, player1);
                }
                else
                {
                    //move = playerHandler.Move(board, player2);
                    move = computer1.BigBrainTime(board);
                }

                board.SetFieldGetRow(move, turn);
                turn = NextTurn(turn);
                board.DisplayBoard();
                Console.WriteLine();
            }

            char winner = check.Winner(board);
            if (winner != Tie)
            {
                Console.WriteLine(winner + " won the game!");
            }


            //Console.WriteLine(check.AnyFree(board));
            //foreach (var col in check.LegalMoves(board))
            //{
            //    Console.WriteLine("\nNum one : " + col);
            //}
            //Console.WriteLine();
            //Player player = new Player(X);
            //int move = player.AskMove();
            //Console.WriteLine("Move made = " + move);

            //Console.WriteLine("Hello World!");
            //Console.WriteLine("Byeeee");
            //Multiplier byFive = new Multiplier(5);
            //Multiplier bySix = new Multiplier(6);
            //Console.WriteLine("byFive: " + byFive.Multiply(12));
            //Console.WriteLine("bySix: " + bySix.Multiply(12));
            //int i = Multiplier.GetNumber();
            //Console.WriteLine("\nNumber: " + i);

            //CountTwo(5);

        }

        public static char NextTurn(char turnIn)
        {
            if (turnIn == X)
            {
                return O;
            }
            else
            {
                return X;
            }
        }

    }

}

//public class Multiplier
    //{
    //    private readonly int _ratio;
    //    private static int _numOfConverters;

    //    public Multiplier(int ratioIn)
    //    {
    //        this._ratio = ratioIn;
    //        _numOfConverters += 1;
    //    }

    //    public int Multiply(int x)
    //    {
    //        return x * this._ratio;
    //    }

    //    public static int GetNumber()
    //    {
    //        return _numOfConverters;
    //    }
    //}

        //static void CountTwo(int x)
        //{
        //    // count some
        //    int y = x * 5;
        //    Console.WriteLine(x + " * 5 = " + y);
        //}
//}

//Welcome message
//Start loop
//    Vraag of tegen speler of pc
//    -> als pc dan level
//        Loop spel
//    Winnaar
//    Nog een spel
//Doeidoei
