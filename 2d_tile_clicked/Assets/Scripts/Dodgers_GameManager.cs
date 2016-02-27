using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class Dodgers_GameManager : MonoBehaviour {
    public GameObject[] asteroids;
    public float fallSpeed = 2f;
    private List<GameObject> inGameAsteroids = new List<GameObject> ();
    private float timeBetweenAsteroid = 2.0f;

    // Use this for initialization
	void Start () {
        StartCoroutine(LaunchAsteroids());
	}
    
    IEnumerator LaunchAsteroids() {
        while (true) {
            // TODO : Let asteroids come from all sides of the screen

            // x between 7 and -7
            float x = Random.Range(-7, 7);
            // y fixed to 8
            float y = 8f;

            GameObject toInstantiate = asteroids[Random.Range(0, asteroids.Length)];
            GameObject asteroid = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;

            Rigidbody2D rb2 = asteroid.GetComponent<Rigidbody2D>();
            rb2.velocity = new Vector2(0f, -fallSpeed);
            inGameAsteroids.Add(asteroid);
            yield return new WaitForSeconds(timeBetweenAsteroid);
        }
    }

    // Update is called once per frame
    void Update () {

	}
}
