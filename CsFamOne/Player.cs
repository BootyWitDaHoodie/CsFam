using System;
using System.Collections.Generic;
using System.Text;

namespace CsFamOne
{
    class Player
    {
        // Player class
        public char symbol;

        public Player(char symbolIn)
        {
            this.symbol = symbolIn;
        }

        public int AskMove()
        {
            int move = 0;
            while (true)
            {
                Console.Write("Please make a move: ");
                while (!int.TryParse(Console.ReadLine(), out move))
                {
                    Console.WriteLine("Please enter 1-7 only!");
                }

                if (move >= 1 && move <= 7)
                {
                    move--;
                    return move;
                }
                else
                {
                    Console.WriteLine("Please enter 1-7 only!");
                }
            }
        }
    }
}
