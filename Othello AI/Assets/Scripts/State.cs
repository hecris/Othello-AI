using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class State
{
    public Board b = new Board();
    public int player;

    public State()
    { // initial state
        b.board[3, 3] = 1;
        b.board[4, 4] = 1;
        b.board[4, 3] = -1;
        b.board[3, 4] = -1;
        player = -1;
    }
    public State(State s)
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                this.b.board[i, j] = s.b.board[i, j];
            }
        }
        player = s.player;
    }

    public State(Board b, int player)
    {
        this.b = b;
        this.player = player;
    }

    public List<Coordinate> possibleMoves()
    {
        List<Coordinate> p = new List<Coordinate>();
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                Coordinate m = new Coordinate(i, j);
                if (b.board[i, j] == 0 && b.CaptureAll(m, player).Count > 0)
                {
                    p.Add(m);
                }
            }
        }
        return p;
    }

    public State Place(Coordinate m)
    {

        if (isPossible(m))
        {
            b.board[m.x, m.y] = player;
            foreach (Coordinate capture in b.CaptureAll(m, player))
            {
                b.board[capture.x, capture.y] = player;
            }

            return new State(b, -player);
        }
        else
        {
            return new State(b, player);
        }


    }



    public bool isPossible(Coordinate m)
    {
        List<Coordinate> possible = possibleMoves();
        foreach (Coordinate c in possible)
        {
            if (c.x == m.x && c.y == m.y)
            {
                return true;
            }
        }
        return false;
    }

	public bool GameOver(){
        State opposite = new State(this.b, -player);
        return (possibleMoves().Count == 0 && opposite.possibleMoves().Count() == 0) ;
	}

}

