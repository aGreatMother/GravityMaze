using UnityEngine;
using System.Collections;
using DG.Tweening;

public class AIbasic : MonoBehaviour {
	public DropingDect leftDropingDect;
	public DropingDect rightDropingDect;
	public HittingDect leftHittingDect;
	public HittingDect RightHittingDect;
	public EnemyAttackRange leftAttackRange;
	public EnemyAttackRange rightAttackRange;
	public AudioClip fart;
	public float movingSpeed=5f;
	private Rigidbody2D rigid;
	private CharacterHitFloorControl hitFloor;
	private Animator anim;
	private Collider2D coll;
	private Vector3 movingDrect = Vector3.right;
	// Use this for initialization
	void Start () {
		hitFloor = this.GetComponent<CharacterHitFloorControl> ();
		rigid = this.GetComponent<Rigidbody2D> ();
		anim = this.GetComponent<Animator> ();
		coll = this.GetComponent<Collider2D> ();
		hitFloor.floating = true;
	}

	// Update is called once per frame
	void Update () {
		if (!coll.IsTouchingLayers ()) {
			//double check
			hitFloor.floating = true;
		}
			
		if (Trandy.dead)
			return;
		DetectEnemy ();
		CheckMove ();
		MakeMove ();
		ShallStartAttack ();

	}

	void CheckMove(){
		if (hitFloor.floating) {
			anim.SetFloat ("direction", 0f);
			return;
		}
		Vector3 nextDrect = Vector3.zero;

		if (leftDropingDect.droging)
			nextDrect = Vector3.right;
		if (rightDropingDect.droging)
			nextDrect = Vector3.left;
		if (leftHittingDect.hitting)
			nextDrect = Vector3.right;
		if(RightHittingDect.hitting)
			nextDrect = Vector3.left;

		if (nextDrect != Vector3.zero) {
			movingDrect = nextDrect;
		}

	}
	void MakeMove(){
		if (hitFloor.floating||fartOnSence) {
			return;
		}
		float speed = movingSpeed;
		if (dectEnemy&&!gameObject.name.StartsWith("dommy")) {
			speed = movingSpeed*2f;
			anim.speed = 2f;
		}
		anim.SetFloat ("direction", movingDrect.x);
		if (movingDrect == Vector3.right)
			rigid.velocity = transform.right * speed;

		if (movingDrect == Vector3.left)
			rigid.velocity = -transform.right * speed;
	}
	private bool dectEnemy = false;
	void DetectEnemy(){
		Vector3 enemyDirec = Vector3.zero;
		LayerMask layerMask = LayerMask.GetMask ("acceptRay","floor");
		RaycastHit2D hit = Physics2D.Raycast(transform.position,transform.right,100,layerMask);
		if (hit.collider&& hit.collider.gameObject.name == "trandy"&&
			this.GetComponentInChildren<CharacterDownDec>().coll==hit.collider.gameObject.GetComponentInChildren<CharacterDownDec>().coll) {
			GameObject.FindObjectOfType<Trandy> ().hitFrom = Vector3.left;
			enemyDirec = Vector3.right;//on the right
			if(gameObject.name.StartsWith("dommy"))
			rightAttackRange.hitEnemy=true;
		}

		hit= Physics2D.Raycast(transform.position,-transform.right,100,layerMask);
		if (hit.collider&& hit.collider.gameObject.name == "trandy"&&
			this.GetComponentInChildren<CharacterDownDec>().coll==hit.collider.gameObject.GetComponentInChildren<CharacterDownDec>().coll){
			GameObject.FindObjectOfType<Trandy> ().hitFrom = Vector3.right;

			enemyDirec = Vector3.left;//on the left
			if(gameObject.name.StartsWith("dommy"))
			leftAttackRange.hitEnemy=true;
		}


		if (enemyDirec == Vector3.zero) {
			dectEnemy = false;

			anim.speed = 1f;
			if(gameObject.name.StartsWith("dommy")){
				leftAttackRange.hitEnemy=false;
				rightAttackRange.hitEnemy=false;
			}

			return;
		}
		dectEnemy = true;
		movingDrect = enemyDirec;





	}
	private Vector3 enemyDirec;
	void ShallStartAttack(){

		enemyDirec = Vector3.zero;

		if (leftAttackRange.hitEnemy)
			enemyDirec = Vector3.left;
		if (rightAttackRange.hitEnemy)
			enemyDirec = Vector3.right;

		if (!fartOnSence&& enemyDirec != Vector3.zero) {
			rigid.velocity = Vector3.zero;
			anim.SetFloat ("direction", 0f);
			anim.speed = 1f;
			anim.SetFloat ("attack", enemyDirec.x);
		}
	}

	public Vector3 getEnemyDirec(){
		return enemyDirec;
	}

	[Header("fart")]
	public GameObject fartPrefab = null;
	public Transform leftFartLocation;
	public Transform rightFartLocation;
	private GameObject fartOnSence=null;
	public void Fart(){
		if(fartOnSence==null){
			fartOnSence= GameObject.Instantiate(fartPrefab) ;
			if (enemyDirec == Vector3.right) {
				fartOnSence.transform.position = rightFartLocation.position;
				fartOnSence.transform.right = this.transform.right;

			}
			if (enemyDirec == Vector3.left) {
				fartOnSence.transform.position = leftFartLocation.position;
				fartOnSence.transform.right =-this.transform.right;

			}
			if (fartOnSence.transform.up != this.transform.up) {
				Vector3 formScale = fartOnSence.transform.localScale;
				fartOnSence.transform.localScale = new Vector3 (formScale.x,- formScale.y, formScale.z);
			}
			if (fartPrefab.name.StartsWith ("liquid")) {//if that the oct guy, the liquid can track to hero;
				fartOnSence.transform.DOMove(Singleton<Trandy>.Instance.transform.position,0.5f);
			}

			anim.SetFloat ("attack", 0f);
			Destroy (fartOnSence, 0.5f);

		}
	}

	public void FartSoundPlay(){
		this.GetComponent<CharacterHitFloorControl> ().soundPlayer.clip = fart;
		this.GetComponent<CharacterHitFloorControl> ().soundPlayer.Play ();
	}

	public void setFartOnscene(GameObject fartSub){
		fartOnSence = fartSub;
	}
	//for animation event
	public void Freeze(){
		
			rigid.isKinematic = true;

	}	
	//for animation event
	public void Move(){

		rigid.isKinematic = false;

	}

	void OnCollisionStay2D(Collision2D other) {
		if (other.gameObject.tag == "floor") {
		
		}
	}

}


