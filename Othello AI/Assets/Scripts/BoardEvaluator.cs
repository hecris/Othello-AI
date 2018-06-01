using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


    class BoardEvaluator
    {
        public int evaluate(State s)
        {
            Board b = s.b;
            int turn = s.player;
            int playerPieces = 0;
            int oppositePieces = 0;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (b.board[i, j] == turn)
                    {
                        playerPieces += 1;
                    }
                    else if (b.board[i, j] == -turn)
                    {
                        oppositePieces += 1;
                    }
                }
            }
            return playerPieces - oppositePieces;

        }

    }

