﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerBehavior : MonoBehaviour {

	private bool enemyInRange = false;
	private List<GameObject> enemiesInRange = new List<GameObject> ();
	private GameObject aimedEnemy = null;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void FixedUpdate () {
		if (enemiesInRange.Count > 0) {
			// Focus enemy that has enter the area first (FIFO list)
			// TODO : Make a way to change behavior
			Vector3 enemyPos = enemiesInRange[0].transform.position;
			transform.rotation = Quaternion.LookRotation(Vector3.forward, enemyPos - transform.position);
		}
	}

	void OnTriggerEnter2D(Collider2D enemy) {
		enemyInRange = true;
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
