using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class Dodgers_AsteroidBehavior : MonoBehaviour {
    public float fallSpeed;

    // Use this for initialization
    void Start () {
        fallSpeed = Random.Range(0.5f, 4f);
        Debug.Log(fallSpeed);
    }

    void FixedUpdate() {
        transform.Translate(Vector2.down * fallSpeed * Time.deltaTime);
    }
}
