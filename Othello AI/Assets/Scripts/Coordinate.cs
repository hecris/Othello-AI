using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


class Coordinate
{
    public int x;
    public int y;
    public Coordinate(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public Coordinate(string c)
    {
        string[] m = c.Split(',');
        int x = int.Parse(m[0]);
        int y = int.Parse(m[1]);
        this.x = x;
        this.y = y;

    }

    public string AsString()
    {
        return (x.ToString() + "," + y.ToString());
    }

    public static Coordinate operator +(Coordinate c1, Coordinate c2)
    {
        return new Coordinate(c1.x + c2.x, c1.y + c2.y);
    }

    public static Coordinate operator *(Coordinate c1, int n)
    {
        return new Coordinate(c1.x * n, c1.y * n);
    }



}

