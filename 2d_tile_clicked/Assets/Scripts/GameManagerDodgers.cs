using UnityEngine;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class GameManagerDodgers : MonoBehaviour {
    public GameObject[] asteroids;
    private List<GameObject> inGameAsteroids = new List<GameObject> ();

    // Use this for initialization
	void Start () {
        Debug.Log(asteroids);
        LaunchAsteroid();
        Debug.Log(inGameAsteroids);
	}

	// Update is called once per frame
	void Update () {

	}

    void LaunchAsteroid() {
        // TODO : Let asteroids come from all sides of the screen

        // x between 7 and -7
        float x = Random.Range (-7, 7);
        // y fixed to 8
        float y = 8f;

        GameObject toInstantiate = asteroids[Random.Range (0, asteroids.Length)];
        GameObject asteroid = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;
        Debug.Log(asteroid);
        inGameAsteroids.Add(asteroid);
    }
}
