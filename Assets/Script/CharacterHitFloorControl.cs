using UnityEngine;
using System.Collections;

public class CharacterHitFloorControl : MonoBehaviour {
	private Animator anim;
	private CharacterDownDec downDec;
	private Rigidbody2D rigid;
	public bool floating=true;
	public AudioSource soundPlayer;
	private BoxCollider2D thisColl;
	private Vector2 originCollSize;
	// Use this for initialization
	void Start () {
		thisColl = this.GetComponent<BoxCollider2D> ();
		originCollSize = thisColl.size;
		anim = this.GetComponent<Animator> ();
		if(anim==null)
			anim = this.GetComponentInParent<Animator> ();
		downDec = this.GetComponentInChildren<CharacterDownDec> ();
		rigid = this.GetComponent<Rigidbody2D> ();
		if (soundPlayer == null)
			soundPlayer = Singleton<SoundContainer>.Instance.modyFoot;
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void HitFloor(){

		//this.thisColl.isTrigger = false;
		thisColl.size=originCollSize;
		anim.SetBool ("hitFloor",true);



	}
	public void LeaveFloor(){
		floating = true;
		downDec.coll = null;
		rigid.velocity=new Vector3 (rigid.velocity.x*GameControlManager.gStage.x,rigid.velocity.y*GameControlManager.gStage.y,0f);
		//this.thisColl.isTrigger = true;
		thisColl.size=new Vector2(downDec.collOnIit.size.x,originCollSize.y);
	
	}

	//for animation
	void SetHitFloorFalse(){
		floating = false;
		anim.SetBool ("hitFloor",false);
	}

	[Header("audio")]
	public AudioClip footStep1;
	public AudioClip footStep2;
	public AudioClip hitFloorSound;

	public void PlayFootStep1(){
		soundPlayer.clip = footStep1;
		soundPlayer.Play ();
	}
	public void PlayFootStep2(){
		soundPlayer.clip = footStep2;
		soundPlayer.Play ();
	}
	public void PlayHitFloor(){
		soundPlayer.clip = hitFloorSound;
		soundPlayer.Play ();
	}

	void OnCollisionExit2D(Collision2D other) {
		if (this.gameObject.name.StartsWith("oct")||(downDec.gameObject.GetComponent<Collider2D> ().IsTouching (other.collider)))
			return;
		if (!floating&&other.collider.gameObject.tag == "floor"&&downDec.coll==other.collider) {


			LeaveFloor ();


		}

	}



}
