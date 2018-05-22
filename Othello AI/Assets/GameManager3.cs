using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GameManager3 : MonoBehaviour {

   
    public GameObject[] squarePrefabs = new GameObject[5];
    GameObject boardObj;
    Coordinate[] directions = new Coordinate[8]
        {
            new Coordinate (0, 1), new Coordinate (0, -1), new Coordinate (1, 0), new Coordinate (-1, 0), new Coordinate (1,1), new Coordinate(-1, 1), new Coordinate (1, -1), new Coordinate(-1,-1)
        };

    int[,] board = new int[8, 8] {
        { 0,  0,  0,  0,  0,  0,  0,  0},
        { 0,  0,  0,  0,  0,  0,  0,  0},
        { 0,  0,  0,  0,  0,  0,  0,  0},
        { 0,  0,  0,  2,  1,  0,  0,  0},
        { 0,  0,  0,  1,  2,  0,  0,  0},
        { 0,  0,  0,  0,  0,  0,  0,  0},
        { 0,  0,  0,  0,  0,  0,  0,  0},
        { 0,  0,  0,  0,  0,  0,  0,  0}
    };

    int player, ai, empty = 0, white = 1, black = 2;

    Text nWhite;
    Text nBlack;

    void Start()
    { 
        player = black;
        ai = OppositeOf(player);
        boardObj = new GameObject("board");
        refreshBoard();
        nWhite = GameObject.Find("white").GetComponent<Text>();
        nBlack = GameObject.Find("black").GetComponent<Text>();
        Screen.SetResolution(800, 600, false);
    }


    void Update()
    {
        ShowPieceCount();
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
            if (hit.collider != null)
            {
                int r = int.Parse(hit.collider.name.Split(',')[0]);
                int c = int.Parse(hit.collider.name.Split(',')[1]);
                if (Place(r, c, player))
                {
                    
                    StartCoroutine(aiPlace());                 

                }       
            }
        }
    }

  void ShowPieceCount()
    {
        int w = 0;
        int b = 0;
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (board[i,j] == white)
                {
                    w += 1;
                }
                else if (board[i,j] == black)
                {
                    b += 1;
                }
            }
        }
        nWhite.text = "White: " + w.ToString();
        nBlack.text = "Black: " + b.ToString();
    }



    void refreshBoard()
    {
        
        // delete board
        foreach (Transform child in boardObj.transform)
        {
            Destroy(child.gameObject);
        }

        //create board
        GameObject squareInstance;
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                Vector2 squarePosition = new Vector2(i, j);
                squareInstance = Instantiate(squarePrefabs[board[i, j]], boardObj.transform);
                squareInstance.transform.position = squarePosition;
                squareInstance.name = i.ToString() + "," + j.ToString();

            }
        }
    }



    List<Coordinate> possibleMoves(int color)
    {
        List<Coordinate> possible = new List<Coordinate>();
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                List<Coordinate> captures = CaptureAll(i, j, color);
                if (captures.Count > 0 && board[i,j] == empty)
                {
                    possible.Add(new Coordinate(i, j));
                    
                }
            }
        }
        return possible;
    }

    bool Place(int r, int c, int color)
    {
        List<Coordinate> captures = CaptureAll(r, c, color);
        if (captures.Count > 0)
        {
            board[r, c] = color;
            foreach (Coordinate coor in captures)
            {
                board[coor.getX(), coor.getY()] = color;
            }
            Debug.Log(color + ":    " + r + "," + c);
            return true;
        }
        else
        {
            return false;
        }
        
    }

    IEnumerator aiPlace()
    {
        refreshBoard();
        yield return new WaitForSeconds(1);
        List<Coordinate> moves = new List<Coordinate>();
        List<int> values = new List<int>();
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                int captures = CaptureAll(i, j, ai).Count;
                if (captures > 0 && board[i,j] == empty)
                {
                    moves.Add(new Coordinate(i, j));
                    values.Add(captures);
                }
            }
        }
        
        Coordinate bestMove;
        int max = values.IndexOf(values.Max());
        bestMove = moves[max];
        Place(bestMove.getX(), bestMove.getY(), ai);
        refreshBoard();
        
    }


    List<Coordinate> CaptureAll(int r, int c, int color)
    {
        List<Coordinate> p = new List<Coordinate>();
        
        foreach (Coordinate d in directions)
        {
            p.AddRange(piecesToCapture(r, c, d, color));
        }
        int n = p.Count;
        return p;
    }



    List<Coordinate> piecesToCapture(int r, int c, Coordinate d, int color)
    {
        List<Coordinate> piecesToCapture = new List<Coordinate>();
        int dr = d.getX();
        int dc = d.getY();
        if (isOnBoard(r + dr, c + dc) && board[r + dr, c + dc] == OppositeOf(color))
        {
            int i = 1;
            bool b = true;

            while (b)
            {
                if (isOnBoard(r + dr * i, c + dc * i))
                {
                    if (board[r + dr * i, c + dc * i] == OppositeOf(color))
                    {
                        if (i < 8)
                        {
                            piecesToCapture.Add(new Coordinate(r + dr * i, c + dc * i));
                            i++;
                        }
                        else
                        {
                            b = false;
                        }

                    }
                    else
                    {

                        b = false;

                        if (board[r + dr * i, c + dc * i] == color)
                        {
                        }
                        else if (board[r + dr * i, c + dc * i] == empty)
                        {
                            return new List<Coordinate>();
                        }



                    }
                }
                else
                {
                    b = false;
                    return new List<Coordinate>();
                }

            }
        }
        return piecesToCapture; 


    }

    static bool isOnBoard(int r, int c)
    {
        return (0 < r && r < 7 && 0 < c && c < 8);
    }




    static int OppositeOf(int color)
    {
        if (color == 1)
        {
            return 2;
        }
        else
        {
            return 1;
        }
    }
}
