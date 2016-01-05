using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

    public float speed = 10;

    void Start() {
        Destroy(gameObject, 2f);
    }

    // Update is called once per frame
    void Update () {
        transform.Translate(0, speed * Time.deltaTime, 0);
	}
}
