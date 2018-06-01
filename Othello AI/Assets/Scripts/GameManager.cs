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
		for (int i = 7; i >= 0; i--) {
			for (int j = 7; j >= 0; j--){
				GameObject square;
				Vector2 position = new Vector2(i, j);
				if (s.b.board[i,j] == -1){ // if square is black
					square = Instantiate (prefabs[2], parent);
					square.transform.position = position;
					square.name = position.ToString ();
					continue;
				}
				square = Instantiate (prefabs[s.b.board[i,j]], parent);
				square.transform.position = position;
				square.name = position.ToString ();
			}
		}

	}
	// Use this for initialization
	void Start () {
		player = -1;
		computer = 1;
		parent = GameObject.Find ("board").transform;
		displayBoard ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
