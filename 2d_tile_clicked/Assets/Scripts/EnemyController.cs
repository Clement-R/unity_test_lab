using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyController : MonoBehaviour {

	public Vector2 velocity;
    public LayerMask items;
    public float moveTime = 0.1f;

    private Rigidbody2D rb2D;
    private Transform exit;
    private float inverseMoveTime;
    private int[][] level;
    private BoardManager board;
	private List<Dictionary<string, int>>  path;

    // Use this for initialization
    void Start () {
		SpriteRenderer sr = GetComponent<SpriteRenderer>();
		Debug.Log (sr.sortingLayerName);
		rb2D = GetComponent<Rigidbody2D>();
        inverseMoveTime = 1f / moveTime;

        board = GameObject.Find("BoardManager").GetComponent<BoardManager>();
        level = board.GetLevel();
		exit = GameObject.Find("Exit").transform;

		path = new List<Dictionary<string, int>>() ;
		path = SearchPathToExit(exit);

		Debug.Break();
    }

    void FixedUpdate() {
        // rb2D.MovePosition(rb2D.position + velocity * Time.fixedDeltaTime);

		/*
        RaycastHit2D hit;

        int xDir = 0;
        int yDir = 0;
		*/
        // bool canMove = Move(xDir, yDir, out hit);
		/*
		Dictionary<string, int> nearest = SearchNearestTile();

		Debug.Log ("Chosen tile : X : " + nearest["x"] + " ; Y : " + nearest["y"]);

		Move(nearest["x"], nearest["y"], out hit);
		*/
    }

	List<Dictionary<string, int>> SearchPathToExit(Transform exit) {
		List<Dictionary<string, int>> pathToExit = new List<Dictionary<string, int>>();

		bool pathFound = false;

		int x = Mathf.FloorToInt(transform.position.x);
		int y = Mathf.FloorToInt(transform.position.y);

		while (pathFound == false) {
			Dictionary<string, int> tile = SearchNearestTile(x, y);

			// Add tile to path
			pathToExit.Add(tile);

			// Change x and y to pos of nearest tile
			// x = x + tile["x"];
			// y = y + tile["y"];

			// If x and y are equal to exit pos, we stop the research
			pathFound = true;
		}

		return pathToExit;
	}

	Dictionary<string, int> SearchNearestTile(int x, int y) {
		// Search if exit is above or under current position, if so go up or down following position
		// If can't do that (being in the border of the map, or no tile upper that's accessible
		// we look on right or left (50% chance). If we haven't found a tile we search if we can go down.

		bool move = false;
		int xDir = 0;
		int yDir = 0;

		Debug.Log ("X : " + x + "; Y : " + y);

		if (transform.position.y < exit.position.y) {
			// We are under the exit
			yDir = 1;
		} else {
			// We are above the exit
			yDir = -1;
		}

		Debug.Log (yDir);

		// Check if tile in front of enemy is walkable
		if (yDir != 0) {
			// Check if we don't go out of bounds
			if((y + yDir) < board.GetRows() && (y + yDir) >= 0) {
				// Check if walkable
				if(level[x][y + yDir] == 0) {
					Debug.Log ("was here");
					move = true;
				}
			}
		}
		/*
		int z = y + yDir;
		Debug.Log ("X : " + x + "; Y : " + z);
		Debug.Log (level [x] [z]);
		*/
		Debug.Log (level [x] [y + yDir]);
		Debug.Log (move);
		Debug.Break ();

		// If we can't go forward, we check if we can go on the sides
		if (move == false) {
			if (x < exit.position.x) {
				if ((transform.position.x + 1) <= (board.GetColumns() - 1)) {
					move = true;
					xDir = 1;
				}
			} else {
				if ((x - 1) >= 0) {
					move = true;
					xDir = -1;
				}
			}
		}

		// We search if we can go backward
		if (move == false) {
		}

		if (move == true) {
			Dictionary<string, int> nearestTile = new Dictionary<string, int>();

			if (yDir != 0) {
				nearestTile.Add("x", x);
				nearestTile.Add("y", y + yDir);
			} else if (xDir != 0) {
				nearestTile.Add("x", x + xDir);
				nearestTile.Add("y", y);
			} else {
				nearestTile.Add("x", x);
				nearestTile.Add("y", y);
			}
			
			return nearestTile;
		}

		return null;
    }

    //Move returns true if it is able to move and false if not.
    //Move takes parameters for x direction, y direction and a RaycastHit2D to check collision.
    protected bool Move(int xDir, int yDir, out RaycastHit2D hit) {
        //Store start position to move from, based on objects current transform position.
        Vector2 start = transform.position;

        // Calculate end position based on the direction parameters passed in when calling Move.
        Vector2 end = start + new Vector2(xDir, yDir);

        //Disable the boxCollider so that linecast doesn't hit this object's own collider.
        // boxCollider.enabled = false;

        //Cast a line from start point to end point checking collision on blockingLayer.
        hit = Physics2D.Linecast(start, end, items);

        //Re-enable boxCollider after linecast
        // boxCollider.enabled = true;

        //Check if anything was hit
        if (hit.transform == null) {
            //If nothing was hit, start SmoothMovement co-routine passing in the Vector2 end as destination
            StartCoroutine(SmoothMovement(end));

            //Return true to say that Move was successful
            return true;
        }

        //If something was hit, return false, Move was unsuccesful.
        return false;
    }

    //Co-routine for moving units from one space to next, takes a parameter end to specify where to move to.
    protected IEnumerator SmoothMovement(Vector3 end) {
        //Calculate the remaining distance to move based on the square magnitude of the difference between current position and end parameter.
        //Square magnitude is used instead of magnitude because it's computationally cheaper.
        float sqrRemainingDistance = (transform.position - end).sqrMagnitude;

        //While that distance is greater than a very small amount (Epsilon, almost zero):
        while (sqrRemainingDistance > float.Epsilon) {
            //Find a new position proportionally closer to the end, based on the moveTime
            Vector3 newPostion = Vector3.MoveTowards(rb2D.position, end, inverseMoveTime * Time.deltaTime);

            //Call MovePosition on attached Rigidbody2D and move it to the calculated position.
            rb2D.MovePosition(newPostion);

            //Recalculate the remaining distance after moving.
            sqrRemainingDistance = (transform.position - end).sqrMagnitude;

            //Return and loop until sqrRemainingDistance is close enough to zero to end the function
            yield return null;
        }
    }
}
