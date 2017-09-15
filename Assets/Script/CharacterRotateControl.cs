using UnityEngine;
using System.Collections;

public class CharacterRotateControl : MonoBehaviour {

	private float needSwitchTime=1f;
	private Rigidbody2D rigid;
	private Collider2D collider;
	private void Start(){
		rigid = this.GetComponent<Rigidbody2D> ();
		collider = this.GetComponent<Collider2D> ();
	}
	public void SwitchDirection(Vector3 tar){
		Vector3 angle=Vector3.zero;


		if(tar== Vector3.up)
			angle = 180f*Vector3.forward;

		if(tar== Vector3.down)
			angle = 0f*Vector3.forward;

		if(tar== Vector3.left)
			angle = 270f*Vector3.forward;

		if(tar== Vector3.right)
			angle = 90f*Vector3.forward;


		StartCoroutine (RotateTo (angle));

	}
	IEnumerator RotateTo(Vector3 tar){
		rigid.AddForce (transform.up*3f);
		Vector3 fro = transform.localEulerAngles;
		float counter = 0f;
		while ( (transform.localEulerAngles-tar).sqrMagnitude > 5f){
			counter += Time.deltaTime;
			transform.localEulerAngles = Vector3.Lerp (fro, tar, counter / needSwitchTime);



			yield return 0;
		}

		this.transform.localEulerAngles = tar;
	}

}
