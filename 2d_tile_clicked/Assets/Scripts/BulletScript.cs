using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

    public float speed = 10;

	private Vector3 targetPosition = Vector3.zero;

    void Start() {
        Destroy(gameObject, 2f);
    }

    // Update is called once per frame
    void Update () {
		if (targetPosition != Vector3.zero) {
			transform.position += targetPosition * speed * Time.deltaTime;
		}
	}

	public void setTargetPosition(Vector3 enemyPosition, Vector3 startPoint) {
		targetPosition = enemyPosition - startPoint;
	}

	void OnTriggerEnter2D(Collider2D collider) {
		GameObject colliderGameObject = collider.gameObject;
		if(colliderGameObject.tag == "Enemy") {
			Destroy(gameObject);
		}
	}
}
