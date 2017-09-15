using UnityEngine;
using System.Collections;

public class Trandy : MonoBehaviour {
	public static MovingDiraction movingDrc=MovingDiraction.df;
	private Animator anim;
	private Rigidbody2D rigid;
	private CharacterHitFloorControl hitFloor;
	[HideInInspector]
	static public bool dead=false;
	private GameControlManager gameControl;
	// Use this for initialization
	void Awake () {
		dead = false;
		rigid = this.GetComponent<Rigidbody2D> ();
		rigid.centerOfMass = Vector3.down;
		anim = this.GetComponent<Animator> ();
		gameControl = GameObject.FindObjectOfType<GameControlManager> ();
		hitFloor = this.GetComponent<CharacterHitFloorControl> (); 


		hitFloor.floating = true;
	}
	
	// Update is called once per frame
	void Update () {
		ReactToInput ();
		gameControl.CheckMovingUI ();
	}

	void ReactToInput(){
		if (hitFloor.floating){//cant move when flying
			anim.SetFloat ("direction", 0f);
			return;}
		Vector3 givenVector = Vector3.zero;
		anim.SetFloat ("direction", 0f);

		movingDrc = MovingDiraction.df;
		//NOTE defined by platform
		if (Input.GetKey ("w"))
			movingDrc = MovingDiraction.up;
		else if (Input.GetKey ("s"))
			movingDrc = MovingDiraction.down;
		else if (Input.GetKey ("a"))
			movingDrc = MovingDiraction.left;
		else if (Input.GetKey ("d"))
			movingDrc = MovingDiraction.right;
		else
			movingDrc = MovingDiraction.df;
		
			

		switch (movingDrc) {

		case MovingDiraction.up:
			givenVector = Vector3.up;
			break;
		case MovingDiraction.down:
			givenVector = Vector3.down;
			break;
		case MovingDiraction.left:
			givenVector = Vector3.left;
			break;
		case MovingDiraction.right:
			givenVector = Vector3.right;
			break;
		
		}
		if (givenVector != Vector3.zero&&givenVector !=transform.up&&givenVector !=-transform.up) {
			if (transform.right == givenVector)
				anim.SetFloat ("direction", 1f);
			if (transform.right == -givenVector)
				anim.SetFloat ("direction", -1f);
			rigid.velocity = givenVector * 10f;
			return;
		}
		if (!hitFloor. floating)
			rigid.velocity = Vector3.zero;
		//when the buttons 

	}
	public bool GetFloating(){
		return hitFloor.floating;
	}
	[HideInInspector]
	public Vector3 hitFrom=Vector3.zero;
	//NOTE enter the danger area
	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "danger") {
			if (hitFrom == Vector3.right) {
				anim.Play ("dropLeft");
			} else {
				anim.Play ("dropRight");
			}
			dead = true;
			rigid.velocity = Vector3.zero;
			this.enabled = false;
			Singleton<GameControlManager>.Instance.enabled = false;
		}
	}




}
