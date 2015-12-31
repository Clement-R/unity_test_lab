using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;

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
    private int pathPos = 0;
    private bool canMove = true;

    // Use this for initialization
    void Start () {
		rb2D = GetComponent<Rigidbody2D>();
        inverseMoveTime = 1f / moveTime;

        board = GameObject.Find("BoardManager").GetComponent<BoardManager>();
        level = board.GetLevel();
        exit = GameObject.Find("Exit").transform;

		path = new List<Dictionary<string, int>>() ;
		path = SearchPathToExit(exit);
    }

    void FixedUpdate() {
        if (transform.position != exit.position) {
            if (canMove) {
                Dictionary<string, int> tile = path[pathPos];

                Move(tile["x"], tile["y"]);
                pathPos++;
            }
        }
    }

    List<Dictionary<string, int>> SearchPathToExit(Transform exit) {
		List<Dictionary<string, int>> pathToExit = new List<Dictionary<string, int>>();

		bool pathFound = false;

		int x = Mathf.FloorToInt(transform.position.x);
		int y = Mathf.FloorToInt(transform.position.y);

        Dictionary<string, int> previousTile = null;
        while (pathFound == false) {
            if (previousTile == null) {
                previousTile = new Dictionary<string, int>();
                previousTile.Add("x", x);
                previousTile.Add("y", y);
            }

            Dictionary<string, int> tile = SearchNearestTile(x, y, previousTile);

            // Add tile to path
            pathToExit.Add(tile);

            // Set previous tile to previous pos
            previousTile["x"] = x;
            previousTile["y"] = y;

            // If x and y are equal to exit pos, we stop the research
            if (x == exit.position.x && y == exit.position.y) {
                pathFound = true;
            }

            // Change x and y to pos of nearest tile
            if (tile != null) {
                x = tile["x"];
                y = tile["y"];
            } else {
                // Error case
                // TODO : manage if enemy is blocked ...
                pathFound = true;
            }
		}
        
		return pathToExit;
	}

	Dictionary<string, int> SearchNearestTile(int x, int y, Dictionary<string, int> previousTile) {
		// Search if exit is above or under current position, if so go up or down following position
		// If can't do that (being in the border of the map, or no tile upper that's accessible
		// we look on right or left (50% chance). If we haven't found a tile we search if we can go down.
		int xDir = 0;
		int yDir = 0;

        Dictionary<string, int> nearestTile = new Dictionary<string, int>();

        /*
        Debug.Log("X : " + x + "; Y : " + y);
        Debug.Log(" VALUE -> " + level[y][x]);
        */

        if (transform.position.y < exit.position.y) {
			// We are under the exit
			yDir = 1;
		} else {
			// We are above the exit
			yDir = -1;
		}

        if (transform.position.x < exit.position.x) {
            // We are on the left of the exit
            xDir = 1;
        }
        else {
            // We are on the right of the exit
            xDir = -1;
        }

		// Check if tile in front of enemy is walkable
		if (yDir != 0) {
			// Check if we don't go out of bounds
			if((y + yDir) < level.Length && (y + yDir) >= 0) {
				// Check if walkable and not previous tile
				if(level[y + yDir][x] == 0 || level[y + yDir][x] == 3) {
                    // Check if we don't go on previous tile
                    if(y + yDir != previousTile["y"]) {
                        nearestTile.Add("x", x);
                        nearestTile.Add("y", y + yDir);
                        return nearestTile;
                    }
				}
			}
		}
        
        // Check right
        if (xDir != 0) {
            if ((x + 1) < level.Length && (x + 1) >= 0) {
                // Check if walkable
                if (level[y][x + 1] == 0 || level[y][x + 1] == 3) {
                    // Check if we don't go on previous tile
                    if (x + 1 != previousTile["x"]) {
                        nearestTile.Add("x", x + 1);
                        nearestTile.Add("y", y);
                        return nearestTile;
                    }
                }
            }
        }

        // We search if we can go backward
        if (yDir != 0) {
            // Check if we don't go out of bounds
            if ((y - yDir) < level.Length && (y - yDir) >= 0) {
                // Check if walkable
                if (level[y - yDir][x] == 0 || level[y - yDir][x] == 3) {
                    // Check if we don't go on previous tile
                    if (y - yDir != previousTile["y"]) {
                        nearestTile.Add("x", x);
                        nearestTile.Add("y", y - yDir);
                        return nearestTile;
                    }
                }
            }
        }

        // Check left
        if (xDir != 0) {
            if ((x - 1) < level.Length && (x - 1) >= 0) {
                // Check if walkable
                if (level[y][x - 1] == 0 || level[y][x - 1] == 3) {
                    // Check if we don't go on previous tile
                    if (x - 1 != previousTile["x"]) {
                        nearestTile.Add("x", x - 1);
                        nearestTile.Add("y", y);
                        return nearestTile;
                    }
                }
            }
        }

        // We can't find a walkable tile
        return null;
    }

    protected void Move(int xDir, int yDir) {
        canMove = false;
        
		// Calculate end position based on the direction parameters passed in when calling Move.
        Vector2 end = new Vector2(xDir, yDir);

        StartCoroutine(SmoothMovement(end));
    }

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
        canMove = true;
    }
}
