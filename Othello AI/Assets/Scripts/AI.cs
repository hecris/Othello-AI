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

   public int Minimax(State s, int depth, bool maxPlayer)
    {
        if (depth == 0)
        {
            return e.evaluate2(s);
        }
        else
        {
            if (maxPlayer)
            {
                List<State> children = possibleChildren(s);
                int bestValue = -9999;
                foreach (State c in children)
                {
                   
                    int v = Minimax(c, depth - 1, false);
                    bestValue = Math.Max(v, bestValue);
                }
                return bestValue;
            }
            else
            {
                List<State> children = possibleChildren(s);
                int bestValue = 9999;
                foreach (State c in children)
                {
                    
                    int v = Minimax(c, depth - 1, true);
                    bestValue = Math.Min(v, bestValue);
                }
                return bestValue;
            }
        }
        
    }
    
    public Coordinate bestMove(State s, int depth)
    {
        
        int bestValue = Minimax(s, depth, true);
        Debug.Log(bestValue);
        List<Coordinate> moves = s.possibleMoves();
        List<State> children = possibleChildren(s);

        for (int i = 0; i < children.Count; i++)
        {
            int v = e.evaluate2(children[i]);
            if (v == bestValue)
            {
                return moves[i];
            }
        }
        return null;


    }

}

