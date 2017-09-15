using UnityEngine;
using System.Collections;

public class MovingObject : MonoBehaviour {

	public AudioClip hitSound;
	// Use this for initialization
	void Start () {
		for (int i = 0; i < transform.childCount; i++) {
			Transform child = transform.GetChild (i);
			if (child.GetComponent<MovingChild> () == null) {
				child.gameObject.AddComponent<MovingChild> ();
			}
				
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
