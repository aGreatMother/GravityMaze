using UnityEngine;
using System.Collections;

public class EnemyAttackRange : MonoBehaviour {

	// Use this for initialization
	[HideInInspector]
	public bool hitEnemy=false;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerStay2D(Collider2D other) {
		if (gameObject.name.StartsWith ("dommy"))
			return;
		if (other.gameObject.name == "trandy") {
			hitEnemy = true;
		}
	}
	void OnTriggerExit2D(Collider2D other) {
		if (gameObject.name.StartsWith ("dommy"))
			return;
		if (other.gameObject.name == "trandy") {
			hitEnemy = false;
		}
	}



}
