using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerBehavior : MonoBehaviour {

	public float fireRate = 0.5f;

	private List<GameObject> enemiesInRange = new List<GameObject> ();
	private GameObject aimedEnemy = null;
	private float nextFire = 0.0f;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void FixedUpdate () {
		if (enemiesInRange.Count > 0) {
			// Check if enemy has not been killed before
			if(enemiesInRange[0].gameObject != null) {
				// Focus enemy that has enter the area first (FIFO list)
				// TODO : Make a way to change behavior
				Vector3 enemyPos = enemiesInRange[0].transform.position;
				transform.rotation = Quaternion.LookRotation(Vector3.forward, enemyPos - transform.position);

				if (Time.time > nextFire) {
					nextFire = Time.time + fireRate;

					// Instatiate a bullet and set its target position
					GameObject clone = Instantiate(Resources.Load("Bullet", typeof(GameObject)), transform.position, transform.rotation * Quaternion.Euler(0, 0, 90	)) as GameObject;

					BulletScript sc = clone.GetComponent<BulletScript> ();
					sc.setTargetPosition(enemyPos, transform.position);
				}
			} else {
				enemiesInRange.RemoveAt(0);
			}
		} else {
			transform.rotation = Quaternion.identity;
		}
	}

	void OnTriggerEnter2D(Collider2D enemy) {
		aimedEnemy = enemy.gameObject;
		enemiesInRange.Add(aimedEnemy);
    }

	void OnTriggerExit2D(Collider2D enemy) {
		aimedEnemy = enemy.gameObject;
		enemiesInRange.Remove(aimedEnemy);
		// TODO : Debug to ensure that the enemy removed is the one that's passed in param
		//        and not a random one. C#.
	}
}
