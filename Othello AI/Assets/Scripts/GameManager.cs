using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public GameObject[] prefabs = new GameObject[3];
	Transform parent;
	State s = new State();
	BoardEvaluator e = new BoardEvaluator();
	AI ai = new AI();
	int player;
	int computer;

	void displayBoard(){
		for (int i = 0; i < 8; i++) {
			for (int j = 0; j <8; j++){
				GameObject square;
				Vector2 position = new Vector2(j, 7-i);
				if (s.b.board[i,j] == -1){ // if square is black
					square = Instantiate (prefabs[2], parent);
					square.transform.position = position;
					square.name = i.ToString () + "," + j.ToString ();
					continue;
				}
				square = Instantiate (prefabs[s.b.board[i,j]], parent);
				square.transform.position = position;
				square.name = i.ToString () + "," + j.ToString ();
			}
		}

	}
	// Use this for initialization
	void Start () {
		player = -1;
		computer = 1;
		parent = GameObject.Find ("board").transform;

	}
	
	// Update is called once per frame
	void Update () {

	}



	void Move(State s, int turn){
		if (turn == computer) {
			displayBoard ();
			s = s.Place (ai.bestMove (s));
			Move (s, player);
		}
		displayBoard ();

	}
}
