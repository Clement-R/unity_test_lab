using UnityEngine;
using System.Collections;

public class BuildableTileBehavior : MonoBehaviour {

	private bool isFree = true;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0))
		{
			Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			RaycastHit2D[] hits = Physics2D.RaycastAll(mousePos, Vector2.zero, Mathf.Infinity);
			GameObject selectedObject = null;

			if(hits.Length > 0) {
				foreach(RaycastHit2D hit in hits) {
					selectedObject = hit.collider.gameObject;
					Debug.Log (selectedObject.name);
					/*
					if(selectedObject.tag == "BuildableWall" && hit.transform == selectedObject.transform) {
						BuildIfFree();
					}
					*/
				}
			}
		}
		
		/*
		if (Input.GetMouseButtonDown (0)) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit)) {
				Debug.Log ("Name = " + hit.collider.name);
				Debug.Log ("Tag = " + hit.collider.tag);
				Debug.Log ("Hit Point = " + hit.point);
				Debug.Log ("Object position = " + hit.collider.gameObject.transform.position);
				Debug.Log ("--------------");
			}
		}
		*/
	}

	void BuildIfFree() {
		if (isFree) {
			Vector3 position = new Vector3 (this.transform.position.x, this.transform.position.y, 0f);
			
			// Instantiates a prefab named "Tower" located in any Resources folder in the project's Assets folder.
			Instantiate (Resources.Load ("Tower", typeof(GameObject)), position, Quaternion.identity);
			
			isFree = false;
		}
	}

	void OnMouseDown() {
	}

	public bool getIsFree() {
		return isFree;
	}
}
