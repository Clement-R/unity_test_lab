using UnityEngine;
using System.Collections;

public class BuildableTileBehavior : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	void OnMouseDown() {
		Debug.Log ("Buildable");
		Debug.Log (this.transform.position.x);
		Debug.Log (this.transform.position.y);
		Vector3 position = new Vector3 (this.transform.position.x, this.transform.position.y, 0f);

		// Instantiates a prefab named "Tower" located in any Resources folder in the project's Assets folder.
		Instantiate(Resources.Load("Tower", typeof(GameObject)), position, Quaternion.identity);
	}
}
