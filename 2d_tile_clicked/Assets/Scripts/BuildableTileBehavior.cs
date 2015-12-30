using UnityEngine;
using System.Collections;

public class BuildableTileBehavior : MonoBehaviour {

	private bool isFree = true;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	void OnMouseDown() {
		if (isFree) {
			Vector3 position = new Vector3 (this.transform.position.x, this.transform.position.y, 0f);
			
			// Instantiates a prefab named "Tower" located in any Resources folder in the project's Assets folder.
			Instantiate(Resources.Load("Tower", typeof(GameObject)), position, Quaternion.identity);

			isFree = false;
		}
	}

	public bool getIsFree() {
		return isFree;
	}
}
