using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class Dodgers_PlayerController : MonoBehaviour {
    public float speed = 30.0f;
    private Rigidbody2D rb2d;

    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
    }

	void FixedUpdate () {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        rb2d.velocity = new Vector2(moveHorizontal * speed, moveVertical * speed);
    }

    void OnTriggerEnter2D (Collider2D collider) {
        if (collider.gameObject.tag == "Enemy") {
            Debug.Log("Asteroid hit !");
        }
    }
}
