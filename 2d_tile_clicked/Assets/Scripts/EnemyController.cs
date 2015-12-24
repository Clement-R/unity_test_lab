using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	public Vector2 velocity;
    public LayerMask items;
    public float moveTime = 0.1f;

    private Rigidbody2D rb2D;
    private Transform exit;
    private float inverseMoveTime;
    private int[][] level;
    private BoardManager board;

    // Use this for initialization
    void Start () {
		SpriteRenderer sr = GetComponent<SpriteRenderer>();
		Debug.Log (sr.sortingLayerName);
		rb2D = GetComponent<Rigidbody2D>();
        inverseMoveTime = 1f / moveTime;

        board = GameObject.Find("BoardManager").GetComponent<BoardManager>();
        level = board.GetLevel();
    }

    void FixedUpdate() {
        // rb2D.MovePosition(rb2D.position + velocity * Time.fixedDeltaTime);

        if (exit == null) {
            exit = GameObject.FindGameObjectWithTag("Exit").transform;
        } else {
            RaycastHit2D hit;

            int xDir = 0;
            int yDir = 0;

            // Debug.Log("Exit - X: " + exit.position.x + " ; Y: " + exit.position.y);


            // if (Mathf.Abs(exit.position.x - transform.position.x) < float.Epsilon)
            //     yDir = exit.position.y > transform.position.y ? 1 : -1;
            // else
            //     xDir = exit.position.x > transform.position.x ? 1 : -1;

            // bool canMove = Move(xDir, yDir, out hit);
            // Move(xDir, yDir, out hit);
        }
    }

    // GameObject SearchNearestTile() {

    // }

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
