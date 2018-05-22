using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coordinate {

    private int x = -1;
    private int y = -1;

    public Coordinate()  {}

    public Coordinate(int x, int y)
    {
        this.x = x;
        this.y = y;
    }





    public int getX()
    {
        return x;
    }

    public int getY()
    {
        return y;
    }

    public string AsString()
    {
        return x.ToString() + "," + y.ToString();
    }
}
