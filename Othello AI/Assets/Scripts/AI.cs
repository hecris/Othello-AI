using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class AI
{
    BoardEvaluator e = new BoardEvaluator();
    State state;
    public void setState(State s)
    {
        state = s;
    }
    public List<State> possibleChildren(State s)
    {
        List<State> children = new List<State>();
        List<Coordinate> moves = s.possibleMoves();
        foreach (Coordinate m in moves)
        {
            State copyState = new State(s);
            children.Add(copyState.Place(m));

        }
        return children;
    }
    List<int> values = new List<int>();
   public int Minimax(State s, int depth, bool maxPlayer, bool isFirst)
    {
        if (depth == 0)
        {
            return e.evaluate2(s);
        }
        else
        {
            List<State> children = possibleChildren(s);
            if (maxPlayer)
            {
                int bestValue = -9999;
                foreach (State c in children)
                {
                   
                    int v = Minimax(c, depth - 1, false, false);

                    bestValue = Math.Max(bestValue, v);
                    if (isFirst)
                    {
                        values.Clear();
                        values.Add(bestValue);
                    }
                }
                return bestValue;
            }
            else
            {
                int bestValue = 9999;
                foreach (State c in children)
                {
                    
                    int v = Minimax(c, depth - 1, true, false);
                    bestValue = Math.Min(bestValue, v);
                }
                return bestValue;
            }
        }
        
    }
    
    public State getBestState(State s, int depth)
    {
        int bestValue = Minimax(s, depth, true, true);
        int index = values.IndexOf(bestValue);
        Debug.Log(index);
        List<State> children = possibleChildren(s);
        return children[index];
    }
}

 