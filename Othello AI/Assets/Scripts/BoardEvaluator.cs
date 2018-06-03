using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


class BoardEvaluator
{
    public int[,] boardValues = new int[8,8] {
        {99, 5, 5, 5, 5, 5, 5, 99 },
        {5 , 1, 1, 1, 1, 1, 1,  5 },
        {5 , 1, 1, 1, 1, 1, 1,  5 },
        {5 , 1, 1, 3, 3, 1, 1,  5 },
        {5 , 1, 1, 3, 3, 1, 1,  5 },
        {5 , 1, 1, 1, 1, 1, 1,  5 },
        {5 , 1, 1, 1, 1, 1, 1,  5 },
        {99, 5, 5, 5, 5, 5, 5,  99}
    };

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

    public int evaluate2(State s)
    {
        Board b = s.b;
        int turn = s.player;
        int playerScore = 0;
        int oppositeScore = 0;

        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (b.board[i, j] == turn)
                {
                    playerScore += boardValues[i,j];
                }
                else if (b.board[i, j] == -turn)
                {
                    oppositeScore += boardValues[i,j];
                }
            }
        }

        return playerScore - oppositeScore;

    }

}

