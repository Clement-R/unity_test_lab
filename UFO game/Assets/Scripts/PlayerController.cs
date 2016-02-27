using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    private Rigidbody2D rb2d;
    public float speed = 10.0f;

    void Start() {
        rb2d = GetComponent<Rigidbody2D>();
    }

	void FixedUpdate () {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        rb2d.AddForce(movement * speed);
	}
    
    void OnTriggerEnter2D(Collider2D other) {
    if (other.gameObject.CompareTag("PickUp")) {
            other.gameObject.SetActive(false);
        }
    }
}
