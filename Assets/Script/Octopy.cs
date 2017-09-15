using UnityEngine;
using System.Collections;

public class Octopy : MonoBehaviour {
	private Rigidbody2D rigid;
	// Use this for initialization
	void Start () {
		this.GetComponent<CharacterHitFloorControl> ().floating = false;
		rigid = this.GetComponent<Rigidbody2D> ();

	}

	void OnCollisionEnter2D(Collision2D other) {
		if ( this.GetComponentInChildren<CharacterDownDec> ().coll==null&&other.gameObject.tag == "floor") {
			this.GetComponentInChildren<CharacterDownDec> ().coll = other.collider;
			this.GetComponent<CharacterHitFloorControl> ().floating = false;

		}
	}
	
	// Update is called once per frame
	void Update () {
		rigid.AddForce (-transform.up * 10f);
	}
}
