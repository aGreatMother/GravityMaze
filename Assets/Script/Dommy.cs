using UnityEngine;
using System.Collections;
using DG.Tweening;
public class Dommy : MonoBehaviour {


	private Animator anim;
	private AIbasic basic;
	// Use this for initialization
	void Start () {
		anim = this.GetComponent<Animator> ();
		basic = this.GetComponent<AIbasic> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	[Header("lazer")]
	public GameObject lazerPrefab = null;
	public Transform leftlazerLocation;
	public Transform rightlazerLocation;
	private GameObject lazerOnSence=null;
	public void lazer(){
		if(lazerOnSence==null){
			LayerMask layerMask = LayerMask.GetMask ("floor");
			RaycastHit2D hit;
			lazerOnSence= GameObject.Instantiate(lazerPrefab) ;
			float longth=0f;//the longth of lazer
			if (basic.getEnemyDirec() == Vector3.right) {
				hit = Physics2D.Raycast(transform.position,transform.right,100,layerMask);
				lazerOnSence.transform.position = rightlazerLocation.position;
				lazerOnSence.transform.right = this.transform.right;
				longth = ((Vector3)hit.point - rightlazerLocation.position).magnitude;

			}
			if (basic.getEnemyDirec() == Vector3.left) {
				hit = Physics2D.Raycast(transform.position,-transform.right,100,layerMask);
				lazerOnSence.transform.position = leftlazerLocation.position;
				lazerOnSence.transform.right =-this.transform.right;
				lazerOnSence.transform.position = leftlazerLocation.position;
				longth = ((Vector3)hit.point - leftlazerLocation.position).magnitude;


			}
			lazerOnSence.transform.localScale = new Vector3 (longth, lazerOnSence.transform.localScale.y, 0f);
			lazerOnSence.transform.DOScaleY (0.5f, 0.2f);

			basic.setFartOnscene (lazerOnSence);

			if (lazerOnSence.transform.up != this.transform.up) {
				Vector3 formScale = lazerOnSence.transform.localScale;
				lazerOnSence.transform.localScale = new Vector3 (formScale.x,- formScale.y, formScale.z);
			}


			anim.SetFloat ("attack", 0f);
			Destroy (lazerOnSence, 0.5f);

		}
	}
}
