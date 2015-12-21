using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	public Vector2 velocity;
    private Rigidbody2D rb2D;

	// Use this for initialization
	void Start () {
		SpriteRenderer sr = GetComponent<SpriteRenderer>();
		Debug.Log (sr.sortingLayerName);
		rb2D = GetComponent<Rigidbody2D>();
	}

    void FixedUpdate() {
        rb2D.MovePosition(rb2D.position + velocity * Time.fixedDeltaTime);
    }

	void SearchForNextTile() {
	}

	void Move() {
	}
}
