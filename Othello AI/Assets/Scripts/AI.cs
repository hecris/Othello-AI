using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public int[] Minimax(State s, int depth)
        {
            List<State> children = possibleChildren(s);
            int[] values = new int[children.Count];
            if (depth == 0)
            {
                for (int i = 0; i < children.Count; i++)
                {
                    values[i] = e.evaluate(children[i]);
                }
                return values;
            }

            else
            {
                for (int i = 0; i < children.Count; i++)
                {
                    int[] placeHolder = Minimax(children[i], depth - 1);
                    if (depth % 2 != 0)
                    {
                        int max = placeHolder.Max();

                        values[i] = max;
                    }
                    else
                    {
                        int min = placeHolder.Min();
                        values[i] = min;
                    }


                }
            }
            return values;
        }

        public Coordinate bestMove(State s)
        {
            List<Coordinate> moves = s.possibleMoves();
            int[] values = Minimax(s, 4);
            int max = values.Max();
            int ind = values.ToList().IndexOf(max);
            return (moves[ind]);
        }

    }

