using UnityEngine;
using System.Collections;

public class Dodgers_PlayerBehavior : MonoBehaviour {

	void OnTriggerEnter2D (Collider2D collider) {
        if (collider.gameObject.tag == "Enemy") {
            Debug.Log("Asteroid hit !");
        }
    }
}
