using UnityEngine;
using System.Collections;

public class MovingChild : MonoBehaviour {
	
	private AudioSource audioSource;
	// Use this for initialization
	void Start () {
		audioSource = this.GetComponent<AudioSource> ();
		if (audioSource == null)
		audioSource = this.gameObject.AddComponent<AudioSource> ();

		audioSource.clip = this.transform.parent.GetComponent<MovingObject> ().hitSound;
		audioSource.loop = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnCollisionEnter2D(Collision2D coll){
		if (coll.gameObject.tag == "floor"&&coll.relativeVelocity.magnitude>1f) {
			audioSource.volume = coll.relativeVelocity.magnitude / 6f;
			audioSource.Play ();
		}
	}
}
