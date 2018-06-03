using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


class Board
{
    public int[,] board = new int[8, 8];

    public void print()
    {
        for (int i = 0; i < 8; i++)
        {

            for (int j = 0; j < 8; j++)
            {
                string o = "-";
                switch (board[i, j])
                {
                    case 1:
                        o = "w";
                        break;
                    case -1:
                        o = "b";
                        break;
                }
                Console.Write(o + " ");
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }



    bool isOnBoard(Coordinate c)
    {
        int x = c.x;
        int y = c.y;
        return (0 <= x && x <= 7 && 0 <= y && y <= 7);
    }

    public List<Coordinate> CaptureAll(Coordinate m, int color)
    {
        List<Coordinate> p = new List<Coordinate>();
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                Coordinate d = new Coordinate(x, y);
                foreach (Coordinate coor in piecesToCapture(m, d, color))
                {
                    //board[coor.x, coor.y] = color;
                    p.Add(coor);
                    //Console.WriteLine(coor.AsString());
                }
            }
        }
        return p;
    }

    List<Coordinate> piecesToCapture(Coordinate m, Coordinate d, int color)
    {
        List<Coordinate> p = new List<Coordinate>();
        Coordinate n = m + d;
        if (isOnBoard(n) && board[n.x, n.y] == -color)
        {
            for (int i = 1; i < 8; i++)
            {
                Coordinate v = d * i;
                Coordinate s = m + v;
                if (isOnBoard(s) && board[s.x, s.y] == -color)
                {
                    p.Add(s);
                }
                else if (isOnBoard(s) && board[s.x, s.y] == color)
                {
                    return p;
                }
            }
        }

        return new List<Coordinate>();
    }

}

