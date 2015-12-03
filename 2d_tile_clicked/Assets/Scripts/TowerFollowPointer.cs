using UnityEngine;
using System.Collections;

public class TowerFollowPointer : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	void FixedUpdate() {
		Vector3 mousePos = Input.mousePosition;
		
		//To make mousePos relative to center of screen
		mousePos.x -= Screen.width / 2;
		mousePos.y -= Screen.height / 2;
		
		//To make mousePos relative to transform
		mousePos += transform.position;
		float angle = Vector3.Angle(mousePos, Vector3.up);
		
		//For 360 degree angle
		if (mousePos.x > 0)
			angle = 360 - angle;
		
		transform.rotation = Quaternion.Euler(0, 0, angle);
	}
}
