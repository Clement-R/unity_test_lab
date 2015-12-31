using UnityEngine;
using System.Collections;

public class TowerBehavior : MonoBehaviour {

	private bool enemyInRange = false;
	private GameObject aimedEnemy = null;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void FixedUpdate () {
		if (enemyInRange) {
			Vector3 enemyPos = aimedEnemy.transform.position;
			transform.rotation = Quaternion.LookRotation(Vector3.forward, enemyPos - transform.position);
		}
	}

	void OnTriggerEnter2D(Collider2D enemy) {
		enemyInRange = true;
		aimedEnemy = enemy.gameObject;
    }

	void OnTriggerExit2D(Collider2D enemy) {
		if (enemy.gameObject == aimedEnemy) {
			enemyInRange = false;
			aimedEnemy = null;
		}
	}
}
