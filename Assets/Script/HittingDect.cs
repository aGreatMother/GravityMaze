using UnityEngine;
using System.Collections;

public class HittingDect : MonoBehaviour {
	[HideInInspector]
	public bool hitting=false;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}
	void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.tag == "floor"||other.gameObject.tag == "bad") {
			hitting = false;
		}
	}
	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "floor"||other.gameObject.tag == "bad") {
			hitting = true;
		}
	}
	void OnTriggerStay2D(Collider2D other) {
		if (other.gameObject.tag == "floor"||other.gameObject.tag == "bad") {
			hitting = true;
		}
	}
}
