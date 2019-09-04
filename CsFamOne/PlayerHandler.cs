using System;
using System.Collections.Generic;
using System.Text;

namespace CsFamOne
{
    class PlayerHandler
    {
        Check check = new Check();
        public int Move(Board board, Player player)
        {
            int move;
            do
            {
                move = player.AskMove();
            } while (!check.LegalMoves(board).Contains(move));

            return move;
        }
    }
}
