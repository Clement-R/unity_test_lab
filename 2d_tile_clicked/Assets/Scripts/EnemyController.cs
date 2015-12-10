using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		SpriteRenderer sr = GetComponent<SpriteRenderer>();
		Debug.Log (sr.sortingLayerName);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
