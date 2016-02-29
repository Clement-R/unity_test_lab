using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class Dodgers_GameManager : MonoBehaviour {
    public GameObject[] asteroids;
    public float timeBetweenAsteroid = 1.0f;

    private List<GameObject> inGameAsteroids = new List<GameObject> ();

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
            // Spawn an asteroid
            GameObject asteroid = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;

            inGameAsteroids.Add(asteroid);
            yield return new WaitForSeconds(timeBetweenAsteroid);
        }
    }
}
