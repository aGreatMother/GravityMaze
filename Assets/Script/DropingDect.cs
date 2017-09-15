using UnityEngine;
using System.Collections;

public class DropingDect : MonoBehaviour {
	[HideInInspector]
	public bool droging=false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.tag == "floor") {
			droging = true;
		}
	}
	void OnTriggerStay2D(Collider2D other) {
		if (other.gameObject.tag == "floor") {
			droging = false;

		}
	}
}
