using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject[] prefabs = new GameObject[3];
    Transform parent;
    State s = new State();
    BoardEvaluator e = new BoardEvaluator();
    AI ai = new AI();
    int player;
    int computer;
	public int depth;
	public GameObject nWhite;
	public GameObject nBlack;

    void displayBoard()
    {
        foreach (Transform children in parent)
        {
            Destroy(children.gameObject);
        }
        for (int i = 0; i < 8; i++) {
			for (int j = 0; j < 8; j++) {
				GameObject square;
				Vector2 position = new Vector2 (j, 7 - i);
				if (s.b.board [i, j] == -1) { // if square is black
					square = Instantiate (prefabs [2], parent);
					square.transform.position = position;
					square.name = i.ToString () + "," + j.ToString ();
					continue;
				}
				square = Instantiate (prefabs [s.b.board [i, j]], parent);
				square.transform.position = position;
				square.name = i.ToString () + "," + j.ToString ();
			}
		}
		nWhite.GetComponent<Text>().text = "White: " + s.b.nWhite();
		nBlack.GetComponent<Text>().text = "Black: " + s.b.nBlack();

    }
    // Use this for initialization
    void Start()
    {
        player = -1;
        computer = 1;
        parent = GameObject.Find("board").transform;
        displayBoard();
        //Move(s, player);
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
		if (Input.GetMouseButtonDown(0)&& s.possibleMoves ().Count != 0)
		{
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
            if (hit.collider != null)
            {
                string move = hit.collider.name;
                Coordinate m = new Coordinate(move);
                if (s.isPossible(m))
                {
                    s = s.Place(m);
                    displayBoard();
                    yield return new WaitForSeconds(.1f);
                    s = ai.getBestState(s, depth);
                    displayBoard();
                }
            }
        }
		/*if (s.possibleMoves ().Count == 0) {
			s.player = -s.player;
            s = s.Place(ai.bestMove(s, depth));
            displayBoard();// switch turn if no possible moves for current turn
		}*/
	}
	
}
