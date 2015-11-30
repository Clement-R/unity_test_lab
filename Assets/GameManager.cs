using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public BoardManager boardScript;

    private int level = 1;

	// Use this for initialization
	void Start () {
        boardScript = GetComponent<BoardManager>();
        InitGame();
	}

    void InitGame() {
        boardScript.SetupScene(level);
    }
	
	// Update is called once per frame
	void Update () {
	    
	}
}
