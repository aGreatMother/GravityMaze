using UnityEngine;
using System.Collections;

public class CharacterDownDec : MonoBehaviour {

	private CharacterHitFloorControl hero;
	[HideInInspector]
	public BoxCollider2D collOnIit;
	private Transform rayCasterL;
	private Transform rayCasterR;

	// Use this for initialization
	void Start () {
		hero = this.GetComponentInParent<CharacterHitFloorControl> ();
		collOnIit = this.gameObject.GetComponent<BoxCollider2D> ();
		if (hero == null)
			hero = transform.parent.GetComponentInChildren<CharacterHitFloorControl> ();
		
		rayCasterL = transform.FindChild ("rayCasterL");
		rayCasterR = transform.FindChild ("rayCasterR");
	}
	
	// Update is called once per frame
	void Update () {

	
	}
	[HideInInspector]
	public Collider2D coll;
	void OnTriggerStay2D(Collider2D other) {
		

		if (hero.floating&&other.gameObject.tag == "floor"&&coll!=other&&transform.up==-GameControlManager.gStage&&other==GetTargetRayCastHit().collider) {
			Rigidbody2D otherRigid = other.gameObject.transform.parent.gameObject.name=="move"?other.transform.parent.gameObject.GetComponent<Rigidbody2D> ():null;
			if (otherRigid!=null&&otherRigid.velocity.sqrMagnitude > 0.01f)
				return;

			hero.HitFloor ();

			coll =other;
		}



	}





	public  RaycastHit2D GetTargetRayCastHit(){
		LayerMask layerMask = LayerMask.GetMask ("floor");

		RaycastHit2D result = Physics2D.Raycast(rayCasterL.position,GameControlManager.gStage,100,layerMask);
		Vector3 resultPoint = rayCasterL.position;


		Vector3 checkingPoint = rayCasterL.position;
		Vector3 unit = (rayCasterR.position - rayCasterL.position)/100f;

		for (int i = 0; i<100;i++) {
			checkingPoint += unit;
			RaycastHit2D temp = Physics2D.Raycast(checkingPoint,GameControlManager.gStage,100,layerMask);

			if (((Vector3)result.point - resultPoint).sqrMagnitude > ((Vector3)temp.point - checkingPoint).sqrMagnitude) {
				result = temp;
			}
		}



		return result;



	} 

}
